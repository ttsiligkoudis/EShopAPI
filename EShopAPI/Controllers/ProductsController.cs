using Repositories.Products;
using AutoMapper;
using Repositories.Orders;
using Microsoft.AspNetCore.Mvc;
using DataModels.Dtos;
using DataModels;
using Enums;
using Repositories.Customers;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IOrderRepository orderRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of products
        /// </summary>
        /// <param name="checkQuantity"> Optional parameter to get only products with remaining quantity</param>
        /// <param name="category"> Optional parameter to get only products of specific category</param>
        /// <returns></returns>
        // GET: api/Products
        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProductDto>>> GetProducts(bool checkQuantity = false, Category? category = null)
        {
            var products = await _productRepository.GetProducts(checkQuantity, category);
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
            return Ok(_mapper.Map(product, productDto));
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
            await _productRepository.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Update products quantity
        /// </summary>
        /// <param name="productsDto"> A list of products</param>
        /// <returns></returns>
        // PUT api/Products
        [HttpPut(Name = "PutProducts")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProductDto>>> PutProductsQuantity(List<ProductDto> productsDto)
        {
            if (productsDto == null)
            {
                return NoContent();
            }

            var productIds = productsDto.Select(s => s.Id).ToList();
            var productsInDb = await _productRepository.GetProductsbyIds(productIds);

            if (productsInDb == null)
            {
                return NotFound();
            }

            foreach (var product in productsInDb)
            {
                var productDto = productsDto.First(w => w.Id == product.Id);

                if (productDto.Quantity >= product.Quantity)
                {
                    var quantity = product.Quantity;
                    product.Quantity = 0;
                    productDto.Quantity = quantity;
                }
                else
                {
                    product.Quantity -= productDto.Quantity;
                }

                await _productRepository.Put(product);
            }

            await _productRepository.SaveChangesAsync();
            return Ok(productsDto);
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

        /// <summary>
        /// Get the number of products
        /// </summary>
        /// <param name="checkQuantity"> Optional parameter to get only products with remaining quantity</param>
        /// <param name="category"> Optional parameter to get only products of specific category</param>
        /// <returns></returns>
        // GET: api/Products/Count
        [HttpGet("Count", Name = "GetProductsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetProductsCount(bool checkQuantity = false, Category? category = null)
        {
            var productsCount = await _productRepository.GetProductsCount(checkQuantity, category);
            if (productsCount == 0)
            {
                return NotFound();
            }
            return Ok(productsCount);
        }

        /// <summary>
        /// Get random products
        /// </summary>
        /// <param name="length"> This parameter determines the number of random products</param>
        /// <param name="checkQuantity"> Optional parameter to get only products with remaining quantity</param>
        /// <param name="category"> Optional parameter to get only products of specific category</param>
        /// <returns></returns>
        // GET: api/Products/Random
        [HttpGet("Random", Name = "GetRandomProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProductDto>>> GetRandomProducts(int length, bool checkQuantity = false, Category? category = null)
        {
            var products = await _productRepository.GetRandomProducts(length, checkQuantity, category);
            if (!products.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

        /// <summary>
        /// Get a product rate by product's id and customer's id
        /// </summary>
        /// <param name="productId"> The product's id</param>
        /// <param name="customerId"> The customer's id</param>
        /// <returns></returns>
        // GET api/Products/Rate
        [HttpGet("Rate", Name = "GetRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductRatesDto>> GetRate(int productId, int customerId)
        {
            var productExists = await _productRepository.CheckIfExists(productId);
            var customerExists = await _customerRepository.CheckIfExists(customerId);

            if (!productExists || !customerExists)
            {
                return NotFound();
            }

            var rate = await _productRepository.GetRate(productId, customerId);

            if (rate == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductRatesDto>(rate));
        }

        /// <summary>
        /// Get a product rates by product's id
        /// </summary>
        /// <param name="productId"> The product's id</param>
        /// <returns></returns>
        // GET api/Products/Rates/productId
        [HttpGet("Rates/{productId}", Name = "GetRatesByProductId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProductRatesDto>>> GetRatesByProductId(int productId)
        {
            var productExists = await _productRepository.CheckIfExists(productId);

            if (!productExists)
            {
                return NotFound();
            }

            var rates = await _productRepository.GetRatesByProductId(productId);

            if (rates == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ProductRatesDto>>(rates));
        }

        /// <summary>
        /// Create a product rate
        /// </summary>
        /// <param name="rateDto"> An instance of a product rate</param>
        /// <returns></returns>
        // POST api/Products/Rate
        [HttpPost("Rate", Name = "PostProductRate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductRatesDto>> PostProductRate(ProductRatesDto rateDto)
        {
            var rate = _mapper.Map<ProductRates>(rateDto);
            rate = await _productRepository.PostRate(rate);
            return Ok(_mapper.Map(rate, rateDto));
        }

        /// <summary>
        /// Update a product rate
        /// </summary>
        /// <param name="id"> Product rate id</param>
        /// <param name="rateDto"> An instance of a product rate</param>
        /// <returns></returns>
        // Put api/Products/Rate/Id
        [HttpPut("Rate/{id}", Name = "PutProductRate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutProductRate(int id, ProductRatesDto rateDto)
        {
            var rateInDb = await _productRepository.GetRateById(id);

            if (rateInDb == null || id != rateDto.Id)
            {
                return NotFound();
            }

            _mapper.Map(rateDto, rateInDb);
            await _productRepository.PutRate(rateInDb);
            return NoContent();
        }
    }
}
