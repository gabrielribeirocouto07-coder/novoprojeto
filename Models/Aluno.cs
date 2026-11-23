namespace MeuAppLogradouros.Models;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Curso { get; set; } = string.Empty;
    public string FotoPath { get; set; } = string.Empty;
    
    // Novos campos para validação de academia
    public bool MatriculaAtiva { get; set; } = true;
    public DateTime DataVencimentoPlano { get; set; } = DateTime.Now.AddDays(30);
}