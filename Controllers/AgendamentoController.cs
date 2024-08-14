using Barbearia.APi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
     
    public class AgendamentoController : ControllerBase
    {
        private readonly AplicationDbContext _dbContext;
        public AgendamentoController(AplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult>Get()
        {
            var agendmentos = await _dbContext.Agendamentos.ToListAsync();
            return Ok(agendmentos);
        }

        [HttpGet("[id:int]")]
        public async Task<ActionResult<Agendamento>>GetById(int id)
        {
            var agendamento = await _dbContext.Agendamentos.FindAsync(id);
            if(agendamento == null){
                return NotFound();
            }
            return Ok(agendamento);
        }

        
    }
}