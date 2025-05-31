using Avalonia.Controls;
using Tmds.DBus.Protocol;

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
