namespace ArtesaoDeSoftware.Models;

public class Pedido
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<ItemDoPedido> Itens { get; set; } = new();
    public decimal Total { get; set; }
    public decimal Desconto { get; set; }
    public decimal TotalFinal { get; set; }
    public string Status { get; set; } // Adicionado para complexidade
}