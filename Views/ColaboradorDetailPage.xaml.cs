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
        TxtCpf.Text = _colaborador.CPF;
        TxtCargo.Text = _colaborador.Cargo;
        TxtDepartamento.Text = _colaborador.Departamento;

        if (!string.IsNullOrEmpty(_colaborador.FotoPath))
            ImgFoto.Source = _colaborador.FotoPath;

        BtnExcluir.IsVisible = true;
    }

    private async void OnTirarFotoClicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult foto = await MediaPicker.Default.CapturePhotoAsync();
            if (foto != null)
            {
                string localFile = Path.Combine(FileSystem.CacheDirectory, foto.FileName);
                using Stream sourceStream = await foto.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFile);
                await sourceStream.CopyToAsync(localFileStream);
                _colaborador.FotoPath = localFile;
                ImgFoto.Source = localFile;
            }
        }
    }

    private async void OnEscolherGaleriaClicked(object sender, EventArgs e)
    {
        FileResult foto = await MediaPicker.Default.PickPhotoAsync();
        if (foto != null)
        {
            string localFile = Path.Combine(FileSystem.CacheDirectory, foto.FileName);
            using Stream sourceStream = await foto.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localFile);
            await sourceStream.CopyToAsync(localFileStream);
            _colaborador.FotoPath = localFile;
            ImgFoto.Source = localFile;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _colaborador.Nome = TxtNome.Text;
        _colaborador.CPF = TxtCpf.Text;
        _colaborador.Cargo = TxtCargo.Text;
        _colaborador.Departamento = TxtDepartamento.Text;

        if (!_isEdicao) ColaboradorRepository.Add(_colaborador);
        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        ColaboradorRepository.Remove(_colaborador);
        await Navigation.PopAsync();
    }
}