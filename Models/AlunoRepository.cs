namespace MeuAppLogradouros.Models;

public static class AlunoRepository
{
    public static List<Aluno> Lista { get; set; } = new List<Aluno>
    {
        new Aluno { Id = 1, Nome = "Gabriel Ribeiro Couto", Matricula = "202601", Curso = "Desenvolvimento de Sistemas" }
    };

    public static void Add(Aluno item)
    {
        if (item.Id == 0)
        {
            item.Id = Lista.Count > 0 ? Lista.Max(x => x.Id) + 1 : 1;
            Lista.Add(item);
        }
    }

    public static void Remove(Aluno item)
    {
        var encontrado = Lista.FirstOrDefault(x => x.Id == item.Id);
        if (encontrado != null) Lista.Remove(encontrado);
    }
}