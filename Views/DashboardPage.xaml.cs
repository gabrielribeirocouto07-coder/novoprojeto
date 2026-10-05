namespace MeuAppLogradouros.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    private async void OnGoToLogradourosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LogradouroListPage());
    }

    private async void OnGoToSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}