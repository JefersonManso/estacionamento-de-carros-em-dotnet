namespace EstacionamentoWebApp.Models
{
    public class Estacionamento
    {
        private decimal precoInicial;
        private decimal precoPorHora;

        private List<string> veiculos = new();
        private Dictionary<string, DateTime> horariosEntrada = new();

        private readonly TimeZoneInfo zonaBrasil;

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
                zonaBrasil = TimeZoneInfo.Local;
            }
        }

        public bool AdicionarVeiculo(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa)) return false;

            if (veiculos.Any(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase)))
                return false;

            veiculos.Add(placa);
            horariosEntrada[placa] = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaBrasil);
            return true;
        }

        public (decimal valorTotal, int horas, DateTime entrada, DateTime saida, decimal troco)? RemoverVeiculo(string placa, decimal valorPago)
        {
            if (!veiculos.Any(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase)))
                return null;

            DateTime entrada = horariosEntrada[placa];
            DateTime saida = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaBrasil);
            TimeSpan tempo = saida - entrada;
            int horas = (int)Math.Ceiling(tempo.TotalHours);

            decimal valorTotal = precoInicial + precoPorHora * horas;

            if (valorPago < valorTotal)
            {
                return null; // pagamento insuficiente
            }

            decimal troco = valorPago - valorTotal;

            veiculos.RemoveAll(v => v.Equals(placa, StringComparison.OrdinalIgnoreCase));
            horariosEntrada.Remove(placa);

            return (valorTotal, horas, entrada, saida, troco);
        }

        public List<string> ListarVeiculos()
        {
            return veiculos.ToList();
        }
    }
}
