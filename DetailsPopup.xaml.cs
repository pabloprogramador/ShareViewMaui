namespace ShareViewMaui;

public partial class DetailsPopup : ShareView
{
	private Image _image;
    private Image _oldimage;
    private double _screenHeight = DeviceDisplay.MainDisplayInfo.Height;
    //double _cacheHeight;
    //double _cacheWidth;
    //Rect _cacheRect;
    
    

    public DetailsPopup(Image image)
	{
        
        IsFadeBackground = false;
        _oldimage = image;
		_image = new Image() { Source = image.Source, WidthRequest = image.Width, HeightRequest = image.Height};
		InitializeComponent();
       
        Init();
	}

    public async override Task BeforeOpen()
    {
        //Init();
        base.BeforeOpen();
    }

    private async void Init()
    {
        var result = this.FindByName<Image>("pgTeste");
        System.Diagnostics.Debug.WriteLine(pgImage.ClassId);

        pgImage.Opacity = 0;
        // while(this.Height < 0) { await Task.Delay(30); }

        //pgImage.Bounds.X
        //_cacheHeight = pgImage.HeightRequest;
        //_cacheWidth = pgImage.WidthRequest;
        //_cacheRect = pgImage.GetAbsoluteBounds();

        //var root = pgImage.GetRootBounds();
        //_newImage.Opacity = 1;
        _image.Aspect = Aspect.AspectFill;
        _image.HorizontalOptions = LayoutOptions.Start;
        _image.VerticalOptions = LayoutOptions.Start;
        //_image.HeightRequest = _image.HeightRequest;
        //_image.WidthRequest = _image.WidthRequest;
        _image.TranslationY = _oldimage.GetAbsoluteBounds().Y + (48);
        _image.TranslationX = _oldimage.GetAbsoluteBounds().X;

        pgGrid.Children.Add(_image);

        pgRoot.TranslationY = _screenHeight;

    }

    public async override Task AfterOpen()
    {


        
        WidthTo(_image, pgImage.Width, 800, Easing.CubicInOut);
        HeightTo(_image, pgImage.Height, 800, Easing.CubicInOut);
        _image.TranslateTo(0, 0, 800, Easing.CubicInOut);
        await Task.Delay(100);
        await pgRoot.TranslateTo(0, 0, 700, Easing.CubicOut);
        base.AfterOpen();
    }

    public async override Task BeforeClose()
    {
         base.BeforeClose();
        WidthTo(_image, _oldimage.Width, 800, Easing.CubicInOut);
        HeightTo(_image, _oldimage.Height, 800, Easing.CubicInOut);
        _image.TranslateTo(_oldimage.GetAbsoluteBounds().X, _oldimage.GetAbsoluteBounds().Y+48, 800, Easing.CubicInOut);
        await pgRoot.TranslateTo(0, _screenHeight, 600, Easing.CubicIn);
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
		Close();
    }

     void WidthTo(View view,
                                  double value,
                                  double lenght,
                                  Easing easing,
                                  Action<bool> finished = null,
                                  Action<double> updated = null)
    {
        var animation = new Animation((value) => {
            view.WidthRequest = value;
            updated?.Invoke(value);
        },
                                      view.Width,
                                      value,
                                      easing);

        animation.Commit(view,
                         "WidthToAnimation",
                         16,
                         (uint)lenght,
                         finished: (value, cancelled) => { finished?.Invoke(cancelled); });
    }

     void HeightTo(View view,
                                double value,
                                double lenght,
                                Easing easing,
                                Action<bool> finished = null,
                                Action<double> updated = null)
    {
        var animation = new Animation((value) => {
            view.HeightRequest = value;
            updated?.Invoke(value);
        },
                                      view.Height,
                                      value,
                                      easing);

        animation.Commit(view,
                         "HeightToAnimation",
                         16,
                         (uint)lenght,
                         finished: (value, cancelled) => { finished?.Invoke(cancelled); });
    }

   
}

public static class ViewHelpers
{
    public static Rect GetAbsoluteBounds(this Microsoft.Maui.Controls.View element)
    {
        Element looper = element;

        var absoluteX = element.X + element.Margin.Top;
        var absoluteY = element.Y + element.Margin.Left;

        // Add logic to handle titles, headers, or other non-view bars

        while (looper.Parent != null)
        {
            looper = looper.Parent;
            if (looper is Microsoft.Maui.Controls.View v)
            {
                absoluteX += v.X + v.Margin.Top;
                absoluteY += v.Y + v.Margin.Left;
            }
        }

        return new Rect(absoluteX, absoluteY, element.Width, element.Height);
    }

    public static Rect GetRootBounds(this Microsoft.Maui.Controls.View element)
    {
        Element looper = element;

        double absoluteX = 0;
        double absoluteY = 0;
        Rect rect = new Rect();
        // Add logic to handle titles, headers, or other non-view bars

        while (looper.Parent != null)
        {
            looper = looper.Parent;
            if (looper is Microsoft.Maui.Controls.View v && looper.Parent.Parent==null)
            {
                absoluteX = v.X + v.Margin.Top;
                absoluteY = v.Y + v.Margin.Left;
                rect = new Rect(absoluteX, absoluteY, v.Width, v.Height);
            }
        }

        return rect;
    }
}
