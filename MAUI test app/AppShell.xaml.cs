namespace MAUI_test_app;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("RecipePage", typeof(RecipePage));
    }
}
