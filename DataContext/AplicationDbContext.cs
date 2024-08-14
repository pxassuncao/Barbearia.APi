using Barbearia.APi.Models;
using Barbearia.APi.Models;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.APi;

public class AplicationDbContext : DbContext
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext>options): base(options)
    {
        
    }

    public DbSet<Cliente>Clientes{get; set;}
    public DbSet<Servico>Servicos{get; set;}
    public DbSet<Agendamento>Agendamentos{get; set;}
}
