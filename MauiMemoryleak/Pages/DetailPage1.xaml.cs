namespace MauiMemoryleak.Pages;

public partial class DetailPage1 : ContentPage
{
    private readonly INavigationService _navigationService;

    public DetailPage1(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavigateAsync<DetailPage2>();
    }

    private async void ButtonStartPage_OnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavigateAsync<StartPage>();
    }
}