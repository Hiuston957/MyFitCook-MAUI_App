namespace MAUI_test_app;

public partial class RecipePage : ContentPage
{
    
    public RecipePage(  )
	{
		InitializeComponent();

        int id = Preferences.Default.Get("recId", -1);
        int uKcal = Preferences.Default.Get("userKcal", -1);
        //  Preferences.Default.Set("userId", 67);
        var Rec = new Recipe(id);

        Title.Text=Rec.Title;
        Instruction.Text = Rec.Description;
        ListOfCusoomIngredients.ItemsSource = Rec.Show_CustomIngredients(uKcal);
        ListOfCopmpsition.Text = Rec.Show_CustomComposition(uKcal);



      //  DisplayAlert("Alert", uid.ToString(), "OK");
    }
}