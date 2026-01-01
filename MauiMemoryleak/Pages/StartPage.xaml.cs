namespace MauiMemoryleak.Pages;

public partial class StartPage : ContentPage
{
    private readonly INavigationService _navigationService;

    public StartPage(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavigateAsync<DetailPage1>();
    }
}