using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiPessoa.Repository.Models;

namespace WebApiPessoa.Repository
{
    public class PessoaContext : DbContext
    {
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options) { }

        public DbSet<tabUsuario> Usuarios { get; set; }

        public DbSet<tabPessoa> Pessoa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tabUsuario>().ToTable("tabUsuario");
            modelBuilder.Entity<tabPessoa>().ToTable("tabPessoa");
        }
    }
}
