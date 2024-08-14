
using Barbearia.APi.Dto;
using Barbearia.APi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.APi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly AplicationDbContext _dbContext;

        public ServicoController(AplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var servicos = await _dbContext.Servicos.ToListAsync();
            return Ok(servicos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Servico>> GetById(int id)
        {
            var servico = await _dbContext.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            return Ok(servico);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServicoDto servicoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var servico = new Servico
            {
                NomeServico = servicoDto.NomeServico,
                Descricao = servicoDto.Descricao,
                DuracaoMin = servicoDto.DuracaoMin,
                Preco = servicoDto.Preco
            };

            _dbContext.Servicos.Add(servico);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ServicoDto servicoDto)
        {
            if (id != servicoDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var servico = await _dbContext.Servicos.FindAsync(servicoDto.Id);
            if (servico == null)
            {
                return NotFound();
            }

            servico.NomeServico = servicoDto.NomeServico;
            servico.Descricao = servicoDto.Descricao;
            servico.DuracaoMin = servicoDto.DuracaoMin;
            servico.Preco = servicoDto.Preco;

            _dbContext.Servicos.Update(servico);
            await _dbContext.SaveChangesAsync();

            return Ok(servico);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var servico = await _dbContext.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            _dbContext.Servicos.Remove(servico);
            await _dbContext.SaveChangesAsync();

            return Ok(servico);
        }
    }
}
