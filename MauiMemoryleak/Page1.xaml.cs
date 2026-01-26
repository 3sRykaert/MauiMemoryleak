namespace MauiMemoryleak;

public partial class Page1 : ContentPage
{
    public Page1()
    {
        InitializeComponent();
        Label1.Text = ReturnString.ReturnValue;
    }
}