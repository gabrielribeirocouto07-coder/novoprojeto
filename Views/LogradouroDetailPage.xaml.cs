using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class LogradouroDetailPage : ContentPage
{
    private Logradouro _logradouro;
    private bool _isEdicao;

    public LogradouroDetailPage()
    {
        InitializeComponent();
        _logradouro = new Logradouro();
        _isEdicao = false;
        BtnExcluir.IsVisible = false; // Esconde o botão de excluir se for novo cadastro
    }

    public LogradouroDetailPage(Logradouro logradouro)
    {
        InitializeComponent();
        _logradouro = logradouro;
        _isEdicao = true;

        // Preenche os campos para edição
        TxtNome.Text = _logradouro.Nome;
        TxtBairro.Text = _logradouro.Bairro;
        TxtCep.Text = _logradouro.CEP;
        BtnExcluir.IsVisible = true;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtNome.Text))
        {
            await DisplayAlert("Erro", "O nome do logradouro é obrigatório (use seu nome completo).", "OK");
            return;
        }

        _logradouro.Nome = TxtNome.Text;
        _logradouro.Bairro = TxtBairro.Text;
        _logradouro.CEP = TxtCep.Text;

        if (!_isEdicao)
        {
            LogradouroRepository.Add(_logradouro);
        }

        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmação", "Deseja realmente excluir este logradouro?", "Sim", "Não");
        if (confirmar)
        {
            LogradouroRepository.Remove(_logradouro);
            await Navigation.PopAsync();
        }
    }
}