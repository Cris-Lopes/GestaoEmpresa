using GestaoEmpresa.Data;
using GestaoEmpresa.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace GestaoEmpresa.Tests
{
    public class EmpresaContextTests
    {
        private static DbContextOptions<EmpresaContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<EmpresaContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public void CanAddArtigoToDatabase()
        {
            var options = CreateInMemoryOptions();

            using (var context = new EmpresaContext(options))
            {
                var artigo = new Artigo
                {
                    Referencia = "ART123",
                    Descricao = "Artigo de Teste",
                    ValorSemIVA = 100m,
                    Desconto = 10m,
                    IVA = 23m
                };

                context.Artigos.Add(artigo);
                context.SaveChanges();
            }

            using (var context = new EmpresaContext(options))
            {
                var artigos = context.Artigos.ToList();
                Assert.Single(artigos);
                Assert.Equal("ART123", artigos[0].Referencia);
            }
        }
    }
}