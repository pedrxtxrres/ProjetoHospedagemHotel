using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHospedagemHotel.Models
{
    public class Suite
    {
        public Suite(string nomeDaSuite, int tipoSuite, int capacidade, decimal valorDiaria)
        {
            NomeDaSuite = nomeDaSuite;
            TipoSuite = tipoSuite;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
        }
        public string NomeDaSuite { get; set; }
        public int TipoSuite { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }

    }
}