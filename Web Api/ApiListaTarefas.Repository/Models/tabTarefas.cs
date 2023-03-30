using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ApiListaTarefas.Repository.Models
{
    public class tabTarefas
    {
        public int id { get; set; }

        public string tarefa { get; set; }
    }
}
