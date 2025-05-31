using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DiariodeSaude.ViewsModels;

namespace DiariodeSaude.Views
{
    public partial class HistoricoView : UserControl
    {
        private readonly HistoricoViewModel _viewModel;

        public HistoricoView()
        {
            InitializeComponent();
            //RegistrosTeste.InserirRegistros();

            _viewModel = new HistoricoViewModel();

            DataContext = _viewModel;

            this.AttachedToVisualTree += async (_, __) =>
            {
                await _viewModel.CarregarRegistrosAsync();
            };
        }

        private void OnVoltarClick(object? sender, RoutedEventArgs e)
        {
            if (VisualRoot is MainWindow mainWindow)
            {
                mainWindow.NavegarPara(new BoasVindasView());
            }
        }

        private void EditarRegistroClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is RegistroCompletoDTO registro)
            {
                var editarView = new EditarRegistroView(registro); // Passa o registro
                if (VisualRoot is MainWindow main)
                {
                    main.NavegarPara(editarView);
                }
            }
        }

        private async void ExcluirRegistroClick(object? sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is RegistroCompletoDTO registro)
            {
                try
                {
                    var registroDiarioLinq = new RegistroDiarioLinq();
                    await registroDiarioLinq.RemoverRegistroDiarioAsync(registro.RegistroId);

                    await _viewModel.CarregarRegistrosAsync(); // Atualiza a lista na tela
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao excluir: {ex.Message}");
                }
            }
        }       
    }
}