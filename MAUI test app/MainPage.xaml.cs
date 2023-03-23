
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
namespace MAUI_test_app;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();

        

        var User = new User(Preferences.Default.Get("userId", "Unknown"), Preferences.Default.Get("userPass", "Unknown"));

        
        

        Chats = GetChats();
        BindingContext = this;
        
    }

    private void SgowAvableRecipes(object sender, EventArgs e)
    {
       


        // FoundListLabel.IsVisible = true;

        var FindForName = RecipeName.Text;

        string loc = Preferences.Default.Get("serverLoc", "Unknown");


        SqlConnection con = new SqlConnection(loc);
        con.Open();

        string quest = $"select ID_Recipe,Name from Recipes where Name LIKE '%{FindForName}%'";


        if ((FindForName == null)||(FindForName == ""))
        {
            quest = $"SELECT NULL WHERE 1 = 0";
        }

        if (FindForName=="all")
            quest = $"select ID_Recipe,Name from Recipes";



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

            FastList newMessage = new FastList(tmp);
            Chats.Add(newMessage);
        }

        con.Close();

    }


   
    private async void Lux2(object sender, EventArgs e)
    {
        

        string xd = (sender as Label)?.Text;

        xd= new string(xd.Where(char.IsDigit).ToArray());

        int tmp = int.Parse(xd);

        Preferences.Default.Set("recId", tmp);

        await Shell.Current.GoToAsync("RecipePage");

 
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

