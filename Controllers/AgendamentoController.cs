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
            var agendamentos = await _dbContext
                                    .Agendamentos
                                    .Include(a=> a.Cliente)
                                    .Include(a=> a.Servicos)
                                    .ToListAsync();
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
        public async Task<IActionResult>Create([FromBody] AgendaentoDto agendamentoDto)
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
            if(ModelState.IsValid){
                Agendamento agendamento = new Agendamento();
                agendamento.ClienteId = agendamentoDto.ClenteId;
                agendamento.Observacoes = agendamentoDto.Observacoes;
                agendamento.DataHora = agendamentoDto.DataHora;
                agendamento.Servicos = new List<Servico>();
                agendamento.Cliente = _dbContext.Clientes.Find(agendamento.ClienteId);

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
            return BadRequest();
          
        }

         [HttpPut("{id:int}")]
        public async Task<IActionResult>Edit(int id, Agendamento agendamentoDto)
        {
            if(id != agendamentoDto.AgendamentoID)
            {
                return BadRequest();
            }
            if(ModelState.IsValid)
            {
                var agendamento = _dbContext.Agendamentos.Find(agendamentoDto.AgendamentoID);
                if(agendamento == null){
                    return NotFound();
                }

                agendamento.ClienteId = agendamentoDto.ClienteId;
                agendamento.Observacoes = agendamentoDto.Observacoes;
                agendamento.DataHora = agendamentoDto.DataHora;

                agendamento.Servicos = new List<Servico>();
                agendamento.Cliente = _dbContext.Clientes.Find(agendamento.ClienteId);

                  foreach (var item in agendamentoDto.Servicos)
                {
                     var servico = await _dbContext.Servicos.FindAsync(item.Id);
                    if (servico == null)
                    {
                         return BadRequest($"Serviço com ID {item.Id} não encontrado.");
                    }

                agendamento.Servicos.Add(servico);
            }

                _dbContext.Agendamentos.Update(agendamento);
                await _dbContext.SaveChangesAsync();
                return Ok(agendamento);
            }
            return BadRequest();
            
        }

         [HttpDelete("{id:int}")]
        public async Task<IActionResult>Delete(int id)
        {
            var agendamento = await _dbContext.Agendamentos.FindAsync(id);
            if(agendamento == null)
            {
                return NotFound();
            }
            _dbContext.Agendamentos.Remove(agendamento);
            await _dbContext.SaveChangesAsync();
            return Ok(agendamento);
        }

    }
}
