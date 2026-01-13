namespace MauiMemoryleak.Pages;

public partial class MasterPage : FlyoutPage
{
    public MasterPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        DisplayAlertAsync("OnBackButtonPressed", "You have been pressed BackButton", "OK");
        return true; //swallow the back button press
    }

    private async void ButtonStartPage_OnClicked(object? sender, EventArgs e)
    {
        var navigationService = ServiceHelper.GetService<INavigationService>();
        await navigationService.NavigateAsync<StartPage>();
    }
}