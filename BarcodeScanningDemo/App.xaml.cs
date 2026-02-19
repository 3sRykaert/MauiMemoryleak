namespace BarcodeScanningDemo
{
    public partial class App : Application
    {
        private readonly StartPage _startPage;

        public static bool TorchOn { get; set; }
        public static bool VibrationOnDetected { get; set; } = true;
        public static bool AimMode { get; set; } = true;
        public static bool TapToSelect { get; set; }
        public static int DelayInMilliSeconds { get; set; }
        public static ScanMode ScanMode { get; set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _startPage = serviceProvider.GetService<StartPage>()!;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(_startPage);
        }
    }
}