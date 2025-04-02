using Microsoft.EntityFrameworkCore;
using SCH.Models;
using SCH.ViewModels;

namespace SCH.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        // Adicionar o DbSet para armazenar os resultados da consulta
        public DbSet<RelatorioDemonstrativoViewModel> RelatorioDemonstrativo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar o DTO como uma entidade sem chave, pois é apenas para leitura
            modelBuilder.Entity<RelatorioDemonstrativoViewModel>().HasNoKey();
        }
    }
}
