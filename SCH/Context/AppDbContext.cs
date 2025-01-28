using Microsoft.EntityFrameworkCore;
using SCH.Models;

namespace SCH.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
    }
}
