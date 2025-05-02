using Avalonia.Controls;

namespace DiariodeSaude;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void NavegarPara(UserControl tela)
    {
        MainContent.Content = tela;
    }
}
    /*
    private void OnRegistroDiarioClick(object? sender, RoutedEventArgs e)
    {
        var registroDiarioView = new Views.RegistroDiarioView();
        var contentControl = this.FindControl<ContentControl>("registroDiarioContentControl");
        if (contentControl != null) contentControl.Content = registroDiarioView;
    }

    private void VerHistoricoClick(object? sender, RoutedEventArgs e)
    {
        var historico = new Views.HistoricoView();
        var contentControl = this.FindControl<ContentControl>("historicoRegistrosContentControl");
        if (contentControl != null) contentControl.Content = historico;
    }
    */