namespace ArtesaoDeSoftware.Models;

public class Produto(int id, string nome, decimal preco)
{
    public int Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public decimal Preco { get; set; } = preco;
}