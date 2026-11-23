namespace MeuAppLogradouros.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage() => InitializeComponent();

    private async void OnAcessoClicked(object sender, EventArgs e) => await Navigation.PushAsync(new AcessoPage());
    private async void OnAlunosClicked(object sender, EventArgs e) => await Navigation.PushAsync(new AlunoListPage());
    private async void OnColaboradoresClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ColaboradorListPage());
    private async void OnLogradourosClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LogradouroListPage());
    private async void OnSettingsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new SettingsPage());
}