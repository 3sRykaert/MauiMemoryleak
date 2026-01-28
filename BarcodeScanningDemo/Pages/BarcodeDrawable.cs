using BarcodeScanning;

namespace BarcodeScanningDemo.Pages;

public class BarcodeDrawable : IDrawable
{
    public IReadOnlySet<BarcodeResult>? BarcodeResults { get; set; }
    
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (BarcodeResults is not null && BarcodeResults.Count > 0)
        {
            canvas.StrokeSize = 8;
            canvas.StrokeColor = Colors.SkyBlue;
            canvas.FillColor = Color.FromRgba(Colors.SkyBlue.Red, Colors.SkyBlue.Green, Colors.SkyBlue.Blue, 0.5);
            canvas.FontColor = Colors.Blue;
            canvas.FontSize = 50;
            var scale = 1 / canvas.DisplayScale;
            canvas.Scale(scale, scale);

            foreach (var barcode in BarcodeResults)
            {
                var newRectF = new RectF(barcode.PreviewBoundingBox.X, barcode.PreviewBoundingBox.Y - 100, 500, 250);
                canvas.DrawString(barcode.DisplayValue, newRectF, HorizontalAlignment.Center, VerticalAlignment.Top);
                canvas.FillRoundedRectangle(barcode.PreviewBoundingBox, 15);
            }
        }
    }
}