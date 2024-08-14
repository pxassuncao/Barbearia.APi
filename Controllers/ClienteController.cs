using Barbearia.APi.Dto;
using Barbearia.APi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Barbearia.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {   
        
        private readonly AplicationDbContext _dbContext;
        
       
        public ClienteController(AplicationDbContext dbContext)
        {
            
            _dbContext = dbContext;
        }
         [HttpGet]
        public async Task<IActionResult>Get()
        {
            var clientes = await _dbContext.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>>GetById(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);

            if(cliente == null){
                return BadRequest();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ClienteDto clienteDto)
        {
            if(ModelState.IsValid)
            {
                Cliente cliente = new Cliente();
                cliente.Nome = clienteDto.Nome;
                cliente.Telefone = clienteDto.Telefone;
                cliente.Email = clienteDto.Email;
                cliente.DataNascimento = clienteDto.DataNascimento;

                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new {id= cliente.ClienteId}, cliente);
                
            }
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult>Edit(int id, ClienteDto clienteDto)
        {
            if(id != clienteDto.ClienteId)
            {
                return BadRequest();
            }
            if(ModelState.IsValid)
            {
                var cliente = _dbContext.Clientes.Find(clienteDto.ClienteId);
                if(cliente == null){
                    return NotFound();
                }

                cliente.Nome = clienteDto.Nome;
                cliente.Email = clienteDto.Email;
                cliente.Telefone = clienteDto.Telefone;
                cliente.DataNascimento = clienteDto.DataNascimento;

                _dbContext.Update(cliente);
                await _dbContext.SaveChangesAsync();
                return Ok(cliente);
            }
            return BadRequest();
            
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult>Delete(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if(cliente == null)
            {
                return NotFound();
            }
            _dbContext.Clientes.Remove(cliente);
            await _dbContext.SaveChangesAsync();
            return Ok(cliente);
        }

    }
}

