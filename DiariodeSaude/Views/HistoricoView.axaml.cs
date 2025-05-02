using Avalonia.Controls;
using Avalonia.Interactivity;
using DiariodeSaude.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace DiariodeSaude.Views
{
    public partial class HistoricoView : Window
    {
        private readonly RegistroDiarioLinq _registroDiarioLinq = new RegistroDiarioLinq();

        public HistoricoView()
        {
            InitializeComponent();
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                //await CarregarRegistros("diario");
            });
        }

        private async void OnFiltrarClick(object? sender, RoutedEventArgs e)
        {
            string periodo = (PeriodoComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "diario";
            //await CarregarRegistros(periodo.ToLower());
        }

        /*
        private async Task CarregarRegistros(string periodo)
        {
            try
            {
                var registros = await ViewsModels.RegistroDiarioLinq.BuscarRegistrosComRelacionamentosAsync();

                if (registros == null || registros.Count == 0)
                {
                    mensagemStatus.Text = "Sem registros!";
                    mensagemStatus.Foreground = Brushes.Red;
                    mensagemStatus.IsVisible = true;
                    dataGridRegistros.ItemsSource = null;
                    return;
                } 

                DateTime dataLimite = periodo switch
                {
                    "diario" => DateTime.Now.Date,
                    "semanal" => DateTime.Now.Date.AddDays(-7),
                    "mensal" => DateTime.Now.Date.AddMonths(-1),
                    _ => DateTime.MinValue
                };

                var registrosFiltrados = registros
                    .Where(r => r.Data >= dataLimite)
                    .OrderByDescending(r => r.Data)
                    .Select(r => new
                    {
                        Data = r.Data.ToString("dd/MM/yyyy HH:mm"),
                        Humor = r.Humor?.Descricao ?? "",
                        Sono = r.Sono?.Descricao.ToString() ?? "",
                        Atividade = r.AtividadeFisica?.TipoAtividade ?? "",
                        Tempo = r.AtividadeFisica?.DuracaoMinutos.ToString() ?? ""
                    })
                    .ToList();

                if (registrosFiltrados.Count == 0)
                {
                    mensagemStatus.Text = "Sem registros!";
                    mensagemStatus.Foreground = Brushes.Red;
                    mensagemStatus.IsVisible = true;
                    dataGridRegistros.ItemsSource = null;
                    return;
                }

                mensagemStatus.IsVisible = false;
                dataGridRegistros.ItemsSource = registrosFiltrados;
            } 
            catch (Exception ex)
            {
                mensagemStatus.Text = $"Erro: {ex.Message}";
                mensagemStatus.Foreground = Brushes.Red;
                mensagemStatus.IsVisible = true;
            } 
        } */
    }
}
