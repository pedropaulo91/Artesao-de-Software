using ArtesaoDeSoftware.Models;
using ArtesaoDeSoftware.Services;

System.Console.WriteLine("Iniciando o sistema.\n");
Console.WriteLine("Simulação de dados que viriam de uma tela ou API\n");

// 1. Criar um cliente
var cliente = new Cliente(1, "João da Silva", "joao.silva@email.com", 6); // 6 anos de fidelidade

// 2. Criar alguns produtos disponíveis na loja
var produto1 = new Produto(101, "Teclado Mecânico RGB", 350.50m);
var produto2 = new Produto(102, "Mouse Gamer 16000 DPI", 199.99m);
var produto3 = new Produto(103, "Monitor Ultrawide 34'", 2500.00m);

// 3. Simular um carrinho de compras
var carrinho = new List<ItemDoPedido>
{
    new ItemDoPedido(produto1, 1),
    new ItemDoPedido(produto2, 2)
};

// 4. Criar o pedido a partir do carrinho
var pedido = new Pedido
{
    Id = new Random().Next(1000, 9999),
    Cliente = cliente,
    Itens = carrinho,
};

// 5. Instanciar e chamar nosso serviço caótico
var processador = new ProcessadorDePedidos();
processador.ProcessarPedido(pedido, ETipoDesconto.Fidelidade, 0); // O valor do desconto é ignorado para Fidelidade

Console.WriteLine("\n--- Processamento Concluído ---");
Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();