using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class AlunoListPage : ContentPage
{
    public AlunoListPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarLista();
    }

    private void CarregarLista(string filtro = "")
    {
        var dados = AlunoRepository.Lista.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(filtro))
        {
            dados = dados.Where(x => x.CPF.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                                     x.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase));
        }
        CollectionAlunos.ItemsSource = dados.ToList();
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e) => CarregarLista(e.NewTextValue);

    private async void OnAddClicked(object sender, EventArgs e) => await Navigation.PushAsync(new AlunoDetailPage());

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Aluno aluno)
        {
            CollectionAlunos.SelectedItem = null;
            await Navigation.PushAsync(new AlunoDetailPage(aluno));
        }
    }
}