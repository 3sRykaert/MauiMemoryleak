using BarcodeScanning;
using Mopups.Interfaces;
using Mopups.Pages;

namespace BarcodeScanningDemo.Pages;

public partial class ScanPagePopup : PopupPage
{
    private readonly IPopupNavigation _popupNavigation;
    private readonly BarcodeDrawable _drawable = new();

    public ScanPagePopup(IPopupNavigation popupNavigation)
    {
        _popupNavigation = popupNavigation;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        await Methods.AskForRequiredPermissionAsync();
        base.OnAppearing();

        Barcode.CameraEnabled = true;
        Barcode.TorchOn = App.TorchOn;
        Barcode.VibrationOnDetected = App.VibrationOnDetected;
        Barcode.AimMode = App.AimMode;

        SetFlashLightButtonSource();
        SetVibrateButtonSource();
        SetAimButtonSource();

        Graphics.Drawable = _drawable;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (Barcode.CameraEnabled)
            Barcode.CameraEnabled = false;
    }

    protected override bool OnBackButtonPressed()
    {
        _popupNavigation.PopAsync();
        return true;
    }

    private void CameraView_OnDetectionFinished(object sender, OnDetectionFinishedEventArg e)
    {
        _drawable.BarcodeResults = e.BarcodeResults;
        Graphics.Invalidate();
    }

    private void TorchButton_Clicked(object sender, EventArgs e)
    {
        Barcode.TorchOn = !Barcode.TorchOn;
        App.TorchOn = Barcode.TorchOn;
        SetFlashLightButtonSource();
    }

    private void VibrateButton_Clicked(object sender, EventArgs e)
    {
        Barcode.VibrationOnDetected = !Barcode.VibrationOnDetected;
        App.VibrationOnDetected = Barcode.VibrationOnDetected;
        SetVibrateButtonSource();
    }

    private void AimButton_Clicked(object? sender, EventArgs e)
    {
        Barcode.AimMode = !Barcode.AimMode;
        App.AimMode = Barcode.AimMode;
        SetAimButtonSource();
    }

    private void SetFlashLightButtonSource()
    {
        FlashLightButton.Source = Barcode.TorchOn ? "flashlight_on.svg" : "flashlight_off.svg";
    }

    private void SetVibrateButtonSource()
    {
        VibrateButton.Source = Barcode.VibrationOnDetected ? "vibrate_on.svg" : "vibrate_off.svg";
    }

    private void SetAimButtonSource()
    {
        AimButton.Source = Barcode.AimMode ? "aimmode_aim.svg" : "aimmode_find.svg";
    }
}