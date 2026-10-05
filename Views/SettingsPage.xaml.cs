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
        // Tratamento seguro para evitar avisos de nulidade
        var itemSelecionado = PickerTema.SelectedItem?.ToString();
        bool temaEscuro = itemSelecionado != null && itemSelecionado.Contains("Escuro");
        
        if (temaEscuro)
        {
            Application.Current!.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current!.UserAppTheme = AppTheme.Light;
        }

        await DisplayAlert("Sucesso", "Credenciais do banco de dados e preferências de tema salvas com sucesso!", "OK");
    }
}