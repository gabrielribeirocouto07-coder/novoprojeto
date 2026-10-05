using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Models;

public static class LogradouroRepository
{
    public static List<Logradouro> Lista { get; set; } = new List<Logradouro>
    {
        // Item inicial padrão com o seu nome e bairro exigido
        new Logradouro { Id = 1, Nome = "Gabriel Ribeiro Couto", Bairro = "Abc Bolinhas", CEP = "88500-000" }
    };

    public static void Add(Logradouro item)
    {
        if (item.Id == 0)
        {
            item.Id = Lista.Count > 0 ? Lista.Max(x => x.Id) + 1 : 1;
            Lista.Add(item);
        }
    }

    public static void Remove(Logradouro item)
    {
        var encontrado = Lista.FirstOrDefault(x => x.Id == item.Id);
        if (encontrado != null)
        {
            Lista.Remove(encontrado);
        }
    }
}