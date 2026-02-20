namespace MauiMemoryleak
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new StartPage(_navigationService));
            return new Window(new MasterPage() { Detail = new NavigationPage(new DetailPage1())});
        }
    }
}