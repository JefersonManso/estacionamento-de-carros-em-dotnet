using Microsoft.AspNetCore.Mvc;
using EstacionamentoWebApp.Models;

namespace EstacionamentoWebApp.Controllers
{
    public class EstacionamentoController : Controller
    {
        private static Estacionamento estacionamento = new(5, 2); // R$5 inicial + R$2/h

        public IActionResult Index()
        {
            // A Home agora só mostra os botões
            return View();
        }

        // NOVA ACTION: mostra os veículos estacionados
        public IActionResult Estacionados()
        {
            var veiculos = estacionamento.ListarVeiculos();
            return View(veiculos); // View: Views/Estacionamento/Estacionados.cshtml
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                ModelState.AddModelError("", "Placa inválida.");
                return View();
            }

            bool sucesso = estacionamento.AdicionarVeiculo(placa);
            if (!sucesso)
            {
                ModelState.AddModelError("", "Veículo já está estacionado.");
                return View();
            }

            return RedirectToAction("Estacionados"); // redireciona para nova página
        }

        public IActionResult Remover()
        {
            return View();
        }

        [HttpPost]
public IActionResult Remover(string placa, string valorPago)
{
    if (!decimal.TryParse(valorPago, 
        System.Globalization.NumberStyles.Number, 
        System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), 
        out decimal valorPagoDecimal))
    {
        ViewBag.Erro = "Valor inválido. Use o formato brasileiro (ex: 15,50)";
        return View();
    }

    var resultado = estacionamento.RemoverVeiculo(placa, valorPagoDecimal);

    if (resultado == null)
    {
        ViewBag.Erro = "Veículo não encontrado ou pagamento insuficiente.";
        return View();
    }

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
