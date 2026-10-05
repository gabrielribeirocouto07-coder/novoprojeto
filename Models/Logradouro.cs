namespace MeuAppLogradouros.Models;

public class Logradouro
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    
    // Suporta tanto Cep quanto CEP para evitar qualquer erro de compilação
    public string Cep { get; set; } = string.Empty;
    public string CEP 
    { 
        get => Cep; 
        set => Cep = value; 
    }
    
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string UF { get; set; } = string.Empty;
}