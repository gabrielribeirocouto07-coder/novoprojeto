using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class ColaboradorDetailPage : ContentPage
{
    private Colaborador _colaborador;
    private bool _isEdicao;

    public ColaboradorDetailPage()
    {
        InitializeComponent();
        _colaborador = new Colaborador();
        _isEdicao = false;
        BtnExcluir.IsVisible = false;
    }

    public ColaboradorDetailPage(Colaborador colaborador)
    {
        InitializeComponent();
        _colaborador = colaborador;
        _isEdicao = true;
        
        TxtNome.Text = _colaborador.Nome;
        TxtCargo.Text = _colaborador.Cargo;
        TxtDepartamento.Text = _colaborador.Departamento;
        BtnExcluir.IsVisible = true;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _colaborador.Nome = TxtNome.Text;
        _colaborador.Cargo = TxtCargo.Text;
        _colaborador.Departamento = TxtDepartamento.Text;

        if (!_isEdicao)
        {
            ColaboradorRepository.Add(_colaborador);
        }

        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmação", "Deseja realmente excluir este colaborador?", "Sim", "Não");
        if (confirmar)
        {
            ColaboradorRepository.Remove(_colaborador);
            await Navigation.PopAsync();
        }
    }
}