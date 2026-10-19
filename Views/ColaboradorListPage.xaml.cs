using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class ColaboradorListPage : ContentPage
{
    public ColaboradorListPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CollectionColaboradores.ItemsSource = ColaboradorRepository.Lista.ToList();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ColaboradorDetailPage());
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Colaborador colaborador)
        {
            CollectionColaboradores.SelectedItem = null;
            await Navigation.PushAsync(new ColaboradorDetailPage(colaborador));
        }
    }
}