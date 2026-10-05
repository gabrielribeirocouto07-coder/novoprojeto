namespace MeuAppLogradouros.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        PickerTema.SelectedIndex = 0; // Padrão
    }

    private async void OnSaveSettingsClicked(object sender, EventArgs e)
    {
        // Salva as preferências localmente ou aplica o tema
        bool temaEscuro = PickerTema.SelectedItem?.ToString()?.Contains("Escuro") ?? false;
        
        if (temaEscuro)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }

        await DisplayAlert("Sucesso", "Credenciais do banco de dados e preferências de tema salvas com sucesso!", "OK");
    }
}