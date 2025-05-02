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
