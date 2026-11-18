namespace MauiMemoryleak;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var somePage = _serviceProvider.GetRequiredService<SomePage>();
        return new Window(somePage);
    }
}