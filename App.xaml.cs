using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;

namespace ShareViewMaui;

public partial class App : Application
{
    public static LixoPage minhaPagina = new LixoPage();
    public App()
	{
		InitializeComponent();
        
        
        // Carregue o conteúdo do arquivo XAML em sua página
        
        global::Microsoft.Maui.Controls.Xaml.Extensions.LoadFromXaml(minhaPagina, typeof(LixoPage));

        MainPage = new AppShell();
	}
}

