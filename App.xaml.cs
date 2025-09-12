using MAD_Keuzedeel.Pages.MainPage;

namespace MAD_Keuzedeel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Pages.MainPage.View();
        }
    }
}
