namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0; // Valor fixo cobrado na entrada
        private decimal precoPorHora = 0; // Valor por hora de estacionamento

        // Lista de placas dos veículos estacionados
        private List<string> veiculos = new List<string>();

        // Dicionário que mapeia placa -> horário de entrada
        private Dictionary<string, DateTime> horariosEntrada = new Dictionary<string, DateTime>();

        private readonly TimeZoneInfo zonaBrasil; // Armazena o fuso horário de Brasília

        // Construtor que recebe os preços e tenta configurar o fuso horário correto
        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;

            try
            {
                zonaBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            }
            catch
            {
                zonaBrasil = TimeZoneInfo.Local; // Fallback caso Brasília não esteja disponível
            }
        }

        // Adiciona um veículo à lista e registra a hora de entrada
        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine()?.Trim(); // Remove espaços extras

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Placa inválida. Operação cancelada.");
                return;
            }

            // Verifica se já está estacionado
            if (veiculos.Any(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Este veículo já está estacionado.");
                return;
            }

            veiculos.Add(placa);
            DateTime horarioEntrada = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaBrasil);
            horariosEntrada[placa] = horarioEntrada;

            Console.WriteLine($"Veículo {placa} estacionado com sucesso.");
            Console.WriteLine($"Entrada registrada às {horarioEntrada:dd/MM/yyyy HH:mm:ss}");
        }

        // Remove um veículo, calcula tempo e valor a pagar
        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Placa inválida. Operação cancelada.");
                return;
            }

            // Verifica se está na lista
            if (veiculos.Any(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase)))
            {
                if (horariosEntrada.TryGetValue(placa, out DateTime horarioEntrada))
                {
                    DateTime horarioSaida = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaBrasil);
                    TimeSpan tempoPermanencia = horarioSaida - horarioEntrada;
                    int horas = (int)Math.Ceiling(tempoPermanencia.TotalHours); // Arredondar pra cima

                    decimal valorTotal = precoInicial + (precoPorHora * horas);

                    Console.WriteLine($"Horário de entrada: {horarioEntrada:dd/MM/yyyy HH:mm:ss}");
                    Console.WriteLine($"Horário de saída:  {horarioSaida:dd/MM/yyyy HH:mm:ss}");
                    Console.WriteLine($"Tempo total: {horas} hora(s)");
                    Console.WriteLine($"Total a pagar: R$ {valorTotal:F2}");

                    veiculos.RemoveAll(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase));
                    horariosEntrada.Remove(placa);
                }
                else
                {
                    Console.WriteLine("Erro: horário de entrada não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Veículo não encontrado. Verifique a placa digitada.");
            }
        }

        // Lista todos os veículos estacionados e suas datas/hora de entrada
        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Veículos atualmente estacionados:");

                foreach (string placa in veiculos)
                {
                    if (horariosEntrada.TryGetValue(placa, out DateTime entrada))
                    {
                        Console.WriteLine($" - {placa} | Entrada: {entrada:dd/MM/yyyy HH:mm:ss}");
                    }
                    else
                    {
                        Console.WriteLine($" - {placa} | Entrada: Não registrada");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nenhum veículo está estacionado no momento.");
            }
        }
    }
}

