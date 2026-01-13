namespace MauiMemoryleak
{
    public partial class App : Application
    {
        private readonly StartPage _startPage;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _startPage = serviceProvider.GetService<StartPage>();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(_startPage);
        }
    }
}