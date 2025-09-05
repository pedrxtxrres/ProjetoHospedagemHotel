using Newtonsoft.Json;
using ProjetoHospedagemHotel.Models;
using System.Globalization;
using System.Linq;

string opcao;
bool exibirMenu = true;
Hotel hotel = new Hotel();


string diretorioRaiz = Directory.GetCurrentDirectory();
string pastaDados = Path.Combine(diretorioRaiz, "Data");

if (!Directory.Exists(pastaDados))
{
    Directory.CreateDirectory(pastaDados);
}

string pathHotel = $"{pastaDados}/Hotel";
string pathHospedes = $"{pastaDados}/Hospedes";
string pathSuites = $"{pastaDados}/Suites";
string pathReservas = $"{pastaDados}/Reservas";

try
{
    bool existe = File.Exists(pathHotel);

    if (existe)
    {
        string jsonHotelDeserializar = File.ReadAllText(pathHotel);
        hotel.NomeHotel = JsonConvert.DeserializeObject<string>(jsonHotelDeserializar);
    }
}
catch (Exception)
{
    Console.WriteLine("Não foram encontradas informações sobre o hotel.");
}

try
{
    bool existe = File.Exists(pathHospedes);

    if (existe)
    {
        string jsonHospedesDeserializar = File.ReadAllText(pathHospedes);
        hotel.HospedesHotel = JsonConvert.DeserializeObject<List<Pessoa>>(jsonHospedesDeserializar);
    }
}
catch (Exception)
{
    Console.WriteLine("Não foram encontradas informações sobre os hóspedes.");
}

try
{
    bool existe = File.Exists(pathSuites);

    if (existe)
    {
        string jsonSuitesDeserializar = File.ReadAllText(pathSuites);
        hotel.SuitesHotel = JsonConvert.DeserializeObject<List<Suite>>(jsonSuitesDeserializar);
    }
}
catch (Exception)
{
    Console.WriteLine("Não foram encontradas informações sobre as suítes.");
}

try
{
    bool existe = File.Exists(pathReservas);

    if (existe)
    {
        string jsonReservasDeserializar = File.ReadAllText(pathReservas);
        hotel.ReservasHotel = JsonConvert.DeserializeObject<List<Reserva>>(jsonReservasDeserializar);
    }
}
catch (Exception)
{
    Console.WriteLine("Não foram encontradas informações sobre as reservas.");    
}


Console.Clear();
if (hotel.NomeHotel is null)
{
    hotel.AdicionarNomeHotel();
}
else
{
    Console.WriteLine($"Hotel {hotel.NomeHotel}");
    Console.WriteLine($"Deseja iniciar outro hotel? 1 - Sim");

    string iniciarOutroHotelTexto = Console.ReadLine();

    if (int.TryParse(iniciarOutroHotelTexto, out int iniciarOutroHotel))
    {
        if (iniciarOutroHotel == 1)
        {
            hotel.AdicionarNomeHotel();
        }
    }
}

