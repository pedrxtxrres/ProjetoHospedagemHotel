using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHospedagemHotel.Models
{
    public class Hotel
    {
        public string NomeHotel { get; set; }
        public List<Pessoa> HospedesHotel { get; set; } = new List<Pessoa>();
        public List<Suite> SuitesHotel { get; set; } = new List<Suite>();
        public List<Reserva> ReservasHotel { get; set; } = new List<Reserva>();

        public void PausarTela()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        public void AdicionarNomeHotel()
        {
            Console.WriteLine("Bem vindo! Adicione o nome do hotel:");
            NomeHotel = Console.ReadLine();

            Console.WriteLine($"Hotel {NomeHotel} adicionado com sucesso!");
            PausarTela();
        }
        public void AdicionarHospedes(Pessoa pessoa)
        {
            HospedesHotel.Add(pessoa);
            Console.WriteLine($"{pessoa.Nome} {pessoa.Sobrenome} adicionado com sucesso!");
            PausarTela();
        }
        public void AdicionarSuite(Suite suite)
        {
            SuitesHotel.Add(suite);
            Console.WriteLine($"{suite.NomeDaSuite} adicionada com sucesso!");
            PausarTela();
        }
        private bool HaConflitoDeDatas(DateTime dataInicialDaNovaReserva, DateTime dataFinalDaNovaReserva, DateTime reservaDataInicial, DateTime reservaDataFinal)
        {
            return dataInicialDaNovaReserva < reservaDataFinal && dataFinalDaNovaReserva > reservaDataInicial;
        }
        private bool VerificarDisponibilidadeSuite(int idSuite, DateTime dataInicial, DateTime dataFinal)
        {
            List<Reserva> reservasDaSuite = ReservasHotel.Where(reservas => reservas.IdDaSuite == idSuite).ToList();

            foreach (Reserva reserva in reservasDaSuite)
            {
                if (HaConflitoDeDatas(dataInicial, dataFinal, reserva.DataInicialDaReserva, reserva.DataFinalDaReserva))
                {
                    return false;
                }
            }

            return true;
        }
        private int? BuscarSuiteDisponivel(int tipoDaSuite, DateTime dataInicial, DateTime dataFinal)
        {
            List<Suite> suitesDoTipoInformado = SuitesHotel.Where(suites => suites.TipoSuite == tipoDaSuite).ToList();

            if (!suitesDoTipoInformado.Any())
            {
                return null;
            }

            foreach (Suite suite in suitesDoTipoInformado)
            {
                bool suiteDisponivel = VerificarDisponibilidadeSuite(suite.IdDaSuite, dataInicial, dataFinal);

                if (suiteDisponivel)
                {
                    return suite.IdDaSuite;
                }
            }

            return null;
        }
        private decimal CalcularValorReserva(int idSuite, DateTime dataInicial, DateTime dataFinal)
        {
            Suite suite = SuitesHotel.FirstOrDefault(suites => suites.IdDaSuite == idSuite);
            
            int quantidadeDias = (dataFinal - dataInicial).Days;
            if (quantidadeDias >= 10)
            {
                return (suite.ValorDiaria * quantidadeDias) * 0.9M;
            }
            else
            {
                return suite.ValorDiaria * quantidadeDias;
            }
        }
        public void AdicionarReserva(List<Pessoa> hospedes, int quantidadeDeHospedes, long cpfResponsavel, int tipoDaSuite, DateTime dataInicialDaReserva, DateTime dataFinalDaReserva)
        {
            int? idSuiteDisponivel = BuscarSuiteDisponivel(tipoDaSuite, dataInicialDaReserva, dataFinalDaReserva);

            if (idSuiteDisponivel == null)
            {
                Console.WriteLine($"Não há suítes do tipo {tipoDaSuite} disponíveis para o período solicitado.");
                PausarTela();
                return;
            }

            decimal valorFinalDaReserva = CalcularValorReserva(idSuiteDisponivel.Value, dataInicialDaReserva, dataFinalDaReserva);

            Reserva reserva = new Reserva(cpfResponsavel, hospedes, quantidadeDeHospedes, idSuiteDisponivel.Value, dataInicialDaReserva, dataFinalDaReserva, valorFinalDaReserva);
            ReservasHotel.Add(reserva);
        }
        public void HistoricoReservas(long cpf)
        {
            List<Reserva> historicoReservasPorCPF = ReservasHotel.Where(reservas => reservas.CpfResponsavel == cpf).ToList();

            if (!historicoReservasPorCPF.Any())
            {
                Console.WriteLine("Nenhuma reserva encontrada para este CPF.");
                return;
            }

            Pessoa hospede = HospedesHotel.FirstOrDefault(hospede => hospede.Cpf == cpf);
            string nomeResponsavel = "";

            if (hospede != null)
            {
                string nomeHospedeResponsavel = hospede.Nome;
                string sobrenomeHospedeResponsavel = hospede.Sobrenome;
                nomeResponsavel = $"{nomeHospedeResponsavel} {sobrenomeHospedeResponsavel}";
            }
            else
            {
                Console.WriteLine("Hóspede não encontrado!");
                PausarTela();
            }
            
            int contador = 1;
            Console.WriteLine($"=== Histórico de Reservas - {nomeResponsavel} ===\n");

            foreach (Reserva reserva in historicoReservasPorCPF)
            {

                string nomeSuite = SuitesHotel.Where(suites => suites.IdDaSuite == reserva.IdDaSuite).Select(suites => suites.NomeDaSuite).FirstOrDefault();

                Console.WriteLine($"{contador} - Reserva no nome do {nomeResponsavel}, na suíte {nomeSuite}," +
                $" do dia {reserva.DataInicialDaReserva.ToString("dd/MM/yyyy")} até o dia {reserva.DataFinalDaReserva.ToString("dd/MM/yyyy")} no valor de {reserva.ValorFinalDaReserva}");
                contador++;
            }
            PausarTela();
        }
        public int ObterQuantidadeHospedes()
        {
            int quantidadeDeHospedes = HospedesHotel.Count;
            return quantidadeDeHospedes;
        }
        public string BuscarHospedePeloCpf(long cpf)
        {
            Pessoa hospede = HospedesHotel.FirstOrDefault(hospede => hospede.Cpf == cpf);
            string nomeResponsavel = "";

            if (hospede != null)
            {
                string nomeHospedeResponsavel = hospede.Nome;
                string sobrenomeHospedeResponsavel = hospede.Sobrenome;
                nomeResponsavel = $"{nomeHospedeResponsavel} {sobrenomeHospedeResponsavel}";
                return nomeResponsavel;
            }
            else
            {
                return "Hóspede não encontrado!";
            }

        }
    }
}