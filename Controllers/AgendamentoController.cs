using Barbearia.APi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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

        [HttpPost]
        public async Task<IActionResult>Create([FromBody] AgendaentoDto agendaentoDto)
        {
            if(ModelState.IsValid){
                Agendamento agendamento = new Agendamento();
                agendamento.AgendamentoID = agendaentoDto.AgendamentoID;
                agendamento.DataHora = agendaentoDto.DataHora;
                agendamento.Observacoes = agendaentoDto.Observacoes;
                agendamento.Status = agendaentoDto.Status;

                agendamento.Servicos = new List<Servico>();
                agendamento.Cliente = _dbContext.Clientes.Find(agendamento.ClienteId);

                foreach( var item in agendaentoDto.Servicos){
                    var servico = _dbContext.Servicos.Find(item.Id);
                    if(servico == null){
                        return BadRequest();
                    }

                    agendamento.Servicos.Add(servico);
                }

                _dbContext.Agendamentos.Add(agendamento);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new{id= agendamento.AgendamentoID},agendamento);
            }
            return BadRequest();
        }
    }
}