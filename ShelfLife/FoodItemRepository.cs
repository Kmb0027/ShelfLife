using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ShelfLife.Models;

namespace ShelfLife
{
    public class FoodItemRepository
    {
        public static string ConnectionString { get; set; }

        public List<FoodItem> GetAllFoods()
        {
            string dC = "";
            string dE = "";
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            List<FoodItem> foods = new List<FoodItem>();

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT idFoodItem, Name, DateCreated, ExpirationDate, IsExpired FROM FoodItem;";

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    dC = (string)dataReader["DateCreated"];
                    dE = (string)dataReader["ExpirationDate"];
                    FoodItem food = new FoodItem()
                    {
                        FoodId = (int)dataReader["idFoodItem"],
                        Name = dataReader["Name"].ToString(),
                        DateCreated = DateTime.Parse(dC),
                        DateExpired = DateTime.Parse(dE),

                    };
                    food.DaysRemaining = food.DateExpired.Date.Subtract(DateTime.Now.Date).Days;
                    foods.Add(food);
                }
                return foods;
            }
        }
        public void CreateFood(string name, DateTime dateCreated, DateTime expirationDate)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO FoodItem (Name, DateCreated, ExpirationDate) VALUES (@name, @DC, @ED);";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("DC", dateCreated);
                cmd.Parameters.AddWithValue("ED", expirationDate);
                cmd.ExecuteNonQuery();

            }

        }
        public void DeleteFood(int foodId)
        {
            var conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM FoodItem WHERE idFoodItem = @fID;";
                cmd.Parameters.AddWithValue("fID", foodId);
                cmd.ExecuteNonQuery();

            }
        }
    }
}

