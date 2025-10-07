using System.Net.Mail;
using ArtesaoDeSoftware.Data;
using ArtesaoDeSoftware.Models;

namespace ArtesaoDeSoftware.Services;

public class ProcessadorDePedidos
{
    private const decimal DESCONTO_FIDELIDADE_ALTO = 0.10m;
    private const decimal DESCONTO_FIDELIDADE_BAIXO = 0.05m;
    private const decimal DESCONTO_PROMOCAO_NATAL = 0.20m;

    public void ProcessarPedido(Pedido pedido, ETipoDesconto tipoDesconto, decimal valorDoDesconto)
    {
        Console.WriteLine($"\nIniciando processamento do pedido {pedido.Id}...");

        pedido.Status = "Processando";

        CalcularTotalBruto(pedido);
        AplicarDesconto(pedido, tipoDesconto, valorDoDesconto);
        SalvarPedido(pedido);
        EnviarEmailDeConfirmacao(pedido);

        pedido.Status = "Concluído";
        Console.WriteLine("Pedido processado com sucesso!");
    }

    private static void EnviarEmailDeConfirmacao(Pedido pedido)
    {
        Console.WriteLine("Enviando e-mail de confirmação...");
        try
        {
            var mail = new MailMessage("loja@email.com", pedido.Cliente.Email);
            mail.Subject = $"Pedido {pedido.Id} confirmado!";

            mail.Body = $"Olá {pedido.Cliente.Nome},\n\nSeu pedido de {pedido.TotalFinal:C} foi processado e será enviado em breve.\n\nObrigado por comprar conosco!";

            // Simulação de envio
            // var smtpClient = new SmtpClient("smtp.server.com");
            // smtpClient.Send(mail);

            Console.WriteLine($"E-mail enviado para {pedido.Cliente.Email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: Falha ao enviar e-mail: {ex.Message}");
        }
    }

    private static void SalvarPedido(Pedido pedido)
    {
        Console.WriteLine("Salvando pedido em 'banco de dados'...");
        InMemoryDatabase.Pedidos.Add(pedido);
    }

    private static void AplicarDesconto(Pedido pedido, ETipoDesconto tipoDesconto, decimal valorDoDesconto)
    {
        
        decimal valorDoDescontoCalculado = 0;

        if (tipoDesconto == ETipoDesconto.CupomFixo)
        {
            valorDoDescontoCalculado = valorDoDesconto;
        }
        else if (tipoDesconto == ETipoDesconto.Porcentagem)
        {
            valorDoDescontoCalculado = pedido.Total * (valorDoDesconto / 100);
        }
        else if (tipoDesconto == ETipoDesconto.Fidelidade)
        {

            if (pedido.Cliente.AnosFidelidade > 5)
            {
                valorDoDescontoCalculado = pedido.Total * DESCONTO_FIDELIDADE_ALTO;
            }
            else
            {
                valorDoDescontoCalculado = pedido.Total * DESCONTO_FIDELIDADE_BAIXO;
            }
        }
        else if (tipoDesconto == ETipoDesconto.PromocaoNatal)
        {
            valorDoDescontoCalculado = pedido.Total * DESCONTO_PROMOCAO_NATAL;
        }

        pedido.Desconto = valorDoDescontoCalculado;
        Console.WriteLine($"Desconto aplicado: {pedido.Desconto:C}");

        pedido.TotalFinal = pedido.Total - pedido.Desconto;
        Console.WriteLine($"Total final: {pedido.TotalFinal:C}");
    }

    private static void CalcularTotalBruto(Pedido pedido)
    {
        decimal total = 0;

        foreach (var item in pedido.Itens)
        {
            total += item.Produto.Preco * item.Quantidade;
        }

        pedido.Total = total;
        Console.WriteLine($"Total dos itens: {pedido.Total:C}");
    }
}