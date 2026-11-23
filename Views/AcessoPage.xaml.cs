using MeuAppLogradouros.Models;

namespace MeuAppLogradouros.Views;

public partial class AcessoPage : ContentPage
{
    public AcessoPage()
    {
        InitializeComponent();
        PickerTipo.SelectedIndex = 0;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AtualizarListaPessoas();
        AtualizarHistorico();
    }

    private void OnTipoChanged(object sender, EventArgs e) => AtualizarListaPessoas();

    private void AtualizarListaPessoas()
    {
        if (PickerTipo.SelectedIndex == 0)
            PickerPessoa.ItemsSource = AlunoRepository.Lista.ToList();
        else
            PickerPessoa.ItemsSource = ColaboradorRepository.Lista.ToList();
    }

    private void AtualizarHistorico()
    {
        CollectionAcessos.ItemsSource = AcessoRepository.Registros
            .Where(r => r.DataHoraEntrada.Date == DateTime.Today)
            .OrderByDescending(r => r.DataHoraEntrada)
            .ToList();
    }

    private async void OnEntradaClicked(object sender, EventArgs e)
    {
        if (PickerPessoa.SelectedItem == null)
        {
            await DisplayAlert("Atenção", "Selecione uma pessoa primeiro.", "OK");
            return;
        }

        if (PickerTipo.SelectedIndex == 0 && PickerPessoa.SelectedItem is Aluno aluno)
        {
            // Regra Aluno 1: Validar matrícula ativa
            if (!aluno.MatriculaAtiva || aluno.DataVencimentoPlano < DateTime.Now)
            {
                await DisplayAlert("Acesso Negado", "Matrícula inativa ou plano vencido!", "OK");
                return;
            }

            // Verifica se já está lá dentro
            if (AcessoRepository.Registros.Any(r => r.TipoPessoa == "Aluno" && r.PessoaId == aluno.Id && r.DataHoraSaida == null))
            {
                await DisplayAlert("Aviso", "Este aluno já registrou entrada e ainda não saiu.", "OK");
                return;
            }

            // Regra Aluno 2: Mostrar tempo restante de plano
            int diasRestantes = (aluno.DataVencimentoPlano.Date - DateTime.Today).Days;
            
            AcessoRepository.Registros.Add(new RegistroAcesso
            {
                Id = AcessoRepository.Registros.Count + 1,
                TipoPessoa = "Aluno",
                PessoaId = aluno.Id,
                NomePessoa = aluno.Nome,
                DataHoraEntrada = DateTime.Now
            });

            await DisplayAlert("Entrada Liberada", $"Bem-vindo(a), {aluno.Nome}!\nVocê ainda possui {diasRestantes} dias de plano ativo.", "OK");
        }
        else if (PickerTipo.SelectedIndex == 1 && PickerPessoa.SelectedItem is Colaborador colab)
        {
            if (AcessoRepository.Registros.Any(r => r.TipoPessoa == "Colaborador" && r.PessoaId == colab.Id && r.DataHoraSaida == null))
            {
                await DisplayAlert("Aviso", "Este colaborador já registrou entrada e ainda não saiu.", "OK");
                return;
            }

            // Regra Colaborador 1: Validar limite diário (8h CLT / 6h Estágio)
            double limiteHoras = colab.TipoContrato.Equals("Estagio", StringComparison.OrdinalIgnoreCase) ? 6.0 : 8.0;
            
            double horasTrabalhadasHoje = AcessoRepository.Registros
                .Where(r => r.TipoPessoa == "Colaborador" && r.PessoaId == colab.Id && r.DataHoraEntrada.Date == DateTime.Today && r.DataHoraSaida != null)
                .Sum(r => r.Duracao.TotalHours);

            if (horasTrabalhadasHoje >= limiteHoras)
            {
                await DisplayAlert("Acesso Bloqueado", $"Limite diário de {limiteHoras}h ({colab.TipoContrato}) já atingido hoje!", "OK");
                return;
            }

            AcessoRepository.Registros.Add(new RegistroAcesso
            {
                Id = AcessoRepository.Registros.Count + 1,
                TipoPessoa = "Colaborador",
                PessoaId = colab.Id,
                NomePessoa = colab.Nome,
                DataHoraEntrada = DateTime.Now
            });

            await DisplayAlert("Entrada Registrada", $"Bom trabalho, {colab.Nome}!\nContrato: {colab.TipoContrato} (Limite: {limiteHoras}h/dia).", "OK");
        }

        AtualizarHistorico();
    }

    private async void OnSaidaClicked(object sender, EventArgs e)
    {
        if (PickerPessoa.SelectedItem == null)
        {
            await DisplayAlert("Atenção", "Selecione uma pessoa primeiro.", "OK");
            return;
        }

        if (PickerTipo.SelectedIndex == 0 && PickerPessoa.SelectedItem is Aluno aluno)
        {
            var registroAberto = AcessoRepository.Registros
                .FirstOrDefault(r => r.TipoPessoa == "Aluno" && r.PessoaId == aluno.Id && r.DataHoraSaida == null);

            if (registroAberto == null)
            {
                await DisplayAlert("Erro", "Nenhuma entrada em aberto encontrada para este aluno.", "OK");
                return;
            }

            registroAberto.DataHoraSaida = DateTime.Now;

            // Regra Aluno 3: Mostrar tempo de permanência na saída
            await DisplayAlert("Saída Registrada", $"Até logo, {aluno.Nome}!\nTempo de permanência nesta sessão: {registroAberto.DuracaoFormatada}.", "OK");
        }
        else if (PickerTipo.SelectedIndex == 1 && PickerPessoa.SelectedItem is Colaborador colab)
        {
            var registroAberto = AcessoRepository.Registros
                .FirstOrDefault(r => r.TipoPessoa == "Colaborador" && r.PessoaId == colab.Id && r.DataHoraSaida == null);

            if (registroAberto == null)
            {
                await DisplayAlert("Erro", "Nenhuma entrada em aberto encontrada para este colaborador.", "OK");
                return;
            }

            registroAberto.DataHoraSaida = DateTime.Now;

            // Regra Colaborador 2: Somar todos os registros do dia na saída
            TimeSpan somaTotalDia = TimeSpan.FromTicks(AcessoRepository.Registros
                .Where(r => r.TipoPessoa == "Colaborador" && r.PessoaId == colab.Id && r.DataHoraEntrada.Date == DateTime.Today && r.DataHoraSaida != null)
                .Sum(r => r.Duracao.Ticks));

            string somaFormatada = $"{(int)somaTotalDia.TotalHours:D2}h {somaTotalDia.Minutes:D2}m {somaTotalDia.Seconds:D2}s";

            await DisplayAlert("Saída Registrada", 
                $"Saída confirmada para {colab.Nome}!\n" +
                $"Permanência atual: {registroAberto.DuracaoFormatada}\n" +
                $"Soma total acumulada hoje: {somaFormatada}", "OK");
        }

        AtualizarHistorico();
    }
}