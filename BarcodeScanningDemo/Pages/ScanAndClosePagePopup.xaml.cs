using BarcodeScanning;
using Mopups.Pages;

namespace BarcodeScanningDemo.Pages;

public partial class ScanAndClosePagePopup : PopupPage
{
    private readonly BarcodeDrawable _drawable = new();
    private readonly TaskCompletionSource<string> _taskCompletionSource = new();
    public Task<string> ResultFromScan => _taskCompletionSource.Task;

    public ScanAndClosePagePopup()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        await Methods.AskForRequiredPermissionAsync();
        base.OnAppearing();

        Barcode.CameraEnabled = true;
        Barcode.OnDetectionFinished += CameraView_OnDetectionFinished;
        Graphics.Drawable = _drawable;
        Graphics.InputTransparent = App.ScanMode != ScanMode.ContinuousWithSelection;

        if (App.TorchOn)
            await Task.Delay(TimeSpan.FromMilliseconds(100)); //needed it seems
        Barcode.TorchOn = App.TorchOn;
        Barcode.VibrationOnDetected = App.VibrationOnDetected;
        Barcode.AimMode = App.AimMode;

        SetFlashLightButtonSource();
        SetVibrateButtonSource();
        SetAimButtonSource();
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

        switch (App.ScanMode)
        {
            case ScanMode.Continuous:
            case ScanMode.ContinuousWithSelection:
                break;
            case ScanMode.ScanAndClose:
            case ScanMode.ScanAndCloseWithDelay:
                if (results?.Count > 0)
                {
                    var bardCode = results.First().DisplayValue;
                    Barcode.OnDetectionFinished -= CameraView_OnDetectionFinished;
                    if (App.ScanMode == ScanMode.ScanAndCloseWithDelay)
                        await Task.Delay(TimeSpan.FromMilliseconds(App.DelayInMilliSeconds));// Small delay to allow user to see the detected barcode
                    _taskCompletionSource.SetResult(bardCode);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
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

    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if (App.ScanMode == ScanMode.ContinuousWithSelection && _drawable.BarcodeResults?.Any() == true)
        {
            var pointTappedPosition = e.GetPosition(null);
            if (pointTappedPosition.HasValue)
            {
                var displayInfo = DeviceDisplay.MainDisplayInfo;

                var adjustedPoint = new Point(pointTappedPosition.Value.X * displayInfo.Density, pointTappedPosition.Value.Y * displayInfo.Density);
                var barcodeResult = _drawable.BarcodeResults.FirstOrDefault(x => x.PreviewBoundingBox.Contains(adjustedPoint));
                if (barcodeResult != null)
                {
                    Barcode.OnDetectionFinished -= CameraView_OnDetectionFinished;
                    _taskCompletionSource.SetResult(barcodeResult.DisplayValue);
                }
            }
        }
    }
}