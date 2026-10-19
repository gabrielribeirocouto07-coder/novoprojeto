namespace MeuAppLogradouros.Models;

public static class ColaboradorRepository
{
    public static List<Colaborador> Lista { get; set; } = new List<Colaborador>
    {
        new Colaborador { Id = 1, Nome = "Gabriel Ribeiro Couto", Cargo = "Tech Lead", Departamento = "TI" }
    };

    public static void Add(Colaborador item)
    {
        if (item.Id == 0)
        {
            item.Id = Lista.Count > 0 ? Lista.Max(x => x.Id) + 1 : 1;
            Lista.Add(item);
        }
    }

    public static void Remove(Colaborador item)
    {
        var encontrado = Lista.FirstOrDefault(x => x.Id == item.Id);
        if (encontrado != null) Lista.Remove(encontrado);
    }
}