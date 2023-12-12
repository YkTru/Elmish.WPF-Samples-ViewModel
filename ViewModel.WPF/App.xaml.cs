using System.Windows;

namespace ViewModel.WPF;
public partial class App : Application
{
    public App()
    {
        Activated += StartElmish;
    }

    private void StartElmish(object sender, EventArgs e)
    {
        Activated -= StartElmish;
        ViewModel.Main.main(MainWindow);
    }
}

