using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHospedagemHotel.Models
{
    public class Pessoa
    {
        public Pessoa(string nome, string sobrenome, long cpf)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
        }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public long Cpf { get; set; }

    }
}