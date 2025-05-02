using Avalonia.Controls;
using Avalonia.Interactivity;
using DiariodeSaude; // Para acessar MainWindow
using DiariodeSaude.Views; // Para acessar BoasVindasView

namespace RegistroDiario;

public partial class RegistroDiarioView : UserControl
{
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
}
