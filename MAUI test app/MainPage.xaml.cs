using Microsoft.Maui.Controls.Xaml;
using System.Data.SqlClient;
namespace MAUI_test_app;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private void SgowAvableRecipes(object sender, EventArgs e)
    {

        FoundListLabel.IsVisible = true;

        var FindForName = RecipeName.Text;

        string loc = "Data Source=DESKTOP-LJ35QQ8;Initial Catalog=MyFitCook;Integrated Security=True";

        SqlConnection con = new SqlConnection(loc);
        con.Open();

        string quest = $"select ID_Recipe,Name from Recipes where Name LIKE '%{FindForName}%'";

        SqlCommand cmd = new SqlCommand(quest, con);

        SqlDataReader reader = cmd.ExecuteReader();

        List<string> list = new List<string>();

        int i = 0;
        while (reader.Read())
        {

            int Id = reader.GetInt32(0);
            string Name = reader.GetString(1);

            Console.WriteLine($"#{Id}: {Name}");

            string tmp = $"{Id}. {Name}";

            list.Add(tmp);

        }

        con.Close();

        ListOfRecipes.ItemsSource = list;

    }

    private async void Lux(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("RecipePage");

        string xd = ListOfRecipes.SelectedItem.ToString();
        FoundListLabel.IsVisible = true;

    }
}