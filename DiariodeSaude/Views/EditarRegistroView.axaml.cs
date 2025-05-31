using Avalonia.Controls;
using Avalonia.Interactivity;
using Data;
using DiariodeSaude.ViewsModels;
using LinqToDB;
using System;
using Avalonia.Media;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace DiariodeSaude.Views;

public partial class EditarRegistroView : UserControl
{
    private readonly RegistroDiarioLinq registroDiarioLinq = new RegistroDiarioLinq();
    private readonly HumorLinq humorLinq = new HumorLinq();
    private readonly AlimentacaoLinq alimentacaoLinq = new AlimentacaoLinq();
    private readonly SonoLinq sonoLinq = new SonoLinq();
    private readonly AtividadeFisicaLinq atividadeFisicaLinq = new AtividadeFisicaLinq();

    private readonly RegistroCompletoDTO registroParaEditar;

    public EditarRegistroView(RegistroCompletoDTO registro)
    {
        InitializeComponent();
        registroParaEditar = registro;
        PreencherCamposParaEdicao();
    }

    private void PreencherCamposParaEdicao()
    {
        var humorRadio = this.FindControl<RadioButton>(registroParaEditar.Humor!);
        if (humorRadio != null)
            humorRadio.IsChecked = true;

        sonoInput.Text = registroParaEditar.Sono;
        alimentacaoInput.Text = registroParaEditar.Alimentacao;
        atvFisicaInput.Text = registroParaEditar.Atividade;
        duracaoInput.Text = registroParaEditar.Tempo.ToString();
    }

    private void OnVoltarClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.NavegarPara(new HistoricoView());
        }
    }

    private async void OnSalvarClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            string? descricaoHumor = null;
            if (this.FindControl<RadioButton>("HumorFeliz")?.IsChecked == true) descricaoHumor = "HumorFeliz";
            else if (this.FindControl<RadioButton>("HumorBom")?.IsChecked == true) descricaoHumor = "HumorBom";
            else if (this.FindControl<RadioButton>("HumorRegular")?.IsChecked == true) descricaoHumor = "HumorRegular";
            else if (this.FindControl<RadioButton>("HumorRuim")?.IsChecked == true) descricaoHumor = "HumorRuim";
            else if (this.FindControl<RadioButton>("HumorPessimo")?.IsChecked == true) descricaoHumor = "HumorPessimo";
            else if (string.IsNullOrEmpty(descricaoHumor))
            {
                MensagemErro("Selecione seu humor.");
                return;
            }

            if (!int.TryParse(sonoInput.Text, out var sono) || sono < 0 || sono > 10)
            {
                MensagemErro("A qualidade do sono deve estar entre 0 e 10.");
                return;
            }

            if (string.IsNullOrWhiteSpace(alimentacaoInput.Text))
            {
                MensagemErro("Informe como foi sua alimentação.");
                return;
            }

            if (string.IsNullOrWhiteSpace(atvFisicaInput.Text))
            {
                MensagemErro("Informe o tipo de atividade física.");
                return;
            }

            if (!int.TryParse(duracaoInput.Text, out var duracao) || duracao < 0)
            {
                MensagemErro("Informe uma duração válida para a atividade física.");
                return;
            }

            var novoHumor = new Humor { Descricao = descricaoHumor };
            var humorId = await humorLinq.AdicionarHumorAsync(novoHumor);

            var novoSono = new QualidadeSono { Descricao = sono.ToString() };
            var sonoId = await sonoLinq.AdicionarSonoAsync(novoSono);

            var novaAlimentacao = new Alimentacao { Descricao = alimentacaoInput.Text.Trim() };
            var alimentacaoId = await alimentacaoLinq.AdicionarAlimentacaoAsync(novaAlimentacao);

            var novaAtividadeFisca = new AtividadeFisica
            {
                TipoAtividade = atvFisicaInput.Text.Trim(),
                DuracaoMinutos = duracao
            };
            var atividadeFisicaId = await atividadeFisicaLinq.AdicionarAtividadeFisicaAsync(novaAtividadeFisca);

            var registroAtualizado = new RegistroDiario
            {
                Id = registroParaEditar.RegistroId,
                HumorId = humorId,
                SonoId = sonoId,
                AlimentacaoId = alimentacaoId,
                AtividadeFisicaId = atividadeFisicaId
            };
            await registroDiarioLinq.AtualizarRegistroDiarioAsync(registroAtualizado);

            MensagemSucesso("Registro atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            MensagemErro($"Erro ao salvar: {ex.Message}");
        }
    }

    private async void MensagemErro(string mensagem)
    {
        mensagemStatus.Text = mensagem;
        mensagemStatus.Foreground = Brushes.Red;
        mensagemStatus.IsVisible = true;
        await OcultarMensagemDepoisDeDelay();
    }

    private async void MensagemSucesso(string mensagem)
    {
        mensagemStatus.Text = mensagem;
        mensagemStatus.Foreground = Brushes.Green;
        mensagemStatus.IsVisible = true;
        await OcultarMensagemDepoisDeDelay();
    }

    private async Task OcultarMensagemDepoisDeDelay()
    {
        await Task.Delay(3000);
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            mensagemStatus.IsVisible = false;
        });
    }
}