
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
namespace MAUI_test_app;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();

        Chats = GetChats();
        BindingContext = this;
    }

    private void SgowAvableRecipes(object sender, EventArgs e)
    {

        

       // FoundListLabel.IsVisible = true;

        var FindForName = RecipeName.Text;

        string loc = "Data Source=DESKTOP-LJ35QQ8;Initial Catalog=MyFitCook;Integrated Security=True";

        SqlConnection con = new SqlConnection(loc);
        con.Open();

        string quest = $"select ID_Recipe,Name from Recipes where Name LIKE '%{FindForName}%'";


        if (FindForName == null) 
        {
            quest = $"SELECT NULL WHERE 1 = 0";
        }



        SqlCommand cmd = new SqlCommand(quest, con);

        SqlDataReader reader = cmd.ExecuteReader();

        List<string> list = new List<string>();

        // int i = 0;

        Chats.Clear();


        while (reader.Read())
        {

            int Id = reader.GetInt32(0);
            string Name = reader.GetString(1);

            Console.WriteLine($"#{Id}: {Name}");

            string tmp = $"{Id}. {Name}";

            //list.Add(tmp);

            FastList newMessage = new FastList(tmp);
            Chats.Add(newMessage);
        }

        con.Close();

        //ListOfRecipes.ItemsSource = list;

    }


    /*
    private async void Lux(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("RecipePage");

        string xd = ListOfRecipes.SelectedItem.ToString();
        FoundListLabel.IsVisible = true;

    }
    */


   
    private async void Lux2(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("RecipePage");

        string xd = (sender as Label)?.Text;

        await DisplayAlert("Test Alert", xd, "OK");

    }




    //-------------------------------------------------------------------------------------
    public ObservableCollection<FastList> Chats { get; set; }

    private static ObservableCollection<FastList> GetChats() =>
        new ObservableCollection<FastList>(
            new List<FastList>
            {

            }
        );


    public record FastList(string Title)
    {
      
    }




}

