namespace MeuAppLogradouros.Models;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Curso { get; set; } = string.Empty;
    public string FotoPath { get; set; } = string.Empty;
}