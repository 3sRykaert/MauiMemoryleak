namespace MauiMemoryleak.Pages;

public partial class DetailPage2 : ContentPage
{

    public DetailPage2()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        var navigationService = ServiceHelper.GetService<INavigationService>();
        await navigationService.NavigateAsync<DetailPage1>();
    }
}