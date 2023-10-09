using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anbaedari_Exam.DbContexts;
using Anbaedari_Exam.Models;

namespace Anbaedari_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryContext _context;

        public ProductsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductfeaturesDto>>> GetProductfeaturesDtos()
        {
          if (_context.ProductfeaturesDtos == null)
          {
              return NotFound();
          }
            return await _context.ProductfeaturesDtos.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductfeaturesDto>> GetProductfeaturesDto(int id)
        {
          if (_context.ProductfeaturesDtos == null)
          {
              return NotFound();
          }
            var productfeaturesDto = await _context.ProductfeaturesDtos.FindAsync(id);

            if (productfeaturesDto == null)
            {
                return NotFound();
            }

            return productfeaturesDto;
        }

        // PUT: api/Products/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductfeaturesDto(int id, ProductfeaturesDto productfeaturesDto)
        {
            if (id != productfeaturesDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(productfeaturesDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductfeaturesDtoExists(id))
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
   
        [HttpPost]
        public async Task<ActionResult<ProductfeaturesDto>> PostProductfeaturesDto(ProductfeaturesDto productfeaturesDto)
        {
          if (_context.ProductfeaturesDtos == null)
          {
              return Problem("Entity set 'InventoryContext.ProductfeaturesDtos'  is null.");
          }
            _context.ProductfeaturesDtos.Add(productfeaturesDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductfeaturesDto", new { id = productfeaturesDto.Id }, productfeaturesDto);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductfeaturesDto(int id)
        {
            if (_context.ProductfeaturesDtos == null)
            {
                return NotFound();
            }
            var productfeaturesDto = await _context.ProductfeaturesDtos.FindAsync(id);
            if (productfeaturesDto == null)
            {
                return NotFound();
            }

            _context.ProductfeaturesDtos.Remove(productfeaturesDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductfeaturesDtoExists(int id)
        {
            return (_context.ProductfeaturesDtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
