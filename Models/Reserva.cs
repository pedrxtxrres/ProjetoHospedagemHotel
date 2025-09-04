using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHospedagemHotel.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        List<Pessoa> ListaHospedes = new List<Pessoa>();
        List<Suite> ListaSuites = new List<Suite>();

        public void PausarTela()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        public void CadastrarHospedes(Pessoa pessoa)
        {
            ListaHospedes.Add(pessoa);
            Console.WriteLine($"{pessoa.Nome} {pessoa.Sobrenome} adicionado com sucesso!");
            PausarTela();
        }
        public void CadastrarSuite(Suite suite)
        {
            ListaSuites.Add(suite);
            Console.WriteLine($"{suite.NomeDaSuite} adicionada com sucesso!");
            PausarTela();
        }
        public int ObterQuantidadeHospedes()
        {
            return 0;
        }
        public decimal CalcularValorDiaria()
        {
            return 0;
        }
    }
}