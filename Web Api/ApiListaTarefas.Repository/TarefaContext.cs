using System;
using System.Collections.Generic;
using System.Text;
using ApiListaTarefas.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiListaTarefas.Repository
{
    public class TarefaContext : DbContext
    {
      
            public TarefaContext(DbContextOptions<TarefaContext> options) : base(options) { }

            public DbSet<tabTarefas> Tarefas { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<tabTarefas>().ToTable("tabTarefas");
             
            }
        
    }
}
