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
                        IsExpired = (string)dataReader["IsExpired"]

                    };
                    food.DaysRemaining = food.DateExpired.Date.Subtract(DateTime.Now.Date).Days;
                    foods.Add(food);
                }
                return foods;
            }
        }

        public FoodItem GetFood(int foodId)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT ProductID, Name, DateCreated, ExpirationDate, IsExpired FROM FoodItem WHERE idFoodItem= @id";
                cmd.Parameters.AddWithValue("id", foodId);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {

                    FoodItem food = new FoodItem()
                    {
                        FoodId = (int)dataReader["idFoodItem"],
                        Name = dataReader["Name"].ToString(),
                        DateCreated = DateTime.Parse(dataReader["DateCreated"].ToString()),
                        DateExpired = DateTime.Parse(dataReader["ExpirationDate"].ToString()),
                        IsExpired = dataReader["IsExpired"].ToString()

                    };
                    return food;
                }
                else
                {
                    return null;
                }
            }
        }

        public void CreateFood(string name, DateTime dateCreated, DateTime expirationDate, string isExpired)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO FoodItem (Name, DateCreated, ExpirationDate, IsExpired) VALUES (@name, @DC, @ED, @Ex);";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("DC", dateCreated);
                cmd.Parameters.AddWithValue("ED", expirationDate);
                cmd.Parameters.AddWithValue("EX", isExpired);
                cmd.ExecuteNonQuery();

            }

        }
        public void CreateFood(FoodItem food)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO FoodItem (Name, DateCreated, ExpirationDate, IsExpired) VALUES (@name, @DC, @ED, @Ex);";
                cmd.Parameters.AddWithValue("name", food.Name);
                cmd.Parameters.AddWithValue("DC", food.DateCreated);
                cmd.Parameters.AddWithValue("ED", food.DateExpired);
                cmd.Parameters.AddWithValue("EX", food.IsExpired);
                cmd.ExecuteNonQuery();

            }

        }
        public void UpdateFood(FoodItem food)
        {
            var conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE FoodItem SET Name = @name, DateCreated = @DC, ExpirationDate = @ED, IsExpired = @EX WHERE idFoodItem = @fID;";
                cmd.Parameters.AddWithValue("name", food.Name);
                cmd.Parameters.AddWithValue("DC", food.DateCreated);
                cmd.Parameters.AddWithValue("ED", food.DateExpired);
                cmd.Parameters.AddWithValue("EX", food.IsExpired);
                cmd.Parameters.AddWithValue("fID", food.FoodId);
                cmd.ExecuteNonQuery();
            }

        }
        public void UpdateFood(FoodItem food, int foodId)
        {
            var conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE FoodItem SET Name = @name, DateCreated = @DC, ExpirationDate = @ED, IsExpired = @EX WHERE idFoodItem = @fID;";
                cmd.Parameters.AddWithValue("name", food.Name);
                cmd.Parameters.AddWithValue("DC", food.DateCreated);
                cmd.Parameters.AddWithValue("ED", food.DateExpired);
                cmd.Parameters.AddWithValue("EX", food.IsExpired);
                cmd.Parameters.AddWithValue("fID", foodId);
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
        public void DeleteFood(FoodItem food)
        {
            var conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM FoodItem WHERE idFoodItem = @fID;";
                cmd.Parameters.AddWithValue("fID", food.FoodId);
                cmd.ExecuteNonQuery();

            }


        }


    }
}

