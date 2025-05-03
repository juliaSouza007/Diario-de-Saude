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
            Console.WriteLine(">>> HistoricoView: CONSTRUTOR chamado");

            InitializeComponent();

            _viewModel = new HistoricoViewModel();
            Console.WriteLine(">>> HistoricoViewModel instanciado");

            DataContext = _viewModel;

            this.AttachedToVisualTree += async (_, __) =>
            {
                Console.WriteLine(">>> AttachedToVisualTree acionado");

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
    }
}