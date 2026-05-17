using Cashier_system.BLL.DTOs;
using Cashier_system.DAL.Models;
using Cashier_system.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.BLL.Services;

public class OrderServices : IOrderServices
{
    private readonly IUnitOfWork _unitOfWork;
    public OrderServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    public  async Task<CommonRespond> DeleteOrder(DeleteOrderDto Dto)
    {
        var Existingorder=await _unitOfWork.OrderRepo.GetByIdAsync(Dto.Id);
        if (Existingorder == null) 
        {
            return new CommonRespond(false, "Order not found", null, null);
        }
     _unitOfWork.OrderRepo.DeleteAsync(Existingorder);
        await _unitOfWork.SaveAsync();
        return new CommonRespond(true, "Order deleted successfully", null, null);


    }

    public async Task<CommonRespond> CreatOrderAsync(CreateOrderDto Dto, string appUserId)
    {
        if (Dto.Items == null || !Dto.Items.Any())
            return new CommonRespond(false, "No items in the order", null, null);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderDate = DateTime.Now,
            AppUserId = appUserId,
            OrderItems = new List<OrderItem>()
        };

        decimal total = 0;

        foreach (var item in Dto.Items)
        {
            var product = await _unitOfWork.ProductRepo.GetByIdAsync(item.ProductId);

            if (product == null)
                return new CommonRespond(false, $"Product {item.ProductId} not found", null, null);

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
                TotalPrice = product.Price * item.Quantity
            };

            total += orderItem.TotalPrice;
            order.OrderItems.Add(orderItem);
        }

        order.TotalPrice = total;

  

        decimal discount = 0;
        decimal finalPrice = total;

        if (Dto.DiscountType.HasValue && Dto.DiscountValue.HasValue)
        {
            if (Dto.DiscountType == DiscountType.Percentage)
            {
                if (Dto.DiscountValue <= 0 || Dto.DiscountValue > 100)
                    return new CommonRespond(false, "Invalid percentage", null, null);

                discount = (total * Dto.DiscountValue.Value) / 100;
            }
            else if (Dto.DiscountType == DiscountType.Fixed)
            {
                if (Dto.DiscountValue <= 0 || Dto.DiscountValue > total)
                    return new CommonRespond(false, "Invalid discount value", null, null);

                discount = Dto.DiscountValue.Value;
            }

            finalPrice = total - discount;
        }

        order.Discount = discount;
        order.FinalPrice = finalPrice;

        await _unitOfWork.OrderRepo.AddAsync(order);
        await _unitOfWork.SaveAsync();
        var result = new GetOrderDto
        {
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            Discount = order.Discount,
            FinalPrice = order.FinalPrice
        };

        return new CommonRespond(true, "Order created successfully", null, result);
    }

    public async Task<CommonRespond> GetOrderByDate(GetOrderByDateDto dto)
    {
        var orders = await _unitOfWork.OrderRepo
     .GetOrdersByDateRangeAsync(dto.DateStart, dto.DateEnd);

        if (orders == null || !orders.Any())
            return new CommonRespond(false, "No orders found", null, null);
        var result = orders.Select(o => new GetOrderDto
        {
            Id = o.Id,
            TotalPrice = o.TotalPrice,
            Discount = o.Discount,
            FinalPrice = o.FinalPrice,
            CreatedAt = o.OrderDate,

            Items = o.OrderItems?.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product?.Name,
                Quantity = i.Quantity,

            }).ToList() ?? new List<OrderItemDto>()
        }).ToList();
        return new CommonRespond(true, "Success", null, result);
    }

    public async Task<CommonRespond> GetOrderById(GetOrderDto dto)
    {
        var order = await _unitOfWork.OrderRepo.GetOrderswithorderitem(dto.Id);

        if (order == null)
            return new CommonRespond(false, "Order not found", null, null);
        var result = new GetOrderDto
        {
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            Discount = order.Discount,
            FinalPrice = order.FinalPrice,
            CreatedAt = order.OrderDate,

            Items = order.OrderItems?.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product?.Name,
                Quantity = i.Quantity

            }).ToList() ?? new List<OrderItemDto>()
        };

        return new CommonRespond(true, "Success", null, result);
    }
}