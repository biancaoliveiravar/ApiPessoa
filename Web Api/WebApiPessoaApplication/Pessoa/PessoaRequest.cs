using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiPessoaApplication.Pessoa
{
    public class PessoaRequest
    {
        public string nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public decimal Altura { get; set; }

        public decimal Peso { get; set; }

        public double Salario { get; set; }

        public decimal Saldo { get; set; }
    }
}
