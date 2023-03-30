using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiPessoa.Repository;
using WebApiPessoa.Repository.Models;

namespace WebApiPessoaApplication.Pessoa
{
    public class PessoaService 
    {
        private readonly PessoaContext _context;
        public PessoaService(PessoaContext context)
        {
            _context = context;
        }

        public bool  RemoverId(int id)
        {
                try
                {
                    var pessoaDb = _context.Pessoa.FirstOrDefault(x => x.id == id);
                    if (pessoaDb == null)
                        return false;

                    _context.Pessoa.Remove(pessoaDb);
                    _context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }          
        }

        public PessoaHistoricoResponse ObterHistoricoPessoa(int id)
        {
            var pessoaDb = _context.Pessoa.FirstOrDefault(x => x.id == id);
            var pessoa = new PessoaHistoricoResponse()
            {
                Aliquota = Convert.ToDouble(pessoaDb.aliquota),
                Altura = pessoaDb.altura,
                Classificacao = pessoaDb.classificacao,
                DataNascimento = Convert.ToDateTime(pessoaDb.dataNascimento),
                id = pessoaDb.id,
                Idade = pessoaDb.idade,
                IdUsuario = pessoaDb.idUsuario,
                Imc = pessoaDb.imc,
                Inss = Convert.ToDouble(pessoaDb.inss),
                nome = pessoaDb.nome,
                Peso = pessoaDb.peso,
                Salario = Convert.ToDouble(pessoaDb.salario),
                SalarioLiquido = Convert.ToDouble(pessoaDb.salarioLiquido),
                Saldo = pessoaDb.saldo,
                SaldoDolar = pessoaDb.saldoDolar,
            };

            return pessoa;
        }

        public List<PessoaHistoricoResponse> ObterHistoricoPessoas()
        {
            var pessoasDb = _context.Pessoa.ToList();
            var pessoas = new List<PessoaHistoricoResponse>();

            foreach (var item in pessoasDb)
            {
                pessoas.Add(new PessoaHistoricoResponse()
                {
                    Aliquota = Convert.ToDouble(item.aliquota),
                    Altura = item.altura,
                    Classificacao = item.classificacao,
                    DataNascimento = Convert.ToDateTime(item.dataNascimento),
                    id = item.id,
                    Idade = item.idade,
                    IdUsuario = item.idUsuario,
                    Imc = item.imc,
                    Inss = Convert.ToDouble(item.inss),
                    nome = item.nome,
                    Peso = item.peso,
                    Salario = Convert.ToDouble( item.salario),
                    SalarioLiquido = Convert.ToDouble(item.salarioLiquido),
                    Saldo = item.saldo,
                    SaldoDolar  = item.saldoDolar,

                }); 
            }
            return pessoas;
        }
        public PessoaResponse ProcessarInformacoesPessoa (PessoaRequest request, int usuarioId)
        {
            var idade = CalcularIdade(request.DataNascimento);

            var imc = CalculaImc(request.Peso, request.Altura);

            var classificacao = CalcularClassificacao(imc);

            var aliquota = CalculaAliquota(request.Salario);

            var inss = CalcularInss(request.Salario, aliquota);

            var salarioLiquido = CalcularSalarioLiquido(request.Salario, inss);

            var saldoDolar = CalcularDolar(request.Saldo);

            var resposta = new PessoaResponse();
            resposta.SaldoDolar = saldoDolar;
            resposta.Aliquota = aliquota;
            resposta.SalarioLiquido = salarioLiquido;
            resposta.Classificacao = classificacao;
            resposta.Idade = idade;
            resposta.Imc = imc;
            resposta.Inss = inss;
            resposta.Nome = request.nome;

            var pessoa = new tabPessoa()
            {
                aliquota = Convert.ToDecimal( aliquota),
                altura = request.Altura,
                classificacao = classificacao,
                dataNascimento = request.DataNascimento,
                idade = idade,
                idUsuario = usuarioId,
                imc = imc ,
                inss = Convert.ToDecimal( inss),
                nome = request.nome,
                peso = request.Peso,
                salario = Convert.ToDecimal( request.Salario),
                salarioLiquido = Convert.ToDecimal( salarioLiquido),
                saldo=request.Saldo,
                saldoDolar = saldoDolar,   
            };

            _context.Pessoa.Add(pessoa);
            _context.SaveChanges();

            return resposta;
        }
        private int CalcularIdade(DateTime DataNascimento)
        {
            var anoAtual = DateTime.Now.Year;
            var idade = anoAtual - DataNascimento.Year;

            var mesAtual = DateTime.Now.Month;

            if (mesAtual < DataNascimento.Month)
            {
                idade = idade - 1;

            }


            return idade;
        }

        private decimal CalculaImc(decimal peso, decimal altura)
        {
            var imc = Math.Round(peso / (altura * altura), 2);
            return imc;

        }

        private string CalcularClassificacao(decimal imc)
        {
            var classificacao = "";
            if (imc < (decimal)18.5)
            {
                classificacao = " Abaixo do peso ideal";
            }
            else if (imc >= (decimal)18.5 && imc <= (decimal)24.99)
            {
                classificacao = " Peso Normal";
            }

            else if (imc >= (decimal)25 && imc <= (decimal)29.99)
            {
                classificacao = " Pré-Obesidade";
            }

            else if (imc >= (decimal)30 && imc <= (decimal)34.99)

            {
                classificacao = " Obesidade Grau I";
            }

            else if (imc >= (decimal)35 && imc <= (decimal)39.99)
            {
                classificacao = " Obesidade Grau II";
            }

            else
            {
                classificacao = " Obesidade Grau III";
            }

            return classificacao;
        }

        private double CalculaAliquota(double Salario)
        {
            var aliquota = 7.5;
            if (Salario <= 1212)
            {
                aliquota = 7.5;
            }

            else if (Salario >= 1212.01 && Salario <= 2427.35)
            {
                aliquota = 9;
            }

            else if (Salario >= 2427.36 && Salario <= 3641.03)
            {
                aliquota = 12;
            }

            else
            {
                aliquota = 14;
            }

            return aliquota;
        }

        private double CalcularInss(double Salario, double aliquota)
        {
            var inss = (Salario * aliquota) / 100;
            return inss;
        }

        private double CalcularSalarioLiquido(double salario, double inss)
        {
            return salario - inss;
        }

        private decimal CalcularDolar(decimal Saldo)
        {
            var dolar = (decimal)5.14;
            var saldoDolar = Math.Round(Saldo / dolar, 2);

            return saldoDolar;
        }
    }
}
