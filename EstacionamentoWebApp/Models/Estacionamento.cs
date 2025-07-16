namespace EstacionamentoWebApp.Models
{
    public class Estacionamento
    {
        // Preço fixo cobrado no início
        private decimal precoInicial;

        // Preço adicional por hora de permanência
        private decimal precoPorHora;

        // Lista de placas dos veículos estacionados
        private List<string> veiculos = new();

        // Dicionário para armazenar a hora de entrada de cada veículo
        private Dictionary<string, DateTime> horariosEntrada = new();

        // Fuso horário configurado para o Brasil
        private readonly TimeZoneInfo zonaBrasil;

        // Construtor da classe Estacionamento
        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;

            // Tenta obter o fuso horário de Brasília
            try
            {
                zonaBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            }
            catch
            {
                // Se não conseguir (ex: sistema Linux), usa o fuso horário local como fallback
                zonaBrasil = TimeZoneInfo.Local;
            }
        }

        // Método para adicionar um veículo ao estacionamento
        public bool AdicionarVeiculo(string placa)
        {
            // Valida a placa
            if (string.IsNullOrWhiteSpace(placa)) return false;

            // Impede duplicidade (placas iguais ignorando maiúsculas/minúsculas)
            if (veiculos.Any(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase)))
                return false;

            // Adiciona a placa à lista de veículos
            veiculos.Add(placa);

            // Registra a hora de entrada no horário do Brasil
            horariosEntrada[placa] = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaBrasil);

            return true;
        }

        // Método para remover um veículo, calculando o valor a pagar e o troco
        public (decimal valorTotal, int horas, DateTime entrada, DateTime saida, decimal troco)? RemoverVeiculo(string placa, decimal valorPago)
        {
            // Verifica se a placa está na lista
            if (!veiculos.Any(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase)))
                return null;

            // Obtém a hora de entrada e calcula a hora de saída atual
            DateTime entrada = horariosEntrada[placa];
            DateTime saida = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaBrasil);

            // Calcula o tempo de permanência em horas, arredondando para cima
            TimeSpan tempo = saida - entrada;
            int horas = (int)Math.Ceiling(tempo.TotalHours);

            // Calcula o valor total: preço fixo + (horas * preço por hora)
            decimal valorTotal = precoInicial + precoPorHora * horas;

            // Verifica se o valor pago é suficiente
            if (valorPago < valorTotal)
            {
                return null; // pagamento insuficiente
            }

            // Calcula o troco
            decimal troco = valorPago - valorTotal;

            // Remove o veículo da lista e da tabela de entrada
            veiculos.RemoveAll(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase));
            horariosEntrada.Remove(placa);

            // Retorna uma tupla com os dados do recibo
            return (valorTotal, horas, entrada, saida, troco);
        }

        // Retorna a lista atual de veículos estacionados
        public List<string> ListarVeiculos()
        {
            return veiculos.ToList();
        }
    }
}
