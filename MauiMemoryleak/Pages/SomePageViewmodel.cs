using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiMemoryleak.Pages;

public partial class SomePageViewmodel : ObservableObject
{
    public ICommand SomeCommand { get; }

    [ObservableProperty]
    private DateTime _startUpTime;

    [ObservableProperty]
    private DateTime _utcNow1;

    [ObservableProperty]
    private double _difference;

    private static DateTime _staticUtcNow1 = DateTime.UtcNow;

    public SomePageViewmodel()
    {
        StartUpTime = DateTime.UtcNow;
        SomeCommand = new Command(async () => await DoSomethingAsync());
    }

    private async Task DoSomethingAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(3));

        UtcNow1 = _staticUtcNow1;

        Difference = (StartUpTime - UtcNow1).TotalMicroseconds;
    }
}