namespace MauiMemoryleak.Pages;

public partial class DetailPage2 : ContentPage
{

    public DetailPage2()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        if (Application.Current.Windows[0].Page is FlyoutPage flyoutPage)
            flyoutPage.Detail = new NavigationPage(new DetailPage1());
    }
}