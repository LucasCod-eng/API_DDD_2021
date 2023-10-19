using Entidades.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Configuracoes
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto (DbContextOptions<Contexto> opcoes) : base(opcoes) 
        {
        }

        public DbSet<Noticia> Noticias { get; set; }    
        public DbSet<ApplicationUser> Applicaciones { get;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)//Ele verifica se houve uam coniguracao no sql
            {     
            optionsBuilder.UseSqlServer(ObterStringConexao());//Aqui ele usa a Metodo de conexao que criei no banco 
            base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        public string ObterStringConexao()
        {
            string strcon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=API_DDD_2021";
            return strcon;
        }
    }   
}
