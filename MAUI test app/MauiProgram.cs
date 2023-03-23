using Microsoft.Extensions.Logging;
using System.Data.SqlClient;


namespace MAUI_test_app;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{

		var builder = MauiApp.CreateBuilder();


        builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        Preferences.Default.Set("serverLoc", "Data Source=DESKTOP-LJ35QQ8;Initial Catalog=MyFitCook;Integrated Security=True");
        Preferences.Default.Set("userPass", "12345678");
        Preferences.Default.Set("userId", "1");

        var User = new User(Preferences.Default.Get("userId", "Unknown"), Preferences.Default.Get("userPass", "Unknown"));
        Preferences.Default.Set("userName", User.Uname);
        Preferences.Default.Set("userKcal", User.Ukcal);

        return builder.Build();




    }



}
