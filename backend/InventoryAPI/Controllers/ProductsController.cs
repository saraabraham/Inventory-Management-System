using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Models;
using InventoryAPI.DTOs;
using InventoryAPI.Repositories;
using System.Text;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                MinimumStock = p.MinimumStock,
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.Name,
                Location = p.Location,
                IsLowStock = p.StockQuantity <= p.MinimumStock,
                IsActive = p.IsActive
            });

            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                MinimumStock = product.MinimumStock,
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier?.Name,
                Location = product.Location,
                IsLowStock = product.StockQuantity <= product.MinimumStock,
                IsActive = product.IsActive
            };

            return Ok(productDto);
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetLowStockProducts()
        {
            var products = await _productRepository.GetLowStockProductsAsync();
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                MinimumStock = p.MinimumStock,
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.Name,
                Location = p.Location,
                IsLowStock = true,
                IsActive = p.IsActive
            });

            return Ok(productDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createDto)
        {
            // Check if SKU already exists
            var existing = await _productRepository.GetBySkuAsync(createDto.SKU);
            if (existing != null)
            {
                return BadRequest("A product with this SKU already exists.");
            }

            var product = new Product
            {
                SKU = createDto.SKU,
                Name = createDto.Name,
                Description = createDto.Description,
                Category = createDto.Category,
                Price = createDto.Price,
                StockQuantity = createDto.StockQuantity,
                MinimumStock = createDto.MinimumStock,
                SupplierId = createDto.SupplierId,
                Location = createDto.Location
            };

            var created = await _productRepository.CreateAsync(product);
            var createdProduct = await _productRepository.GetByIdAsync(created.Id);

            var productDto = new ProductDto
            {
                Id = createdProduct!.Id,
                SKU = createdProduct.SKU,
                Name = createdProduct.Name,
                Description = createdProduct.Description,
                Category = createdProduct.Category,
                Price = createdProduct.Price,
                StockQuantity = createdProduct.StockQuantity,
                MinimumStock = createdProduct.MinimumStock,
                SupplierId = createdProduct.SupplierId,
                SupplierName = createdProduct.Supplier?.Name,
                Location = createdProduct.Location,
                IsLowStock = createdProduct.StockQuantity <= createdProduct.MinimumStock,
                IsActive = createdProduct.IsActive
            };

            return CreatedAtAction(nameof(GetProduct), new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (updateDto.Name != null) product.Name = updateDto.Name;
            if (updateDto.Description != null) product.Description = updateDto.Description;
            if (updateDto.Category != null) product.Category = updateDto.Category;
            if (updateDto.Price.HasValue) product.Price = updateDto.Price.Value;
            if (updateDto.MinimumStock.HasValue) product.MinimumStock = updateDto.MinimumStock.Value;
            if (updateDto.SupplierId.HasValue) product.SupplierId = updateDto.SupplierId.Value;
            if (updateDto.Location != null) product.Location = updateDto.Location;
            if (updateDto.IsActive.HasValue) product.IsActive = updateDto.IsActive.Value;

            await _productRepository.UpdateAsync(product);

            return NoContent();
        }

        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] int quantityChange)
        {
            var success = await _productRepository.UpdateStockAsync(id, quantityChange);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportProducts()
        {
            var products = await _productRepository.GetAllAsync();

            var csv = new StringBuilder();
            csv.AppendLine("SKU,Name,Category,Price,Stock,Minimum Stock,Supplier,Location,Status");

            foreach (var product in products)
            {
                csv.AppendLine($"{product.SKU},{product.Name},{product.Category},{product.Price}," +
                              $"{product.StockQuantity},{product.MinimumStock},{product.Supplier?.Name}," +
                              $"{product.Location},{(product.IsActive ? "Active" : "Inactive")}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"inventory-export-{DateTime.Now:yyyyMMdd}.csv");
        }
    }
}