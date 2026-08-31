namespace MauiMemoryleak.Pages;

public partial class MasterPage : FlyoutPage
{
    public MasterPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        DisplayAlertAsync("OnBackButtonPressed", "You have pressed BackButton", "OK");
        return true; //swallow the back button press
    }
}