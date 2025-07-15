# 🚗 EstacionamentoWebApp

Sistema desenvolvido com base no desafio da [DIO](https://www.dio.me) para gerenciamento de estacionamento, utilizando **ASP.NET Core MVC**.

---

## 📚 Desafio Original - DIO Trilha .NET (Fundamentos)

Construa um sistema para gerenciar veículos estacionados:

- ✅ Adicionar veículo  
- ✅ Remover veículo (com cálculo de horas e valor total)  
- ✅ Listar veículos

### 🎯 Objetivo

Simular um estacionamento real com controle de entradas, saídas e cálculo de valores com base no seguinte diagrama:

![Diagrama de classe estacionamento](EstacionamentoWebApp/docs/imagens/diagrama_classe_estacionamento.png)

---

## 🧪 Testes Realizados no Console

| Cadastro de Veículo | Remoção com Cálculo |
|---------------------|---------------------|
| ![01](EstacionamentoWebApp/docs/imagens/01.png) | ![02](EstacionamentoWebApp/docs/imagens/02.png) |
| ![03](EstacionamentoWebApp/docs/imagens/03.png) | ![04](EstacionamentoWebApp/docs/imagens/04.png) |
| ![05](EstacionamentoWebApp/docs/imagens/05.png) | ![06](EstacionamentoWebApp/docs/imagens/06.png) |
| ![07](EstacionamentoWebApp/docs/imagens/07.png) | ![08](EstacionamentoWebApp/docs/imagens/08.png) |
| ![09](EstacionamentoWebApp/docs/imagens/09.png) | ![10](EstacionamentoWebApp/docs/imagens/10.png) |
| ![11](EstacionamentoWebApp/docs/imagens/11.png) | ![12](EstacionamentoWebApp/docs/imagens/12.png) |
| ![13](EstacionamentoWebApp/docs/imagens/13.png) | ![14](EstacionamentoWebApp/docs/imagens/14.png) |
| ![15](EstacionamentoWebApp/docs/imagens/15.png) | ![16](EstacionamentoWebApp/docs/imagens/16.png) |
| ![17](EstacionamentoWebApp/docs/imagens/17.png) | ![25](EstacionamentoWebApp/docs/imagens/25.png) |

---

## 💻 Evolução para Web com ASP.NET MVC

Este projeto evoluiu para uma aplicação web funcional com Razor Pages, Bootstrap e layout responsivo.

### 🖼️ Prints da Interface Web

| Página Inicial    | Adicionar Veículo  |
|-------------------|--------------------|
| ![Home](EstacionamentoWebApp/docs/imagens/18.png) | ![Adicionar](EstacionamentoWebApp/docs/imagens/19.png) |

| Adicionando Veículo   | Veículo Cadastrado (Recibo) |
|-----------------------|-----------------------------|
| ![Remover](EstacionamentoWebApp/docs/imagens/20.png) | ![Recibo](EstacionamentoWebApp/docs/imagens/21.png) |

| Lista de Estacionados | Remoções e Pagamento  |
|-----------------------|----------------------|
| ![Estacionados](EstacionamentoWebApp/docs/imagens/22.png) | ![Remoção](EstacionamentoWebApp/docs/imagens/23.png) |
|                       | ![Remoção 2](EstacionamentoWebApp/docs/imagens/24.png) |

---

## 🧱 Estrutura da Classe Principal

```csharp
public class Estacionamento
{
    private decimal precoInicial;
    private decimal precoPorHora;
    private List<string> veiculos;

    public bool AdicionarVeiculo(string placa) { ... }

    public (decimal valorTotal, int horas, DateTime entrada, DateTime saida, decimal troco)?
        RemoverVeiculo(string placa, decimal valorPago) { ... }

    public List<string> ListarVeiculos() { ... }
}

```

---

- # 🛠️ Tecnologias Utilizadas
- # 🗂️ Estrutura de Pastas
- # 📄 Licença
+ ## 🛠️ Tecnologias Utilizadas
+ ## 🗂️ Estrutura de Pastas
+ ## 📄 Licença


---

# 🗂️ Estrutura de Pastas

```bash
EstacionamentoWebApp/
├── Controllers/
│   └── EstacionamentoController.cs
├── Models/
│   └── Estacionamento.cs
├── Views/
│   └── Estacionamento/
│       ├── Index.cshtml
│       ├── Adicionar.cshtml
│       ├── Remover.cshtml
│       ├── Removido.cshtml
│       └── Estacionados.cshtml
├── imagens/
│   ├── console-adicionar.png
│   ├── console-remover.png
│   ├── web-home.png
│   ├── web-adicionar.png
│   ├── web-remover.png
│   ├── web-removido.png
│   └── web-estacionados.png
├── wwwroot/
│   └── css/site.css (estilização personalizada)
└── README.md
```
---
# 📄 Licença
Este projeto foi desenvolvido para fins educacionais no contexto do bootcamp DIO. Livre para aprendizado e melhoria.


---

### 🔍 O que foi melhorado:

| Item | Antes | Depois |
|------|-------|--------|
| Imagens | Muitos títulos longos | Nomes curtos, layout mais limpo |
| Cabeçalhos | Sem padrão claro | Hierarquia de títulos mantida |
| Estrutura de pastas | `imagens/` fora de `docs` | Centralizado em `docs/imagens/` |
| Licença | Ausente | Adicionada (opcional, mas recomendada) |
| Diagrama | Com legenda redundante | Mais direto e fluido |


