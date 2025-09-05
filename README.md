# Projeto Hospedagem Hotel

Este projeto é um sistema de gerenciamento de hospedagem para hotéis, desenvolvido em C# e .NET 9.0. Ele permite o cadastro de hóspedes, suítes, reservas e consulta de histórico de reservas.

## Funcionalidades

- Cadastro de hóspedes com nome, sobrenome e CPF.
- Cadastro de suítes com identificação, nome, tipo, capacidade e valor da diária.
- Cadastro de reservas, associando hóspedes, suíte, datas e responsável.
- Consulta de hóspedes pelo CPF.
- Consulta de histórico de reservas por CPF do responsável.
- Persistência dos dados em arquivos JSON na pasta `Data`.

## Estrutura do Projeto

- [`Program.cs`](Program.cs): Ponto de entrada do sistema e interface de menu.
- [`Models/Hotel.cs`](Models/Hotel.cs): Lógica principal do hotel.
- [`Models/Pessoa.cs`](Models/Pessoa.cs): Representa um hóspede.
- [`Models/Suite.cs`](Models/Suite.cs): Representa uma suíte.
- [`Models/Reserva.cs`](Models/Reserva.cs): Representa uma reserva.

## Como Executar

1. Certifique-se de ter o [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado.
2. No terminal, execute:

   ```sh
   dotnet run
   ```

3. Siga as instruções do menu para utilizar o sistema.

## Observações

- Os dados são salvos automaticamente na pasta `Data` ao encerrar o programa.
- O projeto utiliza o pacote [`Newtonsoft.Json`](https://www.nuget.org/packages/Newtonsoft.Json/) para serialização dos dados.

## Estrutura de Pastas

```
├── Program.cs
├── Models/
│   ├── Hotel.cs
│   ├── Pessoa.cs
│   ├── Reserva.cs
│   └── Suite.cs
├── ProjetoHospedagemHotel.csproj
├── ProjetoHospedagemHotel.sln
├── .gitignore
└── bin/ e obj/ (gerados pelo build)
```

## Licença

Este projeto é apenas para
