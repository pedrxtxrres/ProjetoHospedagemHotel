using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHospedagemHotel.Models
{
    public class Reserva
    {
        public Reserva(long cpfResponsavel, List<Pessoa> hospedes, int quantidadeDeHospedes, int idDaSuite, DateTime dataInicialDaReserva, DateTime dataFinalDaReserva, decimal valorFinalDaReserva)
        {
            CpfResponsavel = cpfResponsavel;
            Hospedes = hospedes;
            QuantidadeDeHospedes = quantidadeDeHospedes;
            IdDaSuite = idDaSuite;
            DataInicialDaReserva = dataInicialDaReserva;
            DataFinalDaReserva = dataFinalDaReserva;
            ValorFinalDaReserva = valorFinalDaReserva;
        }
        public long CpfResponsavel { get; set; }
        public List<Pessoa> Hospedes { get; set; }
        public int QuantidadeDeHospedes { get; set; }
        public int IdDaSuite { get; set; }
        public DateTime DataInicialDaReserva { get; set; }
        public DateTime DataFinalDaReserva { get; set; }
        public decimal ValorFinalDaReserva { get; set; }
    }
}