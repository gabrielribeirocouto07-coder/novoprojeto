using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class ColaboradorListPage : ContentPage
{
    public ColaboradorListPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarLista();
    }

    private void CarregarLista(string filtro = "")
    {
        var dados = ColaboradorRepository.Lista.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(filtro))
        {
            dados = dados.Where(x => x.CPF.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                                     x.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase));
        }
        CollectionColaboradores.ItemsSource = dados.ToList();
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e) => CarregarLista(e.NewTextValue);

    private async void OnAddClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ColaboradorDetailPage());

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Colaborador colaborador)
        {
            CollectionColaboradores.SelectedItem = null;
            await Navigation.PushAsync(new ColaboradorDetailPage(colaborador));
        }
    }
}