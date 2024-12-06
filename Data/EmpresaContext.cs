using Microsoft.EntityFrameworkCore;
using GestaoEmpresa.Models;

namespace GestaoEmpresa.Data
{
    public class EmpresaContext(DbContextOptions<EmpresaContext> options) : DbContext(options)
    {
        public DbSet<Artigo> Artigos { get; set; } = null!;
        public DbSet<Fatura> Faturas { get; set; } = null!;
        public DbSet<Fornecedor> Fornecedores { get; set; } = null!;
        public DbSet<ProdutoFatura> ProdutosFatura { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=GestaoEmpresa;Trusted_Connection=True;");
            }
        }
    }
}