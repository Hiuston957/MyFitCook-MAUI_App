namespace MAUI_test_app;

public partial class App : Application
{
    public static string myVariable = "Hello, world!";

    public App()
	{

    

    InitializeComponent();

		MainPage = new AppShell();

		
	}
}
