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
        public async Task<IActionResult> Get()
        {
            var agendamentos = await _dbContext.Agendamentos.ToListAsync();
            return Ok(agendamentos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Agendamento>> GetById(int id)
        {
            var agendamento = await _dbContext.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            return Ok(agendamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AgendaentoDto agendamentoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _dbContext.Clientes.FindAsync(agendamentoDto.AgendamentoID);
            if (cliente == null)
            {
                return BadRequest("Cliente não encontrado.");
            }

            var agendamento = new Agendamento
            {
                AgendamentoID = agendamentoDto.AgendamentoID,
                DataHora = agendamentoDto.DataHora,
                Observacoes = agendamentoDto.Observacoes,
                Status = agendamentoDto.Status,
                Cliente = cliente,
                Servicos = new List<Servico>()
            };

            foreach (var item in agendamentoDto.Servicos)
            {
                var servico = await _dbContext.Servicos.FindAsync(item.Id);
                if (servico == null)
                {
                    return BadRequest($"Serviço com ID {item.Id} não encontrado.");
                }

                agendamento.Servicos.Add(servico);
            }

            _dbContext.Agendamentos.Add(agendamento);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = agendamento.AgendamentoID }, agendamento);
        }
    }
}
