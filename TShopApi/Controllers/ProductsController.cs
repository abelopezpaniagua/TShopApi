using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TShopApi.Filters;
using TShopApi.Helpers;
using TShopApi.Models;
using TShopApi.Services;
using TShopApi.Wrappers;

namespace TShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TShopDBContext _context;
        private readonly IUriService _uriService;

        public ProductsController(TShopDBContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string filter, [FromQuery] PaginationFilter paginationFilter, [FromQuery] SortFilter sortFilter)
        {
            var route = Request.Path.Value;
            var paginator = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            var sorter = new SortFilter(sortFilter.SortColumn == null ? "code" : sortFilter.SortColumn, sortFilter.SortOrder);

            var filteredData = _context.Products
                .Where(p => !string.IsNullOrEmpty(filter) ? (p.Code.Contains(filter) || p.Name.Contains(filter)) : true)
                .AsQueryable()
                .OrderByPropertyName(sorter.SortColumn, sorter.SortOrder == "asc");

            var retrievedData = await filteredData
                .Skip((paginator.PageNumber - 1) * paginator.PageSize)
                .Take(paginator.PageSize)
                .Include(p => p.Category)
                .ToListAsync();
            
            var totalRecords = await filteredData.CountAsync();

            var pagedResponse = PaginationHelper.CreatePagedResponse<Product>(retrievedData, paginator, totalRecords, _uriService, route);

            return Ok(pagedResponse);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new Response<Product>(product));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, new Response<Product>(product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
