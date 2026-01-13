namespace MauiMemoryleak.Pages;

public partial class MasterPage : FlyoutPage
{
    private readonly INavigationService _navigationService;

    public MasterPage(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        DisplayAlertAsync("OnBackButtonPressed", "You have been pressed BackButton", "OK");
        return true; //swallow the back button press
    }

    private async void ButtonStartPage_OnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavigateAsync<StartPage>();
    }
}