using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MAUI_test_app
{
    internal class Recipe
    {
        private int Id;
        public string Title;
        public string Description;
        public List<List<string>> Ingredients = new List<List<string>>();

        private int[] Composition = new int[4]; 
        // 0- kcal ; 1- Protei ; 2 - Carbs ; 3- Fat






        public Recipe(int id)
        {

            string loc = Preferences.Default.Get("serverLoc", "Unknown");
          
            SqlConnection con = new SqlConnection(loc);
            con.Open();

            //data from Recipes

            string quest = $"select ID_Recipe,Name,Instruction from Recipes where ID_Recipe={id} ";
            SqlCommand cmd = new SqlCommand(quest, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
               this.Id= reader.GetInt32(0);
               this.Title = reader.GetString(1);
               this.Description = reader.GetString(2);
            }
            con.Close();

            //data from Ingredients
            quest = $"SELECT Ingredients.IngName,IngCount,IngUnit FROM IngredientsToIngredients JOIN Ingredients ON IngredientsToIngredients.Id_Ingredients = Ingredients.Id_Ingredient WHERE IngredientsToIngredients.Id_Rec ={id} ";
            cmd = new SqlCommand(quest, con);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {

             List<string> lista = new List<string>();
             lista.Add(reader.GetString(0));
             var tmp = reader.GetInt32(1);
             string tmps=tmp.ToString();
             lista.Add(tmps);
             lista.Add(reader.GetString(2));
             Ingredients.Add(lista);

            }


            con.Close();
            //data from Composition

            con.Open();
            quest = $"select    ( SELECT Kcals.kcal_value FROM Composition JOIN Kcals ON Composition.ID_Kcal = Kcals.ID_Kcal WHERE Composition.Id_Rec = {id} ) as kcal,    ( SELECT Proteins.g_value FROM Composition JOIN Proteins ON Composition.ID_Prt = Proteins.ID_Protein WHERE Composition.Id_Rec = {id} ) as prot,  ( SELECT Carbs.g_value FROM Composition JOIN Carbs ON Composition.ID_Crb = Carbs.ID_Carb WHERE Composition.Id_Rec = {id} ) as carbs,  ( SELECT Fats.g_value FROM Composition JOIN Fats ON Composition.ID_Crb = Fats.ID_Fat WHERE Composition.Id_Rec = {id} ) as fat  from Composition where ID_Rec={id} ";
             cmd = new SqlCommand(quest, con);
             reader = cmd.ExecuteReader();
           
            while (reader.Read())
            {
                Composition[0] = reader.GetInt32(0);
                Composition[1] = reader.GetInt32(1);
                Composition[2] = reader.GetInt32(2);
                Composition[3] = reader.GetInt32(3);
            }
            con.Close();



        }


        public List<string> Show_Ingredients()
        {

            var tmplist = new List<string>();


            for (int i = 0; i < Ingredients.Count; i++)
            {
                string tmp = "";
                for (int j = 0; j < Ingredients[i].Count; j++)
                {
                    tmp+=$"{ Ingredients[i][j]} ";
                }
                tmplist.Add(tmp);
            }

            return tmplist;


        }


        /*
        private void Show_Composition()
        {
            Console.WriteLine("Composition:");
            Console.WriteLine($"kcal: {Composition[0]}, Pro" +
                $"tein: {Composition[1]}, Carbs: {Composition[2]}, Fat: {Composition[3]}");

        }
        */


        public string Show_CustomComposition(int UserKcal)
        {
            double multipler = (double)UserKcal / (double)Composition[0];

            var tmplist = new List<string>();
           
            string tmp=
                $"kcal: {Math.Round((double)Composition[0] * multipler, 2)}, " +
                $"Protein: {Math.Round((double)Composition[1] * multipler, 2)}, " +
                $"Carbs: {Math.Round((double)Composition[2] * multipler, 2)}, " +
                $"Fat: {Math.Round((double)Composition[3] * multipler, 2)}";

            return tmp;


           

        }


        /*
        public void Show_Recipe(int Mykcal)
        {
            

            double multipler = (double) Mykcal / (double) Composition[0];
           // Console.WriteLine("Multipler: ");


            
           // Console.WriteLine(Title);
          //  Console.WriteLine(" ");
            Show_Ingredients();
            Console.WriteLine(" ");
         
            Show_CustomIngredients(multipler);
            Console.WriteLine(" ");
            Console.WriteLine(Description);
            Console.WriteLine(" ");
            Show_Composition();
            Show_CustomComposition(multipler);


        }
        */



        public List<string> Show_CustomIngredients(int UserKcal)
        {

            double multipler = (double)UserKcal / (double)Composition[0];








            var tmplist = new List<string>();

            for (int i = 0; i < Ingredients.Count; i++)
            {

                string tmp = "";
                for (int j = 0; j < Ingredients[i].Count; j++)
                {

                    if (j==1)
                        tmp += $"{Math.Round(     (double.Parse(Ingredients[i][j]) )*multipler,2  )} ";
                    else
                        tmp += $"{Ingredients[i][j]} ";
                }
                tmplist.Add(tmp);
            }

            return tmplist;

        }

    }
}
