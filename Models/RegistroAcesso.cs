namespace MeuAppLogradouros.Models;

public class RegistroAcesso
{
    public int Id { get; set; }
    public string TipoPessoa { get; set; } = string.Empty; // "Aluno" ou "Colaborador"
    public int PessoaId { get; set; }
    public string NomePessoa { get; set; } = string.Empty;
    public DateTime DataHoraEntrada { get; set; }
    public DateTime? DataHoraSaida { get; set; }

    public string StatusTexto => DataHoraSaida == null 
        ? $"🟢 Na academia desde {DataHoraEntrada:HH:mm:ss}" 
        : $"🔴 Saiu às {DataHoraSaida:HH:mm:ss} (Duração: {DuracaoFormatada})";

    public TimeSpan Duracao => (DataHoraSaida ?? DateTime.Now) - DataHoraEntrada;

    public string DuracaoFormatada => $"{(int)Duracao.TotalHours:D2}h {Duracao.Minutes:D2}m {Duracao.Seconds:D2}s";
}

public static class AcessoRepository
{
    public static List<RegistroAcesso> Registros { get; set; } = new List<RegistroAcesso>();
}