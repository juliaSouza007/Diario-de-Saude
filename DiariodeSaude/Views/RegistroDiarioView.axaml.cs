using Avalonia.Controls;
using System;
using DiariodeSaude.ViewsModels;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using System.Threading.Tasks;

namespace DiariodeSaude.Views;

public partial class RegistroDiarioView : UserControl
{
    private readonly RegistroDiarioLinq registroDiarioLinq = new RegistroDiarioLinq();
    private readonly HumorLinq humorLinq = new HumorLinq();
    private readonly AlimentacaoLinq alimentacaoLinq = new AlimentacaoLinq();
    private readonly SonoLinq sonoLinq = new SonoLinq();
    private readonly AtividadeFisicaLinq atividadeFisicaLinq = new AtividadeFisicaLinq();

    public RegistroDiarioView()
    {
         InitializeComponent();
    }
    private void OnVoltarClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.NavegarPara(new BoasVindasView());
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
          
            string? descricaoSono = null;
            if (this.FindControl<RadioButton>("SonoBom")?.IsChecked == true) descricaoSono = "Boa";
            else if (this.FindControl<RadioButton>("SonoMedio")?.IsChecked == true) descricaoSono = "Média";
            else if (this.FindControl<RadioButton>("SonoRuim")?.IsChecked == true) descricaoSono = "Ruim";
            else if (string.IsNullOrEmpty(descricaoSono))
            {
                MensagemErro("Selecione a qualidade do sono.");
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

            int duracao = (int)(this.FindControl<NumericUpDown>("duracaoInput")?.Value ?? 0);
            if (duracao <= 0)
            {
                MensagemErro("Informe uma duração válida para a atividade física.");
                return;
            }

            var novoHumor = new Humor { Descricao = descricaoHumor };
            var humorId = await humorLinq.AdicionarHumorAsync(novoHumor);

            var novoSono = new QualidadeSono { Descricao = descricaoSono };
            var sonoId = await sonoLinq.AdicionarSonoAsync(novoSono);

            var novaAlimentacao = new Alimentacao { Descricao = alimentacaoInput.Text.Trim() };
            var alimentacaoId = await alimentacaoLinq.AdicionarAlimentacaoAsync(novaAlimentacao);

            var novaAtividadeFisca = new AtividadeFisica
            {
                TipoAtividade = atvFisicaInput.Text.Trim(),
                DuracaoMinutos = duracao
            };
            var atividadeFisicaId = await atividadeFisicaLinq.AdicionarAtividadeFisicaAsync(novaAtividadeFisca);

            var novoRegistro = new RegistroDiario
            {
                Data = DateTime.Now.Date,
                HumorId = humorId,
                SonoId = sonoId,
                AlimentacaoId = alimentacaoId,
                AtividadeFisicaId = atividadeFisicaId
            };
            await registroDiarioLinq.AdicionarRegistroDiarioAsync(novoRegistro);

            MensagemSucesso("Registro salvo com sucesso!");

            LimpaCampos();
        }
        catch (Exception ex)
        {
            MensagemErro($"Erro ao salvar: {ex.Message}");
        }
    }

    private void LimpaCampos() 
    {
        alimentacaoInput.Text = "";
        atvFisicaInput.Text = "";
        var duracaoControl = this.FindControl<NumericUpDown>("duracaoInput");
        if (duracaoControl is not null) duracaoControl.Value = 0;


        foreach (var rb in new[] { "HumorFeliz", "HumorBom", "HumorRegular", "HumorRuim", "HumorPessimo", "SonoBom", "SonoMedio", "SonoRuim" })
        {
            var radio = this.FindControl<RadioButton>(rb);
            if (radio != null) radio.IsChecked = false;
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