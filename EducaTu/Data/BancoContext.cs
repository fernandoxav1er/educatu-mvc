using EducaTu.Models;
using Microsoft.EntityFrameworkCore;

namespace EducaTu.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) 
        {

        }
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<PlanoModel> Planos { get; set; }
        public DbSet<UsuarioPlano> UsuarioPlano { get; set; }
    }
}
