using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anbaedari_Exam.DbContexts;
using Anbaedari_Exam.Models;
using Microsoft.AspNetCore.Authorization;

namespace Anbaedari_Exam.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly InventoryContext _context;

        public GroupsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product_group_characteristicsDto>>> GetProduct_Group_CharacteristicsDtos(bool? inStock, int? skip, int? take)
        {
            var product_Group_Characteristics = _context.Product_Group_CharacteristicsDtos.AsQueryable();

            if (inStock != null) // mojodi check kardan
            {
              //  product_Group_Characteristics = _context.Product_Group_CharacteristicsDtos.Where(p => p.AvailableQuanty > 0);
            }
            //چرا دلیگیت نمیگیره و نمیشناسه؟؟؟؟؟؟؟؟؟؟؟؟مثل کد پایین
           // _context.Products.Where(product => product.Orders.Where(order => order.Confirmed).Sum(order => order.Quantity) < product.AvailableQuantity)

            if (skip != null)
            {
                product_Group_Characteristics = product_Group_Characteristics.Skip((int)skip);
            }

            if (take != null)
            {
                product_Group_Characteristics = product_Group_Characteristics.Take((int)take);
            }

            return await product_Group_Characteristics.ToListAsync();
            //if (_context.Product_Group_CharacteristicsDtos == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Product_Group_CharacteristicsDtos.ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product_group_characteristicsDto>> GetProduct_group_characteristicsDto(int id)
        {
          if (_context.Product_Group_CharacteristicsDtos == null)
          {
              return NotFound();
          }
            var product_group_characteristicsDto = await _context.Product_Group_CharacteristicsDtos.FindAsync(id);

            if (product_group_characteristicsDto == null)
            {
                return NotFound();
            }

            return product_group_characteristicsDto;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct_group_characteristicsDto(int id, Product_group_characteristicsDto product_group_characteristicsDto)
        {
            if (id != product_group_characteristicsDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(product_group_characteristicsDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_group_characteristicsDtoExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product_group_characteristicsDto>> PostProduct_group_characteristicsDto(Product_group_characteristicsDto product_group_characteristicsDto)
        {
          if (_context.Product_Group_CharacteristicsDtos == null)
          {
              return Problem("Entity set 'InventoryContext.Product_Group_CharacteristicsDtos'  is null.");
          }
            _context.Product_Group_CharacteristicsDtos.Add(product_group_characteristicsDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct_group_characteristicsDto", new { id = product_group_characteristicsDto.Id }, product_group_characteristicsDto);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct_group_characteristicsDto(int id)
        {
            if (_context.Product_Group_CharacteristicsDtos == null)
            {
                return NotFound();
            }
            var product_group_characteristicsDto = await _context.Product_Group_CharacteristicsDtos.FindAsync(id);
            if (product_group_characteristicsDto == null)
            {
                return NotFound();
            }

            _context.Product_Group_CharacteristicsDtos.Remove(product_group_characteristicsDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Product_group_characteristicsDtoExists(int id)
        {
            return (_context.Product_Group_CharacteristicsDtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
