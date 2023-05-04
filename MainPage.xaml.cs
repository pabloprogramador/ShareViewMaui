using Microsoft.Maui.Graphics;

namespace ShareViewMaui;

public partial class MainPage : ContentPage
{
    Page lixo = new LixoPage();

	public MainPage()
	{
		InitializeComponent();
        
        //var graphics = (MauiContext as IMauiContext)?.Context?.GetGraphics();
    }

    private void OnOpenShareView(object sender, EventArgs e)
	{
       

        Popup.Open(new DetailsPopup(pgImage));
	}

    private void OnOpenShareView2(object sender, EventArgs e)
    {


        Popup.Open(new DetailsPopup(pgImage2));
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushAsync(lixo);
    }

}

