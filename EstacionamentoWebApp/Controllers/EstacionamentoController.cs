using Microsoft.AspNetCore.Mvc;
using EstacionamentoWebApp.Models;

namespace EstacionamentoWebApp.Controllers
{
    public class EstacionamentoController : Controller
    {
        // Instância única do estacionamento com preço inicial R$5 e R$2 por hora
        private static Estacionamento estacionamento = new(5, 2);

        // Página inicial que exibe apenas os botões
        public IActionResult Index()
        {
            return View();
        }

        // Mostra os veículos estacionados
        public IActionResult Estacionados()
        {
            var veiculos = estacionamento.ListarVeiculos();
            return View(veiculos); // View: Views/Estacionamento/Estacionados.cshtml
        }

        // Exibe o formulário para adicionar um veículo
        public IActionResult Adicionar()
        {
            return View();
        }

        // Recebe a placa enviada via POST para adicionar um veículo
        [HttpPost]
        public IActionResult Adicionar(string placa)
        {
            // Validação: placa não pode ser nula ou vazia
            if (string.IsNullOrWhiteSpace(placa))
            {
                ModelState.AddModelError("", "Placa inválida.");
                return View();
            }

            // Tenta adicionar o veículo; se já estiver cadastrado, exibe erro
            bool sucesso = estacionamento.AdicionarVeiculo(placa);
            if (!sucesso)
            {
                ModelState.AddModelError("", "Veículo já está estacionado.");
                return View();
            }

            // Redireciona para a lista de veículos estacionados
            return RedirectToAction("Estacionados");
        }

        // Exibe o formulário para remover um veículo
        public IActionResult Remover()
        {
            return View();
        }

        // Processa o POST da remoção de um veículo, com valor pago
        [HttpPost]
        public IActionResult Remover(string placa, string valorPago)
        {
            // Validação do valor pago no formato brasileiro (ex: 15,50)
            if (!decimal.TryParse(valorPago,
                    System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.GetCultureInfo("pt-BR"),
                    out decimal valorPagoDecimal))
            {
                ViewBag.Erro = "Valor inválido. Use o formato brasileiro (ex: 15,50)";
                return View();
            }

            // Tenta remover o veículo com base na placa e valor pago
            var resultado = estacionamento.RemoverVeiculo(placa, valorPagoDecimal);

            // Se não encontrou o veículo ou pagamento for insuficiente
            if (resultado == null)
            {
                ViewBag.Erro = "Veículo não encontrado ou pagamento insuficiente.";
                return View();
            }

            // Preenche os dados para exibir no recibo (View "Removido")
            ViewBag.Placa = placa;
            ViewBag.Entrada = resultado?.entrada;
            ViewBag.Saida = resultado?.saida;
            ViewBag.Horas = resultado?.horas;
            ViewBag.Total = resultado?.valorTotal;
            ViewBag.Troco = resultado?.troco;

            return View("Removido");
        }
    }
}
