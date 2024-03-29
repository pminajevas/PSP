﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly ITaxRepository _taxRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            ICustomerRepository customerRepository,
            IBusinessRepository businessRepository,
            IStaffRepository staffRepository,
            ITaxRepository taxRepository,
            IItemRepository itemRepository,
            IServiceRepository serviceRepository,
            IDiscountRepository discountRepository,
            ILoyaltyProgramRepository loyaltyProgramRepository,
            IPaymentRepository paymentRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IAppointmentRepository appointmentRepository,
            IMapper mapper
        )
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
            _businessRepository = businessRepository;
            _staffRepository = staffRepository;
            _taxRepository = taxRepository;
            _itemRepository = itemRepository;
            _serviceRepository = serviceRepository;
            _discountRepository = discountRepository;
            _loyaltyProgramRepository = loyaltyProgramRepository;
            _paymentRepository = paymentRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> AddOrderAsync(OrderRequest createRequest)
        {
            var order = _mapper.Map<Order>(createRequest);

            if (!await _businessRepository.Exists(x => x.Id == order.BusinessId))
            {
                throw new PoSException($"Business with id - {order.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (order.CustomerId is not null)
            {
                if (!await _customerRepository.Exists(x => x.Id == order.CustomerId))
                {
                    throw new PoSException($"Customer with id - {order.CustomerId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }

            if (order.DiscountId is not null)
            {
                if (!await _discountRepository.Exists(x => x.Id == order.DiscountId))
                {
                    throw new PoSException($"Discount with id - {order.DiscountId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }

            if (!await _staffRepository.Exists(x => x.Id == order.StaffId))
            {
                throw new PoSException($"Staff with id - {order.StaffId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (!await _taxRepository.Exists(x => x.Id == order.TaxId))
            {
                throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            order.Date = DateTime.Now;
            order.Status = Core.Enums.OrderStatusEnum.Draft;
            order.TotalAmount = 0;
            order.TotalAmountWithOrderDiscount = 0;
            order.TotalAmountBase = 0;
            order.Tip = 0;

            return _mapper.Map<OrderResponse>(await _orderRepository.InsertAsync(order));
        }

        public async Task<OrderResponse> AddOrderAsync(AppointmentOrderRequest body)
        {
            if (!await _appointmentRepository.Exists(x => x.Id == body.AppointmentId))
            {
                throw new PoSException($"Appointment with id - {body.AppointmentId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            Appointment? appointment = await _appointmentRepository.GetByIdAsync(body.AppointmentId);

            Order order = new Order();

            if (appointment!=null)
            {
                if (!await _serviceRepository.Exists(x => x.Id == appointment.ServiceId))
                {
                    throw new PoSException($"Service with id - {appointment.ServiceId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }

                Service? service = await _serviceRepository.GetByIdAsync(appointment.ServiceId);

                order.CustomerId = appointment.CustomerId;
                order.BusinessId = appointment.BusinessId;
                order.StaffId = appointment.StaffId;
                order.TaxId = body.TaxId;
                order.Date = DateTime.UtcNow;
                order.Status = Core.Enums.OrderStatusEnum.Draft;
                
                if(service != null)
                {
                    double discountAmount = 0;

                    if (service.DiscountId is not null)
                    {
                        var discount = await _discountRepository.GetByIdAsync(service.DiscountId) ??
                            throw new PoSException($"Discount with id - {service.DiscountId} does not exist", System.Net.HttpStatusCode.BadRequest);

                        discountAmount = Math.Round((service.Price * discount.DiscountPercentage), 2);
                    }

                    var tax = await _taxRepository.GetByIdAsync(body.TaxId) ??
                        throw new PoSException($"Tax with id - {body.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest); ;

                    order.TotalAmountBase = Math.Round(service.Price, 2);
                    order.TotalAmountWithOrderDiscount = Math.Round(order.TotalAmountBase - discountAmount, 2);
                    order.TotalAmount = Math.Round(order.TotalAmountBase - discountAmount + (order.TotalAmountWithOrderDiscount * tax.TaxValue / 100), 2); ;

                    order = await _orderRepository.InsertAsync(order);

                    OrderItem orderItem = new OrderItem()
                    {
                        OrderId = order.Id,
                        ItemId = service.Id,
                        UnitPrice = service.Price,
                        UnitPriceDiscount = discountAmount,
                        Quantity = 1,
                        Subtotal = service.Price - discountAmount
                    };

                    await _orderItemRepository.InsertAsync(orderItem);
                }
                else
                {
                    throw new PoSException($"Service with id {appointment.ServiceId} could not be retrieved", System.Net.HttpStatusCode.BadRequest);
                }
            }else
            {
                throw new PoSException($"Appointment with id {body.AppointmentId} could not be retrieved", System.Net.HttpStatusCode.BadRequest);
            }

            return _mapper.Map<OrderResponse>(order);

        }

        public async Task<List<OrderResponse>> GetOrdersAsync(OrderFilter orderFilter)
        {
            var filter = PredicateBuilder.True<Order>();
            Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null;

            if (orderFilter.CustomerId != null)
            {
                filter = filter.And(x => x.CustomerId == orderFilter.CustomerId);
            }

            if (orderFilter.BusinessId != null)
            {
                filter = filter.And(x => x.BusinessId == orderFilter.BusinessId);
            }

            if (orderFilter.StaffId != null)
            {
                filter = filter.And(x => x.StaffId == orderFilter.StaffId);
            }

            if (orderFilter.TaxId != null)
            {
                filter = filter.And(x => x.TaxId == orderFilter.TaxId);
            }

            if (orderFilter.Status != null)
            {
                filter = filter.And(x => x.Status == orderFilter.Status);
            }

            if (orderFilter.FromDate != null)
            {
                filter = filter.And(x => x.Date >= orderFilter.FromDate);
            }

            if (orderFilter.ToDate != null)
            {
                filter = filter.And(x => x.Date <= orderFilter.ToDate);
            }

            if (orderFilter.OrderBy != string.Empty)
            {
                switch (orderFilter.Sorting)
                {
                    case Sorting.dsc:
                        orderBy = x => x.OrderByDescending(p => EF.Property<Order>(p, orderFilter.OrderBy));
                        break;
                    default:
                        orderBy = x => x.OrderBy(p => EF.Property<Order>(p, orderFilter.OrderBy));
                        break;
                }
            }

            var orders = await _orderRepository.GetAsync(
                filter,
                orderBy,
                orderFilter.ItemsToSkip(),
                orderFilter.PageSize
            );

            return _mapper.Map<List<OrderResponse>>(orders);
        }

        public async Task<OrderResponse?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
            {
                throw new PoSException($"Order with id - {orderId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<OrderResponse?> UpdateOrderByIdAsync(Guid orderId, OrderRequest updateRequest)
        {
            var order = _mapper.Map<Order>(updateRequest);
            order.Id = orderId;

            var oldOrder = await _orderRepository.GetFirstAsync(x => x.Id == orderId) ??
                throw new PoSException($"Order with id - {orderId} does not exist and can not be updated",
                    System.Net.HttpStatusCode.BadRequest);

            if (oldOrder.Status == Core.Enums.OrderStatusEnum.Draft)
            {
                if (!await _businessRepository.Exists(x => x.Id == order.BusinessId))
                {
                    throw new PoSException($"Business with id - {order.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }

                if (!await _staffRepository.Exists(x => x.Id == order.StaffId))
                {
                    throw new PoSException($"Staff with id - {order.StaffId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }

                if (!await _taxRepository.Exists(x => x.Id == order.TaxId))
                {
                    throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }
            else
            {
                throw new PoSException($"Order with id - {orderId} has been paid or is confirmed and can not be updated", System.Net.HttpStatusCode.BadRequest);
            }

            if (order.DiscountId is not null && oldOrder.DiscountId != order.DiscountId)
            {
                var discount = await _discountRepository.GetByIdAsync(order.DiscountId) ??
                    throw new PoSException($"Discount with id - {order.DiscountId} does not exist", System.Net.HttpStatusCode.BadRequest);

                var tax = await _taxRepository.GetByIdAsync(order.TaxId) ??
                    throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                order.TotalAmountBase = Math.Round(oldOrder.TotalAmountBase, 2);
                order.TotalAmountWithOrderDiscount = Math.Round(order.TotalAmountBase - (order.TotalAmountBase * discount.DiscountPercentage), 2);
                order.TotalAmount = Math.Round(order.TotalAmountWithOrderDiscount + (order.TotalAmountWithOrderDiscount * tax.TaxValue / 100), 2);
            }
            else
            {
                order.TotalAmountBase = oldOrder.TotalAmountBase;
                order.TotalAmountWithOrderDiscount = oldOrder.TotalAmountWithOrderDiscount;
                order.TotalAmount = oldOrder.TotalAmount;
            }

            order.Status = oldOrder.Status;
            order.Tip = oldOrder.Tip;
            order.Date = oldOrder.Date;

            return _mapper.Map<OrderResponse>(await _orderRepository.UpdateAsync(order));
        }

        public async Task<bool> DeleteOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetFirstAsync(x => x.Id == orderId) ??
                throw new PoSException($"Order with id - {orderId} does not exist",
                    System.Net.HttpStatusCode.BadRequest);

            if (order.Status != Core.Enums.OrderStatusEnum.Draft)
            {
                throw new PoSException($"Paid or confirmed orders can not be deleted", System.Net.HttpStatusCode.BadRequest);
            }

            IEnumerable<OrderItem> orderItems = await _orderItemRepository.GetAsync(x => x.OrderId == orderId);

            foreach (OrderItem orderItem in orderItems)
            {
                await _orderItemRepository.DeleteAsync(orderItem);
            }

            return await _orderRepository.DeleteAsync(orderId);
        }

        public async Task<OrderItemResponse> AddOrderItemAsync(OrderItemRequest createRequest)
        {
            var orderItem = _mapper.Map<OrderItem>(createRequest);

            var order = await _orderRepository.GetFirstAsync(x => x.Id == orderItem.OrderId) ??
                throw new PoSException($"Can not add item, order with id - {orderItem.OrderId} does not exist",
                    System.Net.HttpStatusCode.BadRequest);

            if (order.Status != Core.Enums.OrderStatusEnum.Draft)
            {
                throw new PoSException($"Can not add items to order, order with id - {orderItem.OrderId} is paid or confirmed and can not be modified",
                    System.Net.HttpStatusCode.BadRequest);
            }

            if (orderItem.Quantity <= 0)
            {
                throw new PoSException($"Only positive decimals are allowed for quantity",
                    System.Net.HttpStatusCode.BadRequest);
            }

            var item = await _itemRepository.GetFirstAsync(x => x.Id == orderItem.ItemId);
            var service = await _serviceRepository.GetFirstAsync(x => x.Id == orderItem.ItemId);
            Guid? discountId = null;

            if (item is not null)
            {
                orderItem.Type = Core.Enums.OrderItemTypeEnum.Item;

                if (item.DiscountId is not null)
                {
                    discountId = item.DiscountId;
                }

                orderItem.UnitPrice = item.Price;
            }
            else if (service is not null)
            {
                orderItem.Type = Core.Enums.OrderItemTypeEnum.Service;

                if (service.DiscountId is not null)
                {
                    discountId = service.DiscountId;
                }

                orderItem.UnitPrice = service.Price;
            }
            else
            {
                throw new PoSException($"Can not add item to order, neither item or service with id - {orderItem.ItemId} exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            if (discountId is not null)
            {
                var discount = await _discountRepository.GetByIdAsync(discountId);

                if (discount is not null)
                {
                    if (discount.ValidUntil >= DateTime.Now)
                    {
                        orderItem.UnitPriceDiscount = Math.Round(orderItem.UnitPrice * discount.DiscountPercentage, 2);
                    }
                }
            }

            orderItem.Subtotal = Math.Round(orderItem.Quantity * (orderItem.UnitPrice - orderItem.UnitPriceDiscount), 2);

            orderItem = await _orderItemRepository.InsertAsync(orderItem);

            if (order.DiscountId is not null)
            {
                var discount = await _discountRepository.GetByIdAsync(order.DiscountId) ??
                    throw new PoSException($"Discount with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                var tax = await _taxRepository.GetByIdAsync(order.TaxId) ??
                    throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                order.TotalAmountBase = Math.Round(order.TotalAmountBase + orderItem.Subtotal, 2);
                order.TotalAmountWithOrderDiscount = Math.Round(order.TotalAmountBase - (order.TotalAmountBase * discount.DiscountPercentage), 2);
                order.TotalAmount = Math.Round(order.TotalAmountWithOrderDiscount + (order.TotalAmountWithOrderDiscount * tax.TaxValue / 100), 2);
            }
            else
            {
                var tax = await _taxRepository.GetByIdAsync(order.TaxId) ??
                    throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                order.TotalAmountBase = Math.Round(order.TotalAmountBase + orderItem.Subtotal, 2);
                order.TotalAmountWithOrderDiscount = order.TotalAmountBase;
                order.TotalAmount = Math.Round(order.TotalAmountWithOrderDiscount + (order.TotalAmountWithOrderDiscount * tax.TaxValue / 100), 2);
            }

            await _orderRepository.UpdateAsync(order);

            return _mapper.Map<OrderItemResponse>(orderItem);
        }

        public async Task<List<OrderItemResponse>> GetOrderItemsAsync(OrderItemFilter orderItemfilter)
        {
            var filter = PredicateBuilder.True<OrderItem>();
            Func<IQueryable<OrderItem>, IOrderedQueryable<OrderItem>>? orderBy = null;

            if (orderItemfilter.OrderId != null)
            {
                filter = filter.And(x => x.OrderId == orderItemfilter.OrderId);
            }

            if (orderItemfilter.Type != null)
            {
                filter = filter.And(x => x.Type == orderItemfilter.Type);
            }

            if (orderItemfilter.OrderBy != string.Empty)
            {
                switch (orderItemfilter.Sorting)
                {
                    case Sorting.dsc:
                        orderBy = x => x.OrderByDescending(p => EF.Property<OrderItem>(p, orderItemfilter.OrderBy));
                        break;
                    default:
                        orderBy = x => x.OrderBy(p => EF.Property<OrderItem>(p, orderItemfilter.OrderBy));
                        break;
                }
            }

            var orderItems = await _orderItemRepository.GetAsync(
                filter,
                orderBy,
                orderItemfilter.ItemsToSkip(),
                orderItemfilter.PageSize
            );

            return _mapper.Map<List<OrderItemResponse>>(orderItems);
        }

        public async Task<OrderItemResponse?> GetOrderItemByIdAsync(Guid orderItemId)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId);

            if (orderItem is null)
            {
                throw new PoSException($"Order item with id - {orderItemId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<OrderItemResponse>(orderItem);
        }

        public async Task<OrderItemResponse?> UpdateOrderItemByIdAsync(Guid orderItemId, OrderItemRequest updateRequest)
        {
            var orderItem = _mapper.Map<OrderItem>(updateRequest);
            orderItem.Id = orderItemId;

            var oldOrderItem = await _orderItemRepository.GetFirstAsync(x => x.Id == orderItemId) ??
                throw new PoSException($"Can not update order item, order item with id - {orderItemId} does not exist",
                    System.Net.HttpStatusCode.BadRequest);

            var order = await _orderRepository.GetFirstAsync(x => x.Id == orderItem.OrderId) ??
                throw new PoSException($"Can not update item, order with id - {orderItem.OrderId} no longer exists",
                    System.Net.HttpStatusCode.BadRequest);

            if (order.Status != Core.Enums.OrderStatusEnum.Draft)
            {
                throw new PoSException($"Can not edit order items, order with id - {orderItem.OrderId} is paid or confirmed and can not be modified",
                    System.Net.HttpStatusCode.BadRequest);
            }

            if (oldOrderItem.OrderId != orderItem.OrderId)
            {
                throw new PoSException($"Can not change order to which the order item is assigned",
                    System.Net.HttpStatusCode.BadRequest);
            }

            if (orderItem.Quantity <= 0)
            {
                throw new PoSException($"Only positive decimals are allowed for quantity",
                    System.Net.HttpStatusCode.BadRequest);
            }

            var item = await _itemRepository.GetFirstAsync(x => x.Id == orderItem.ItemId);
            var service = await _serviceRepository.GetFirstAsync(x => x.Id == orderItem.ItemId);
            Guid? discountId = null;

            if (item is not null)
            {
                orderItem.Type = Core.Enums.OrderItemTypeEnum.Item;

                if (item.DiscountId is not null)
                {
                    discountId = item.DiscountId;
                }

                orderItem.UnitPrice = item.Price;
            }
            else if (service is not null)
            {
                orderItem.Type = Core.Enums.OrderItemTypeEnum.Service;

                if (service.DiscountId is not null)
                {
                    discountId = service.DiscountId;
                }

                orderItem.UnitPrice = service.Price;
            }
            else
            {
                throw new PoSException($"Can not update order item, neither item or service with id - {orderItem.ItemId} exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            if (discountId is not null)
            {
                var discount = await _discountRepository.GetByIdAsync(discountId);

                if (discount is not null)
                {
                    if (discount.ValidUntil >= DateTime.Now)
                    {
                        orderItem.UnitPriceDiscount = Math.Round(orderItem.UnitPrice * discount.DiscountPercentage, 2);
                    }
                }
            }

            orderItem.Subtotal = Math.Round(orderItem.Quantity * (orderItem.UnitPrice - orderItem.UnitPriceDiscount), 2);

            orderItem = await _orderItemRepository.UpdateAsync(orderItem);

            if (order.DiscountId is not null)
            {
                var discount = await _discountRepository.GetByIdAsync(order.DiscountId) ??
                    throw new PoSException($"Discount with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                var tax = await _taxRepository.GetByIdAsync(order.TaxId) ??
                    throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                order.TotalAmountBase = Math.Round(order.TotalAmountBase + orderItem.Subtotal, 2);
                order.TotalAmountWithOrderDiscount = Math.Round(order.TotalAmountBase - (order.TotalAmountBase * discount.DiscountPercentage), 2);
                order.TotalAmount = Math.Round(order.TotalAmountWithOrderDiscount + (order.TotalAmountWithOrderDiscount * tax.TaxValue / 100), 2);
            }
            else
            {
                var tax = await _taxRepository.GetByIdAsync(order.TaxId) ??
                    throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                order.TotalAmountBase = Math.Round(order.TotalAmountBase + orderItem.Subtotal, 2);
                order.TotalAmountWithOrderDiscount = order.TotalAmountBase;
                order.TotalAmount = Math.Round(order.TotalAmountWithOrderDiscount + (order.TotalAmountWithOrderDiscount * tax.TaxValue / 100), 2);
            }

            await _orderRepository.UpdateAsync(order);

            return _mapper.Map<OrderItemResponse>(orderItem);
        }

        public async Task<bool> DeleteOrderItemByIdAsync(Guid orderItemId)
        {
            var orderItem = await _orderItemRepository.GetFirstAsync(x => x.Id == orderItemId) ??
                throw new PoSException($"Order item with id - {orderItemId} does not exist",
                    System.Net.HttpStatusCode.BadRequest);

            var order = await _orderRepository.GetFirstAsync(x => x.Id == orderItem.OrderId) ??
                throw new PoSException($"Order item's order with id - {orderItem.OrderId} no longer exists",
                    System.Net.HttpStatusCode.BadRequest);

            if (order.Status != Core.Enums.OrderStatusEnum.Draft)
            {
                throw new PoSException($"Paid or confirmed order's items can not be deleted", System.Net.HttpStatusCode.BadRequest);
            }

            if (order.DiscountId is not null)
            {
                var discount = await _discountRepository.GetByIdAsync(order.DiscountId) ??
                    throw new PoSException($"Discount with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                var tax = await _taxRepository.GetByIdAsync(order.TaxId) ??
                    throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                order.TotalAmountBase = Math.Round(order.TotalAmountBase - orderItem.Subtotal, 2);
                order.TotalAmountWithOrderDiscount = Math.Round(order.TotalAmountBase - (order.TotalAmountBase * discount.DiscountPercentage), 2);
                order.TotalAmount = Math.Round(order.TotalAmountWithOrderDiscount + (order.TotalAmountWithOrderDiscount * tax.TaxValue / 100), 2);
            }
            else
            {
                var tax = await _taxRepository.GetByIdAsync(order.TaxId) ??
                    throw new PoSException($"Tax with id - {order.TaxId} does not exist", System.Net.HttpStatusCode.BadRequest);

                order.TotalAmountBase = Math.Round(order.TotalAmountBase - orderItem.Subtotal, 2);
                order.TotalAmountWithOrderDiscount = order.TotalAmountBase;
                order.TotalAmount = Math.Round(order.TotalAmountWithOrderDiscount + (order.TotalAmountWithOrderDiscount * tax.TaxValue / 100), 2);
            }

            await _orderRepository.UpdateAsync(order);

            return await _orderItemRepository.DeleteAsync(orderItemId);
        }

        public async Task<ReceiptResponse> GenerateReceipt(ReceiptRequest receiptRequest)
        {
            var order = await _orderRepository.GetFirstAsync(x => x.Id == receiptRequest.OrderId) ??
                throw new PoSException($"Order with id - {receiptRequest.OrderId} does not exist",
                    System.Net.HttpStatusCode.BadRequest);

            var staff = await _staffRepository.GetByIdAsync(order.StaffId) ??
                throw new PoSException($"Order with id - {receiptRequest.OrderId} does not have correct worker assigned",
                    System.Net.HttpStatusCode.BadRequest);

            var tax = await _taxRepository.GetFirstAsync(x => x.Id == order.TaxId && x.ValidFrom < DateTime.Now && x.ValidUntil > DateTime.Now) ??
                throw new PoSException($"Order with id - {receiptRequest.OrderId} does not have valid tax assigned",
                    System.Net.HttpStatusCode.BadRequest);

            var response = new ReceiptResponse();

            response.ReceiptDateTime = DateTime.Now;
            response.EmployeeName = staff.FirstName;

            var orderItems = await _orderItemRepository.GetAsync(x => x.OrderId == order.Id);
            var receiptLines = new List<ReceiptLineResponse>();
            var paymentLines = new List<PaymentLineResponse>();

            foreach (var orderItem in orderItems )
            {
                var receiptLine = new ReceiptLineResponse();

                Service? service = await _serviceRepository.GetByIdAsync(orderItem.ItemId);
                Item? item = await _itemRepository.GetByIdAsync(orderItem.ItemId);

                if (item is null && service is null)
                {
                    throw new PoSException($"Item or service with id - {orderItem.ItemId} in requested order does not exist",
                        System.Net.HttpStatusCode.BadRequest); ;
                }

                receiptLine.ItemName = item is not null ? item.ItemName : service is not null ? service.ServiceName : "";
                receiptLine.UnitPrice = orderItem.UnitPrice;
                receiptLine.Quantity = orderItem.Quantity;
                receiptLine.DiscountAmount = orderItem.UnitPriceDiscount;
                receiptLine.TotalLineAmount = orderItem.Subtotal;

                receiptLines.Add(receiptLine);
            }

            response.ReceiptLines = receiptLines;

            response.TotalAmountBeforeDiscount = order.TotalAmountBase;
            response.TotalAmountWithDiscount = order.TotalAmountWithOrderDiscount;
            response.TotalAmountWithDiscountAfterTaxes = order.TotalAmount;

            if (order.Status == Core.Enums.OrderStatusEnum.Draft)
            {
                if (order.CustomerId is not null)
                {
                    var customer = await _customerRepository.GetByIdAsync(order.CustomerId);

                    if (customer is not null && customer.LoyaltyId is not null)
                    {
                        var loyalty = await _loyaltyProgramRepository.GetByIdAsync(customer.LoyaltyId);

                        if (loyalty is not null)
                        {
                            customer.Points += loyalty.PointsPerPurchase;

                            await _customerRepository.UpdateAsync(customer);
                        }
                    }
                }

                order.Status = Core.Enums.OrderStatusEnum.Confirmed;
                await _orderRepository.UpdateAsync(order);
            }

            if (order.Status == Core.Enums.OrderStatusEnum.Invoiced || order.Status == Core.Enums.OrderStatusEnum.Confirmed)
            {
                var payments = await _paymentRepository.GetAsync(x => x.OrderId == order.Id);

                foreach (var payment in payments)
                {
                    var paymentLine = new PaymentLineResponse();

                    var paymentMethod = await _paymentMethodRepository.GetByIdAsync(payment.PaymentMethodId);

                    if (paymentMethod is not null)
                    {
                        paymentLine.PaymentMethod = paymentMethod.MethodName;
                    }

                    paymentLine.PaymentDateTime = payment.PaymentDate;
                    paymentLine.PaymentStatus = payment.Status;
                    paymentLine.PaymentAmount = payment.Amount;

                    paymentLines.Add(paymentLine);
                }
            }

            response.PaymentLines = paymentLines;

            return response;
        }
    }
}
