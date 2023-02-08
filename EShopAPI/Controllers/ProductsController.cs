using Repositories.Products;
using AutoMapper;
using Repositories.Orders;
using Microsoft.AspNetCore.Mvc;
using DataModels.Dtos;
using DataModels;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of products
        /// </summary>
        /// <param name="checkQuantity"> Optional parameter to get only products with remaining quantity</param>
        /// <returns></returns>
        // GET: api/Products
        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProductDto>>> GetProducts(bool checkQuantity = false)
        {
            var products = await _productRepository.GetProducts(checkQuantity);
            if (!products.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

        /// <summary>
        /// Get a specific product
        /// </summary>
        /// <param name="id"> The product's id</param>
        /// <returns></returns>
        // GET api/Products/id
        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(await _productRepository.GetProduct(id)));
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="productDto"> An instance of a product</param>
        /// <returns></returns>
        // POST api/Products
        [HttpPost(Name = "PostProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product = await _productRepository.Post(product);
            return Ok(_mapper.Map<ProductDto>(product));
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id"> The product's id</param>
        /// <param name="productDto"> The instance of the product</param>
        /// <returns></returns>
        // PUT api/Products/id
        [HttpPut("{id}", Name = "PutProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutProduct(int id, ProductDto productDto)
        {
            var productInDb = await _productRepository.GetProduct(productDto.Id);
            if (productInDb == null || id != productDto.Id)
            {
                return NotFound();
            }
            _mapper.Map(productDto, productInDb);
            await _productRepository.Put(productInDb);
            return NoContent();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"> The product's id</param>
        /// <returns></returns>
        // DELETE api/Products/id
        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            var orderProducts = await _orderRepository.GetOrderProductsByProductId(id);
            if (!orderProducts.Any())
            {
                await _productRepository.Delete(id);
            }
            return NoContent();
        }
    }
}
