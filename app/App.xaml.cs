using Microsoft.Maui;
using Microsoft.Maui.Controls;
using cursiv.Services;

namespace cursiv
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            TestDatabaseConnection();
        }

        public static int CurrentUserId { get; set; } = 1;

        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new MainPage());
        }

        private async void TestDatabaseConnection()
        {
            var dbService = new DatabaseService();
            await dbService.TestConnection();
        }
    }
}
