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
            _mapper = mapper;
        }

        public async Task<OrderResponse> AddOrderAsync(OrderRequest createRequest)
        {
            var order = _mapper.Map<Order>(createRequest);

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

            order.Status = Core.Enums.OrderStatusEnum.Unpaid;
            order.TotalAmount = 0;
            order.Tip = 0;

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

            if (oldOrder.Status == Core.Enums.OrderStatusEnum.Unpaid)
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
                throw new PoSException($"Order with id - {orderId} has been paid and can not be updated", System.Net.HttpStatusCode.BadRequest);
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

            if (order.Status == Core.Enums.OrderStatusEnum.Paid)
            {
                throw new PoSException($"Paid orders can not be deleted", System.Net.HttpStatusCode.BadRequest);
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

            if (order.Status == Core.Enums.OrderStatusEnum.Paid)
            {
                throw new PoSException($"Can not add items to order, order with id - {orderItem.OrderId} is paid and can not be modified",
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

            if (order.Status == Core.Enums.OrderStatusEnum.Paid)
            {
                throw new PoSException($"Can not edit order items, order with id - {orderItem.OrderId} is paid and can not be modified",
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

            if (order.Status == Core.Enums.OrderStatusEnum.Paid)
            {
                throw new PoSException($"Paid order's items can not be deleted", System.Net.HttpStatusCode.BadRequest);
            }

            order.TotalAmount -= orderItem.Subtotal;

            await _orderRepository.UpdateAsync(order);

            return await _orderItemRepository.DeleteAsync(orderItemId);
        }
    }
}