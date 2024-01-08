using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderResponse> AddOrderAsync(OrderRequest createRequest);

        public Task<List<OrderResponse>> GetOrdersAsync(OrderFilter filter);

        public Task<OrderResponse?> GetOrderByIdAsync(Guid orderId);

        public Task<OrderResponse?> UpdateOrderByIdAsync(Guid orderId, OrderRequest updateRequest);

        public Task<bool> DeleteOrderByIdAsync(Guid orderId);

        public Task<OrderItemResponse> AddOrderItemAsync(OrderItemRequest createRequest);

        public Task<List<OrderItemResponse>> GetOrderItemsAsync(OrderItemFilter filter);

        public Task<OrderItemResponse?> GetOrderItemByIdAsync(Guid orderItemId);

        public Task<OrderItemResponse?> UpdateOrderItemByIdAsync(Guid orderItemId, OrderItemRequest updateRequest);

        public Task<bool> DeleteOrderItemByIdAsync(Guid orderItemId);
    }
}
