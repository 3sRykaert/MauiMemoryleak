using BarcodeScanning;
using Mopups.Pages;

namespace BarcodeScanningDemo.Pages;

public partial class ScanAndClosePagePopupWithDelay : PopupPage
{
    private readonly BarcodeDrawable _drawable = new();
    private readonly TaskCompletionSource<string> _taskCompletionSource = new();
    public Task<string> ResultFromScan => _taskCompletionSource.Task;

    public ScanAndClosePagePopupWithDelay()
    {
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

        Barcode.OnDetectionFinished += CameraView_OnDetectionFinished;
        Graphics.Drawable = _drawable;
    }

    protected override void OnDisappearing()
    {
        Barcode.OnDetectionFinished -= CameraView_OnDetectionFinished;
        Barcode.CameraEnabled = false;
        base.OnDisappearing();
    }

    protected override bool OnBackButtonPressed()
    {
        Barcode.OnDetectionFinished -= CameraView_OnDetectionFinished;
        _taskCompletionSource.SetResult(string.Empty);
        return true;
    }

    private async void CameraView_OnDetectionFinished(object? sender, OnDetectionFinishedEventArg e)
    {
        var results = e.BarcodeResults;
        _drawable.BarcodeResults = results;
        Graphics.Invalidate();

        if (results?.Count > 0)
        {
            var bardCode = results.First().DisplayValue;
            Barcode.OnDetectionFinished -= CameraView_OnDetectionFinished;
            await Task.Delay(TimeSpan.FromMilliseconds(App.DelayInMilliSeconds));// Small delay to allow user to see the detected barcode
            _taskCompletionSource.SetResult(bardCode);
        }
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