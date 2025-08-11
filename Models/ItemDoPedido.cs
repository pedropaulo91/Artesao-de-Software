namespace ArtesaoDeSoftware.Models;

public class ItemDoPedido(Produto produto, int quantidade)
{
    public Produto Produto { get; set; } = produto;
    public int Quantidade { get; set; } = quantidade;
}
