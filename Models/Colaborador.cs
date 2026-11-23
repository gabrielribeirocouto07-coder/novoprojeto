namespace MeuAppLogradouros.Models;

public class Colaborador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public string FotoPath { get; set; } = string.Empty;

    // Regime de contratação: "CLT" (8h) ou "Estagio" (6h)
    public string TipoContrato { get; set; } = "CLT"; 
}