using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class AlunoListPage : ContentPage
{
    public AlunoListPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CollectionAlunos.ItemsSource = AlunoRepository.Lista.ToList();
    }

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