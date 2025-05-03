using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace DiariodeSaude.Views;

public partial class BoasVindasView : UserControl
{
    public BoasVindasView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnRegistroClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.NavegarPara(new RegistroDiarioView());
    }

    private void OnHistoricoClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.NavegarPara(new HistoricoView());
    }
    
    private void OnRelatoriosClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.NavegarPara(new UserControl
            {
                Content = new TextBlock
                {
                    Text = "Tela de Relatórios (em breve)",
                    FontSize = 20,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            });
    }
}
