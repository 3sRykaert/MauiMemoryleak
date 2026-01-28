using Mopups.Interfaces;

namespace BarcodeScanningDemo.Pages;

public partial class StartPage : ContentPage
{
    private readonly IPopupNavigation _popupNavigation;

    public StartPage(IPopupNavigation popupNavigation)
    {
        _popupNavigation = popupNavigation;
        InitializeComponent();
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void ButtonContinuousScanning_OnClicked(object? sender, EventArgs e)
    {
        App.ScanMode = ScanMode.Continuous;
        var popup = ServiceHelper.GetService<ScanAndClosePagePopup>();
        await _popupNavigation.PushAsync(popup);

        await popup.ResultFromScan;

        await _popupNavigation.PopAsync();
    }

    private async void ButtonScanAndClose_OnClicked(object? sender, EventArgs e)
    {
        App.ScanMode = ScanMode.ScanAndClose;
        var popup = ServiceHelper.GetService<ScanAndClosePagePopup>();
        await _popupNavigation.PushAsync(popup);

        var scannedQr = await popup.ResultFromScan;

        await _popupNavigation.PopAsync();
        LastScannedQRCode.Text = scannedQr;
    }

    private async void ButtonScanAndCloseWithDelay_OnClicked(object? sender, EventArgs e)
    {
        App.ScanMode = ScanMode.ScanAndCloseWithDelay;
        App.DelayInMilliSeconds = int.TryParse(DelayInMilliSeconds.Text, out var result)
            ? result
            : 0;

        var popup = ServiceHelper.GetService<ScanAndClosePagePopup>();
        await _popupNavigation.PushAsync(popup);

        var scannedQr = await popup.ResultFromScan;

        await _popupNavigation.PopAsync();
        LastScannedQRCodeWithDelay.Text = scannedQr;
    }
}