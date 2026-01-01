namespace MauiMemoryleak.Pages;

public partial class DetailPage1 : ContentPage
{
    public DetailPage1()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        var navigationService = ServiceHelper.GetService<INavigationService>();
        await navigationService.NavigateAsync<DetailPage2>();
    }
}