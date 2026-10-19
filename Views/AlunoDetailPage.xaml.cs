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
        TxtMatricula.Text = _aluno.Matricula;
        TxtCurso.Text = _aluno.Curso;
        BtnExcluir.IsVisible = true;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _aluno.Nome = TxtNome.Text;
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