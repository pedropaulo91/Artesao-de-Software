namespace ArtesaoDeSoftware.Models;

public class Cliente(int id, string nome, string email, int anosFidelidade)
{
    public int Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email;
    public int AnosFidelidade { get; set; } = anosFidelidade;
}