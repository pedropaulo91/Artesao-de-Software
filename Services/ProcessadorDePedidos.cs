using System.Net.Mail;
using ArtesaoDeSoftware.Data;
using ArtesaoDeSoftware.Models;

namespace ArtesaoDeSoftware.Services;

public class ProcessadorDePedidos
{
    public void ProcessarPedido(Pedido p, ETipoDesconto tipoDesc, decimal val)
    {

        Console.WriteLine($"\nIniciando processamento do pedido {p.Id}...");

        p.Status = "Processando";

        decimal total = 0;
        foreach (var i in p.Itens)
        {
            total += i.Produto.Preco * i.Quantidade;
        }
        p.Total = total;
        Console.WriteLine($"Total dos itens: {p.Total:C}");

        decimal desc = 0;
        if (tipoDesc == ETipoDesconto.CupomFixo)
        {
            desc = val;
        }
        else if (tipoDesc == ETipoDesconto.Porcentagem)
        {
            desc = p.Total * (val / 100);
        }
        else if (tipoDesc == ETipoDesconto.Fidelidade)
        {

            if (p.Cliente.AnosFidelidade > 5)
            {
                desc = p.Total * 0.10m; 
            }
            else
            {
                desc = p.Total * 0.05m; 
            }
        }
        else if (tipoDesc == ETipoDesconto.PromocaoNatal)
        {
            desc = p.Total * 0.20m; 
        }
        p.Desconto = desc;
        Console.WriteLine($"Desconto aplicado: {p.Desconto:C}");

        p.TotalFinal = p.Total - p.Desconto;
        Console.WriteLine($"Total final: {p.TotalFinal:C}");

        Console.WriteLine("Salvando pedido em 'banco de dados'...");
        InMemoryDatabase.Pedidos.Add(p);

        Console.WriteLine("Enviando e-mail de confirmação...");
        try
        {
            var mail = new MailMessage("loja@email.com", p.Cliente.Email); 
            mail.Subject = $"Pedido {p.Id} confirmado!";
           
            mail.Body = $"Olá {p.Cliente.Nome},\n\nSeu pedido de {p.TotalFinal:C} foi processado e será enviado em breve.\n\nObrigado por comprar conosco!";

            // Simulação de envio
            // var smtpClient = new SmtpClient("smtp.server.com");
            // smtpClient.Send(mail);

            Console.WriteLine($"E-mail enviado para {p.Cliente.Email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: Falha ao enviar e-mail: {ex.Message}");
        }

        p.Status = "Concluído";
        Console.WriteLine("Pedido processado com sucesso!");
    }
}