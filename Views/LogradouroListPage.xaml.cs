using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class LogradouroListPage : ContentPage
{
    public LogradouroListPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarLista();
    }

    private void CarregarLista(string filtro = "")
    {
        var dados = LogradouroRepository.Lista.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            dados = dados.Where(x => x.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                                     x.Bairro.Contains(filtro, StringComparison.OrdinalIgnoreCase));
        }

        CollectionLogradouros.ItemsSource = dados.ToList();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LogradouroDetailPage());
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Logradouro logradouroSelecionado)
        {
            CollectionLogradouros.SelectedItem = null;
            await Navigation.PushAsync(new LogradouroDetailPage(logradouroSelecionado));
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        CarregarLista(e.NewTextValue);
    }
}