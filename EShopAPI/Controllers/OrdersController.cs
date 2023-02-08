using Repositories.Orders;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DataModels.Dtos;
using DataModels;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of orders
        /// </summary>
        /// <returns></returns>
        // GET: api/Orders
        [HttpGet(Name = "GetOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderRepository.GetOrders();
            if (!orders.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        /// <summary>
        /// Get a list of orders for a specific customer
        /// </summary>
        /// <param name="id"> The customer's id</param>
        /// <returns></returns>
        // GET: api/Orders/Customer/id
        [HttpGet("Customer/{id:int}", Name = "GetOrdersByCustomerId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomerId(int id)
        {
            var orders = await _orderRepository.GetOrderByCustomerId(id);
            if (!orders.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        /// <summary>
        /// Get a specific order
        /// </summary>
        /// <param name="id"> The order's id</param>
        /// <returns></returns>
        // GET api/Orders/id
        [HttpGet("{id:int}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderDto>(order));
        }

        /// <summary>
        /// Get a list of products from a specific order
        /// </summary>
        /// <param name="id"> The order's id</param>
        /// <returns></returns>
        // GET api/Orders/id/Products
        [HttpGet("{id:int}/Products", Name = "GetOrderProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderProductsDto>>> GetOrderProducts(int id)
        {
            var orderProducts = await _orderRepository.GetOrderProducts(id);
            if (!orderProducts.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<OrderProductsDto>>(orderProducts));
        }

        /// <summary>
        /// Get a list of orders for a specific product
        /// </summary>
        /// <param name="id"> The product's id</param>
        /// <returns></returns>
        // GET api/Orders/Products/id
        [HttpGet("Products/{id:int}", Name = "GetOrderProductsByProductId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderProductsDto>>> GetOrderProductsByProductId(int id)
        {
            var orderProducts = await _orderRepository.GetOrderProductsByProductId(id);
            if (!orderProducts.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<OrderProductsDto>>(orderProducts));
        }

        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="orderDto"> An instance of an order</param>
        /// <returns></returns>
        // POST api/Orders
        [HttpPost(Name = "PostOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order = await _orderRepository.Post(order);
            _mapper.Map(order, orderDto);
            return CreatedAtAction("GetOrder", new { id = orderDto.Id }, orderDto);
        }

        /// <summary>
        /// Insert the products of an order
        /// </summary>
        /// <param name="id"> The order's id</param>
        /// <param name="orderProductsDto"> A list of products</param>
        /// <returns></returns>
        // POST api/Orders/id/Products
        [HttpPost("{id:int}/Products", Name = "PostOrderProducts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<OrderProductsDto>>> PostOrderProducts(int id, IEnumerable<OrderProductsDto> orderProductsDto)
        {
            if (orderProductsDto.Any(op => op.OrderId != id))
            {
                return BadRequest();
            }
            var orderProducts = _mapper.Map<IEnumerable<OrderProducts>>(orderProductsDto);
            orderProducts = await _orderRepository.PostOrderProducts(orderProducts);
            _mapper.Map(orderProducts, orderProductsDto);
            return CreatedAtAction("GetOrderProducts","Orders",new { id }, orderProductsDto);
        }

        /// <summary>
        /// Update an order
        /// </summary>
        /// <param name="id"> The order's id</param>
        /// <param name="orderDto"> The order's instance</param>
        /// <returns></returns>
        // PUT api/Orders/id
        [HttpPut("{id:int}", Name = "PutOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> PutOrder(int id, OrderDto orderDto)
        {
            var order = await _orderRepository.GetOrder(id);
            if (order == null || id != orderDto.Id)
            {
                return NotFound();
            }
            _mapper.Map(orderDto, order);
            await _orderRepository.Put(order);
            return NoContent();
        }

        /// <summary>
        /// Delete an order
        /// </summary>
        /// <param name="id"> The order's id</param>
        /// <returns></returns>
        // DELETE api/Orders/id
        [HttpDelete("{id:int}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            await _orderRepository.DeleteOrder(id);
            return NoContent();
        }
    }
}
