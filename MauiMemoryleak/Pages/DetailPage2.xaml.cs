namespace MauiMemoryleak.Pages;

public partial class DetailPage2 : ContentPage
{
    private readonly INavigationService _navigationService;

    public DetailPage2(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavigateAsync<DetailPage1>();
    }
}