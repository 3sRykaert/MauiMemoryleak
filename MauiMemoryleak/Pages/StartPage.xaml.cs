namespace MauiMemoryleak.Pages;

public partial class StartPage : ContentPage
{
    public StartPage()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        Application.Current?.Windows[0].Page = new MasterPage
        {
            Detail = new NavigationPage(new DetailPage1())
        };
        //await _navigationService.NavigateAsync<DetailPage1>();
    }
}