using AutoMapper;
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

            order.Status = Core.Enums.OrderStatusEnum.Draft;
            order.TotalAmount = 0;
            order.Tip = 0;

            return _mapper.Map<OrderResponse>(await _orderRepository.InsertAsync(order));
        }

        public async Task<OrderResponse> AddOrderAsync(Guid appointmentId, Guid taxId)
        {
            Appointment? appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

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
                order.TaxId = taxId;
                order.Date = DateTime.UtcNow;
                
                if(service != null)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        OrderId = order.Id,
                        ItemId = appointmentId,
                        UnitPrice = service.Price
                    };

                    await _orderItemRepository.InsertAsync(orderItem);
                }
                else
                {
                    throw new PoSException($"Service with id {appointment.ServiceId} could not be retrieved", System.Net.HttpStatusCode.BadRequest);
                }
            }else
            {
                throw new PoSException($"Appointment with id {appointmentId} could not be retrieved", System.Net.HttpStatusCode.BadRequest);
            }

            return _mapper.Map<OrderResponse>(await _orderRepository.InsertAsync(order));

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

                if (!await _customerRepository.Exists(x => x.Id == order.CustomerId))
                {
                    throw new PoSException($"Customer with id - {order.CustomerId} does not exist", System.Net.HttpStatusCode.BadRequest);
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

            order.Status = oldOrder.Status;
            order.TotalAmount = oldOrder.TotalAmount;
            order.Tip = oldOrder.Tip;

            return _mapper.Map<OrderResponse>(await _orderRepository.InsertAsync(order));
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
                        orderItem.UnitPriceDiscount = (orderItem.UnitPrice * discount.DiscountPercentage);
                    }
                }
            }

            orderItem.Subtotal = orderItem.Quantity * (orderItem.UnitPrice - orderItem.UnitPriceDiscount);

            orderItem = await _orderItemRepository.InsertAsync(orderItem);

            order.TotalAmount += orderItem.Subtotal;

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
                        orderItem.UnitPriceDiscount = (orderItem.UnitPrice * discount.DiscountPercentage);
                    }
                }
            }

            orderItem.Subtotal = orderItem.Quantity * (orderItem.UnitPrice - orderItem.UnitPriceDiscount);

            orderItem = await _orderItemRepository.UpdateAsync(orderItem);

            order.TotalAmount = order.TotalAmount - oldOrderItem.Subtotal + orderItem.Subtotal;

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

            order.TotalAmount -= orderItem.Subtotal;

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
            double discountVal = 0;

            if (order.DiscountId is not null)
            {
                var discount = await _discountRepository.GetFirstAsync(x => x.Id == order.DiscountId);

                if (discount is not null && discount.ValidUntil >= DateTime.Now)
                {
                    discountVal = discount.DiscountPercentage;
                }
            }

            response.ReceiptDateTime = DateTime.Now;
            response.EmployeeName = staff.FirstName;

            var orderItems = await _orderItemRepository.GetAsync(x => x.OrderId == order.Id);
            var receiptLines = new List<ReceiptLineResponse>();

            foreach (var orderItem in orderItems )
            {
                var receiptLine = new ReceiptLineResponse();

                var item = await _itemRepository.GetByIdAsync(orderItem.ItemId) ??
                    throw new PoSException($"Item with id - {orderItem.ItemId} in requested order does not exist",
                        System.Net.HttpStatusCode.BadRequest); ;

                receiptLine.ItemName = item.ItemName;
                receiptLine.UnitPrice = orderItem.UnitPrice;
                receiptLine.Quantity = orderItem.Quantity;
                receiptLine.DiscountAmount = orderItem.UnitPriceDiscount;
                receiptLine.TotalLineAmount = orderItem.Subtotal;

                receiptLines.Add(receiptLine);
            }

            response.ReceiptLines = receiptLines;

            response.TotalAmountBeforeDiscount = order.TotalAmount;

            response.TotalAmountWithDiscount = order.TotalAmount - (order.TotalAmount * discountVal);

            switch (tax.Category)
            {
                case Core.Enums.TaxCategoryEnum.Percent:
                    response.TotalAmountWithDiscountAfterTaxes = response.TotalAmountWithDiscount * (tax.TaxValue / 100 + 1);
                    break;
                case Core.Enums.TaxCategoryEnum.Flat:
                    response.TotalAmountWithDiscountAfterTaxes = response.TotalAmountWithDiscount + tax.TaxValue;
                    break;
            }

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

            if (order.Status == Core.Enums.OrderStatusEnum.Invoiced)
            {
                var payment = await _paymentRepository.GetFirstAsync(x => x.OrderId == order.Id);

                if (payment is not null)
                {
                    var paymentMethod = await _paymentMethodRepository.GetByIdAsync(payment.PaymentMethodId);

                    if (paymentMethod is not null)
                    {
                        response.PaymentMethod = paymentMethod.MethodName;
                    }

                    response.PaymentDateTime = payment.PaymentDate;
                    response.PaymentStatus = payment.Status;
                }
            }

            return response;
        }
    }
}
