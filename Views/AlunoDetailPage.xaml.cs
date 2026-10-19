using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class AlunoDetailPage : ContentPage
{
    private Aluno _aluno;
    private bool _isEdicao;

    public AlunoDetailPage()
    {
        InitializeComponent();
        _aluno = new Aluno();
        _isEdicao = false;
        BtnExcluir.IsVisible = false;
    }

    public AlunoDetailPage(Aluno aluno)
    {
        InitializeComponent();
        _aluno = aluno;
        _isEdicao = true;
        TxtNome.Text = _aluno.Nome;
        TxtCpf.Text = _aluno.CPF;
        TxtMatricula.Text = _aluno.Matricula;
        TxtCurso.Text = _aluno.Curso;

        if (!string.IsNullOrEmpty(_aluno.FotoPath))
            ImgFoto.Source = _aluno.FotoPath;

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
                _aluno.FotoPath = localFile;
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
            _aluno.FotoPath = localFile;
            ImgFoto.Source = localFile;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _aluno.Nome = TxtNome.Text;
        _aluno.CPF = TxtCpf.Text;
        _aluno.Matricula = TxtMatricula.Text;
        _aluno.Curso = TxtCurso.Text;

        if (!_isEdicao) AlunoRepository.Add(_aluno);
        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        AlunoRepository.Remove(_aluno);
        await Navigation.PopAsync();
    }
}