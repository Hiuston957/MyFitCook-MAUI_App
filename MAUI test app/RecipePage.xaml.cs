namespace MAUI_test_app;

public partial class RecipePage : ContentPage
{
    
    public RecipePage(  )
	{
		InitializeComponent();

        int id = 1;
        var Rec = new Recipe(id);

        Title.Text=Rec.Title;
        Instruction.Text = Rec.Description;
        ListOfCusoomIngredients.ItemsSource = Rec.Show_CustomIngredients(5);
        ListOfCopmpsition.Text = Rec.Show_CustomComposition(5);


     


    }
}