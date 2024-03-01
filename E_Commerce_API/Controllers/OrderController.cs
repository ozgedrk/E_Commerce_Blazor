﻿using E_Commerce_Bussiness.Repository.IRepository;
using E_Commerce_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            if (orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest,
                });
            }
            var orderHeader = await _orderRepository.Get(orderHeaderId.Value);
            if (orderHeader == null)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest,
                });
            }
            return Ok(orderHeader);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderRepository.GetAll();
            return Ok(result);
        }
    }
}
