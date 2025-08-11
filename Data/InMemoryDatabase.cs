using ArtesaoDeSoftware.Models;

namespace ArtesaoDeSoftware.Data;

 // Nosso "banco de dados" falso em memória
public static class InMemoryDatabase
{
    // Simula uma tabela de pedidos no banco de dados
    public static readonly List<Pedido> Pedidos = [];
}