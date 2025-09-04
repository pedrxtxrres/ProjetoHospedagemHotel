using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHospedagemHotel.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public List<Suite> Suite { get; set; }
        public int DiasReservados { get; set; }


        public void PausarTela()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        public void CadastrarHospedes(Pessoa pessoa)
        {
            Hospedes.Add(pessoa);
            Console.WriteLine($"{pessoa.Nome} {pessoa.Sobrenome} adicionado com sucesso!");
            PausarTela();
        }
        public void CadastrarSuite(Suite suite)
        {
            Suite.Add(suite);
            Console.WriteLine($"{suite.NomeDaSuite} adicionada com sucesso!");
            PausarTela();
        }
        public int ObterQuantidadeHospedes()
        {
            int QuantidadeDeHospedes = Hospedes.Count;
            return QuantidadeDeHospedes;
        }
        public decimal CalcularValorDiaria()
        {
            return 0;
        }
    }
}