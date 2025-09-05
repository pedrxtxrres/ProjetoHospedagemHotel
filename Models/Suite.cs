using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHospedagemHotel.Models
{
    public class Suite
    {
        public Suite(int idDaSuite, string nomeDaSuite, int tipoSuite, int capacidade, decimal valorDiaria)
        {
            IdDaSuite = idDaSuite;
            NomeDaSuite = nomeDaSuite;
            TipoSuite = tipoSuite;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
        }
        public int IdDaSuite { get; set; }
        public string NomeDaSuite { get; set; }
        public int TipoSuite { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }

    }
}