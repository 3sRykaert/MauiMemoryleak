namespace MauiMemoryleak
{
    public partial class App : Application
    {
        private readonly INavigationService? _navigationService;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _navigationService = serviceProvider.GetService<INavigationService>();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new StartPage(_navigationService)));
        }
    }
}