while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("=== MENU PRINCIPAL ===");
    Console.WriteLine($"Bem vindo ao Hotel {hotel.NomeHotel}. Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar novo hóspede");
    Console.WriteLine("2 - Cadastrar nova suíte");
    Console.WriteLine("3 - Quantidade de hóspedes cadastrados");
    Console.WriteLine("4 - Buscar Hóspede pelo CPF");
    Console.WriteLine("5 - Adicionar reserva");
    Console.WriteLine("6 - Histórico de reservas pelo CPF"); 
    Console.WriteLine("7 - Encerrar");

    opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Clear();

            Console.WriteLine("Digite o primeiro nome da pessoa:");
            string nome = Console.ReadLine();

            Console.WriteLine("Agora digite o sobrenome da pessoa:");
            string sobrenome = Console.ReadLine();

            bool exibiCpf = true;
            long cpf = 0;
            bool cpfValido = false;

            while (exibiCpf)
            {
                Console.WriteLine("Digite o CPF do novo hóspede (ex. 11111111111)");

                string cpfTexto = Console.ReadLine();

                if (long.TryParse(cpfTexto, out cpf))
                {
                    if (cpf >= 10000000000L && cpf <= 99999999999L)
                    {
                        exibiCpf = false;
                        cpfValido = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("CPF deve ter exatamente 11 dígitos!");
                        hotel.PausarTela();
                    }
                }
                else
                {
                    Console.WriteLine("CPF inválido! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            Pessoa hospede = new Pessoa(nome, sobrenome, cpf);

            hotel.AdicionarHospedes(hospede);

            break;
        case "2":
            Console.Clear();

            bool exibiIdDaSuite = true;
            int idDaSuite = 0;
            bool idDaSuiteValido = false;

            while (exibiIdDaSuite)
            {
                Console.WriteLine("Digite o identificador(ID) da suíte:");

                string idDaSuiteTexto = Console.ReadLine();

                if (int.TryParse(idDaSuiteTexto, out idDaSuite))
                {
                    Suite suiteExistente = hotel.SuitesHotel.FirstOrDefault(suites => suites.IdDaSuite == idDaSuite);

                    if (suiteExistente != null)
                    {
                        Console.WriteLine("Outra suíte está usando esse ID. Digite um ID único.");
                        hotel.PausarTela();
                    }
                    else
                    {
                        exibiIdDaSuite = false;
                        idDaSuiteValido = true;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("ID inválido! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            Console.WriteLine("Digite o nome da suite:");
            string nomeDaSuite = Console.ReadLine();

            bool exibiTipoDaSuite = true;
            int tipoDaSuite = 0;
            bool tipoDaSuiteValido = false;

            while (exibiTipoDaSuite)
            {
                Console.WriteLine("Digite o tipo da suite.");
                Console.WriteLine("1 - Básica");
                Console.WriteLine("2 - Intermediária");
                Console.WriteLine("3 - Completa");

                string tipoDaSuiteTexto = Console.ReadLine();

                if (int.TryParse(tipoDaSuiteTexto, out tipoDaSuite))
                {
                    if (tipoDaSuite != 1 && tipoDaSuite != 2 && tipoDaSuite != 3)
                    {
                        Console.WriteLine("Tipo de suíte inválido! Digite 1, 2 ou 3.");
                        hotel.PausarTela();
                    }
                    else
                    {
                        exibiTipoDaSuite = false;
                        tipoDaSuiteValido = true;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Tipo inválido! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            bool exibiCapacidadeDaSuite = true;
            int capacidadeDaSuite = 0;
            bool capacidadeDaSuiteValido = false;

            while (exibiCapacidadeDaSuite)
            {
                Console.WriteLine("Qual a capacidade máxima da suíte? (ex: 2)");

                string capacidadeDaSuiteTexto = Console.ReadLine();

                if (int.TryParse(capacidadeDaSuiteTexto, out capacidadeDaSuite))
                {
                    exibiCapacidadeDaSuite = false;
                    capacidadeDaSuiteValido = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Capacidade inválida! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            bool exibiValorDaDiariaDaSuite = true;
            decimal valorDaDiariaDaSuite = 0M;
            bool valorDaDiariaDaSuiteValido = false;

            while (exibiValorDaDiariaDaSuite)
            {
                Console.WriteLine("Por último digite o valor de cada diária dessa suíte (ex: 120,50):");

                string valorDaDiariaDaSuiteTexto = Console.ReadLine();

                if (decimal.TryParse(valorDaDiariaDaSuiteTexto, out valorDaDiariaDaSuite))
                {
                    if (valorDaDiariaDaSuite > 0)
                    {
                        Console.WriteLine($"Valor R$ {valorDaDiariaDaSuite:F2} registrado com sucesso!");
                        exibiValorDaDiariaDaSuite = false;
                        valorDaDiariaDaSuiteValido = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("O valor da diária deve ser maior que zero!");
                        hotel.PausarTela();
                    }
                }
                else
                {
                    Console.WriteLine("Valor inválido! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            Suite Suite = new Suite(idDaSuite, nomeDaSuite, tipoDaSuite, capacidadeDaSuite, valorDaDiariaDaSuite);

            hotel.AdicionarSuite(Suite);

            break;
        case "3":
            Console.Clear();

            int quantidadeDeHospedes = hotel.ObterQuantidadeHospedes();

            Console.WriteLine($"Atualmente há {quantidadeDeHospedes} hóspedes cadastrasdos no hotel.");
            hotel.PausarTela();

            break;
        case "4":
            Console.Clear();

            bool exibiCpfDoHospede = true;

            Console.WriteLine("Digite o CPF do hóspede:");
            while(exibiCpfDoHospede)
            {    
                string cpfDoHospedeTexto = Console.ReadLine();
    
                if (long.TryParse(cpfDoHospedeTexto, out long cpfDoHospede))
                {
                    if (cpfDoHospede >= 10000000000L && cpfDoHospede <= 99999999999L)
                    {
                        string nomeHospedeOut = hotel.BuscarHospedePeloCpf(cpfDoHospede);
                        Console.WriteLine(nomeHospedeOut);
                        hotel.PausarTela();
                        exibiCpfDoHospede = false;
                        Console.WriteLine(nomeHospedeOut);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("CPF deve ter exatamente 11 dígitos!");
                        hotel.PausarTela();
                    }
                }
                else
                {
                    Console.WriteLine("CPF inválido! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            break;
        case "5":
            Console.Clear();

            Console.WriteLine("Por favor digite os CPFs dos hóspedes dessa reserva.");
            Console.WriteLine("Aperte ENTER sem digitar nada para finalizar lista de hóspedes.");

            bool exibiLoopHospedes = true;
            bool exibiHospedesDaReserva = true;
            List<long> cpfsHospedes = new List<long>();

            while (exibiHospedesDaReserva)
            {
                string inputCpf = Console.ReadLine();

                if (string.IsNullOrEmpty(inputCpf))
                {
                    
                    exibiHospedesDaReserva = false;
                    break;
                }

                if (long.TryParse(inputCpf, out long cpfHospedeDaReserva))
                {
                    if (cpfHospedeDaReserva >= 10000000000L && cpfHospedeDaReserva <= 99999999999L)
                    {
                        Pessoa cpfHospedeDaReservaPessoa = hotel.HospedesHotel.FirstOrDefault(hospede => hospede.Cpf == cpfHospedeDaReserva);
                        if (cpfHospedeDaReservaPessoa == null)
                        {
                            Console.WriteLine("Este CPF não está cadastrado no hotel!");
                            hotel.PausarTela();
                        }
                        else
                        {
                            cpfsHospedes.Add(cpfHospedeDaReserva);
                            Console.WriteLine($"CPF {cpfHospedeDaReserva} adicionado! Digite o próximo ou ENTER para finalizar.");
                        }                  
                    }
                    else
                    {
                        Console.WriteLine("CPF deve ter exatamente 11 dígitos!");
                        hotel.PausarTela();
                    }
                }
                else
                {
                    Console.WriteLine("CPF inválido! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            Console.WriteLine($"Total de CPFs cadastrados: {cpfsHospedes.Count}");

            bool exibiCpfResponsavel = true;
            long cpfResponsavel = 0;
            bool cpfResponsavelValido = false;

            while (exibiCpfResponsavel)
            {
                Console.WriteLine("Qual desses CPFs vai ser o responsável pela reserva?");

                string cpfResponsavelTexto = Console.ReadLine();

                if (long.TryParse(cpfResponsavelTexto, out cpfResponsavel))
                {
                    if (cpfResponsavel >= 10000000000L && cpfResponsavel <= 99999999999L)
                    {
                        Pessoa hospedeResponsavel = hotel.HospedesHotel.FirstOrDefault(hospede => hospede.Cpf == cpfResponsavel);
                        bool cpfResponavelPelaReservaEstaNaLista = cpfsHospedes.Contains(cpfResponsavel);

                        if (hospedeResponsavel == null)
                        {
                            Console.WriteLine("Este CPF não está cadastrado no hotel!");
                            hotel.PausarTela();
                        }
                        else if (!cpfResponavelPelaReservaEstaNaLista)
                        {
                            Console.WriteLine("Este CPF não está na lista de hóspedes desta reserva!");
                            hotel.PausarTela();
                        }
                        else
                        {
                            exibiCpfResponsavel = false;
                            cpfResponsavelValido = true;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("CPF deve ter exatamente 11 dígitos!");
                        hotel.PausarTela();
                    }
                }
                else
                {
                    Console.WriteLine("CPF inválido! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            bool exibiTipoSuiteDaReserva = true;
            int tipoDaSuiteDaReserva = 0;
            bool tipoDaSuiteDaReservaValido = false;

            while (exibiTipoSuiteDaReserva)
            {
                Console.WriteLine("Qual o tipo de suíte da reserva?");

                bool exibiTipoDaSuiteReserva = true;
                int tipoDaSuiteReserva = 0;
                bool tipoDaSuiteReservaValido = false;

                while (exibiTipoDaSuiteReserva)
                {
                    Console.WriteLine("Digite o tipo da suite.");
                    Console.WriteLine("1 - Básica");
                    Console.WriteLine("2 - Intermediária");
                    Console.WriteLine("3 - Completa");

                    string tipoDaSuiteReservaTexto = Console.ReadLine();

                    if (int.TryParse(tipoDaSuiteReservaTexto, out tipoDaSuiteReserva))
                    {
                        if (tipoDaSuiteReserva != 1 && tipoDaSuiteReserva != 2 && tipoDaSuiteReserva != 3)
                        {
                            Console.WriteLine("Tipo de suíte inválido! Digite 1, 2 ou 3.");
                            hotel.PausarTela();
                        }
                        else
                        {
                            exibiTipoDaSuiteReserva = false;
                            tipoDaSuiteReservaValido = true;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tipo inválido! Digite apenas números.");
                        hotel.PausarTela();
                    }
                }



            }

            bool exibiDataInicialDaReserva = true;
            DateTime dataInicialDaReserva = DateTime.Now;
            bool dataInicialDaReservaValido = false;

            while (exibiDataInicialDaReserva)
            {
                Console.WriteLine("Qual a data inical da reserva? (ex: dd/MM/aaaa)");

                string dataInicialDaReservaTexto = Console.ReadLine();

                if (DateTime.TryParseExact(dataInicialDaReservaTexto, "dd/MM/yyyy",
                                      CultureInfo.InvariantCulture, DateTimeStyles.None,
                                      out dataInicialDaReserva))
                {
                    exibiDataInicialDaReserva = false;
                    dataInicialDaReservaValido = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Data inválida! Data de exemplo: 04/06/2001");
                    hotel.PausarTela();
                }
            }

            bool exibiDataFinalDaReserva = true;
            DateTime dataFinalDaReserva = DateTime.Now;
            bool dataFinalDaReservaValido = false;

            while (exibiDataFinalDaReserva)
            {
                Console.WriteLine("Qual a data final da reserva? (ex: dd/MM/aaaa)");

                string dataFinalDaReservaTexto = Console.ReadLine();

                if (DateTime.TryParseExact(dataFinalDaReservaTexto, "dd/MM/yyyy",
                                      CultureInfo.InvariantCulture, DateTimeStyles.None,
                                      out dataFinalDaReserva))
                {
                    if (dataFinalDaReserva <= dataInicialDaReserva)
                    {
                        Console.WriteLine("Data inválida! A data final da reserva tem que ser posterior a data inicial da reserva.");
                        hotel.PausarTela();
                    }
                    else
                    {
                        exibiDataFinalDaReserva = false;
                        dataFinalDaReservaValido = true;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Data inválida! Data de exemplo: 19/04/2003");
                    hotel.PausarTela();
                }
            }         

            
            List<Pessoa> hospedesDaReserva = new List<Pessoa>();

            foreach (long cpfHospedesNomeCompleto in cpfsHospedes)
            {
                string nomeHospedeDaReserva = hotel.HospedesHotel.Where(hospede => hospede.Cpf == cpfHospedesNomeCompleto).Select(hospede => hospede.Nome).FirstOrDefault();
                string sobrenomeHospedeDaReserva = hotel.HospedesHotel.Where(hospede => hospede.Cpf == cpfHospedesNomeCompleto).Select(hospede => hospede.Sobrenome).FirstOrDefault();

                Pessoa p1 = new Pessoa(nomeHospedeDaReserva, sobrenomeHospedeDaReserva, cpfHospedesNomeCompleto);
                
                hospedesDaReserva.Add(p1);
            }

            if (cpfResponsavelValido && tipoDaSuiteDaReservaValido && dataInicialDaReservaValido && dataFinalDaReservaValido)
            {
                hotel.AdicionarReserva(hospedesDaReserva, hospedesDaReserva.Count, cpfResponsavel, tipoDaSuiteDaReserva, dataInicialDaReserva, dataFinalDaReserva);
                Console.WriteLine("Reserva adicionada com sucesso!");
            }

            hotel.PausarTela();

            break;
        case "6":
            Console.Clear();

            Console.WriteLine("Digite o CPF responsável pelas reservas:");
            
            bool exibiCpfResponsavelPelaReserva = true;
            long cpfResponsavelPelaReserva = 0;
            bool cpfResponsavelPelaReservaValido = false;

            while (exibiCpfResponsavelPelaReserva)
            {

                string cpfResponsavelPelaReservaTexto = Console.ReadLine();

                if (long.TryParse(cpfResponsavelPelaReservaTexto, out cpfResponsavelPelaReserva))
                {
                    if (cpfResponsavelPelaReserva >= 10000000000L && cpfResponsavelPelaReserva <= 99999999999L)
                    {
                        exibiCpfResponsavelPelaReserva = false;
                        cpfResponsavelPelaReservaValido = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("CPF deve ter exatamente 11 dígitos!");
                        hotel.PausarTela();
                    }
                }
                else
                {
                    Console.WriteLine("CPF inválido! Digite apenas números.");
                    hotel.PausarTela();
                }
            }

            if (cpfResponsavelPelaReservaValido)
            {
                hotel.HistoricoReservas(cpfResponsavelPelaReserva);
            }

            break;
        case "7":
            Console.WriteLine("Sair");
            exibirMenu = false;

            if (!Directory.Exists(pastaDados))
            {
                Directory.CreateDirectory(pastaDados);
            }

            string jsonHotel = JsonConvert.SerializeObject(hotel.NomeHotel);
            File.WriteAllText(pathHotel,jsonHotel);

            string jsonListaHospedes = JsonConvert.SerializeObject(hotel.HospedesHotel);
            File.WriteAllText(pathHospedes,jsonListaHospedes);

            string jsonListaSuites = JsonConvert.SerializeObject(hotel.SuitesHotel);
            File.WriteAllText(pathSuites,jsonListaSuites);

            string jsonListaReservas = JsonConvert.SerializeObject(hotel.ReservasHotel);
            File.WriteAllText(pathReservas,jsonListaReservas);

            break;
        default:
            Console.Clear();
            Console.WriteLine("Opção inválida");
            break;
    }
}
    Console.WriteLine("O programa se encerrou");
