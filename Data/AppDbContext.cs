using ApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ProdutoModel> Produtos { get; set; }
    }
}
