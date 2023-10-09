using Anbaedari_Exam.DbContexts;
using Anbaedari_Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anbaedari_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Afzayesh_Va_Kahesh_MojodiController : ControllerBase
    {
        private readonly InventoryContext _context;
        public Afzayesh_Va_Kahesh_MojodiController(InventoryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<OrderDto>> Receipt()
        {
            if (_context.OrderDtos == null)
            {
                return (IEnumerable<OrderDto>)NotFound();
            }
            return await _context.OrderDtos.ToListAsync();
        }
        [HttpGet]
        public async Task<IEnumerable<OrderDto>> Transfer()
        {
            if (_context.OrderDtos == null)
            {
                return (IEnumerable<OrderDto>)NotFound();
            }
            return await _context.OrderDtos.ToListAsync();
        }


    }
}
