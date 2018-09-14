using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class DataOfPlayer : IRepository
    {
        public static int numberOfPlayer = 0;
        public static string currentPlayerToken = null;

        public void AddLog(Logger log)
        {
            using (var connection = new SqlConnection("Data Source=TAVDESK033;Initial Catalog=TicTacToeGame;User ID=sa;Password=test123!@#"))
            {
                numberOfPlayer++;
                connection.Open();
                string sql = "INSERT INTO LogDB(Exception,Result,Response) VALUES(@Name,@Email,@Token)";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Name", log.Exception);
                cmd.Parameters.AddWithValue("@Email", log.Request);
                cmd.Parameters.AddWithValue("@Token", log.Response);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }
        public static bool Add(Player player)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source=TAVDESK033;Initial Catalog=TicTacToeGame;User ID=sa;Password=test123!@#"))
                {
                    numberOfPlayer++;
                    connection.Open();
                    string sql = "INSERT INTO Game(Name,Email,AccessToken,CurrentStatus) VALUES(@Name,@Email,@Token,@Status)";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Name", player.Name);
                    cmd.Parameters.AddWithValue("@Email", player.Email);
                    cmd.Parameters.AddWithValue("@Token", GetToken());
                    if (NumberOfPlayersReady() <= 2)
                    {
                        cmd.Parameters.AddWithValue("@Status", true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Status", false);
                    }
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    player.Id = GetCurrentUserID();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CheckIfTokenPresentInDB(string apiKey)
        {
            try
            {
                string key = null;
                int count = 0;
                using (var connection = new SqlConnection("Data Source=TAVDESK033;Initial Catalog=TicTacToeGame;User ID=sa;Password=test123!@#"))
                {
                    numberOfPlayer++;
                    connection.Open();
                    string sqlCmd = "Select * from Game";
                    SqlCommand cmd = new SqlCommand(sqlCmd, connection);
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            key = Convert.ToString(dataReader[3]);
                            key = key.Trim();
                            if (key.Equals(apiKey))
                            {
                                count++;
                                break;
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            //return true;
        }
        public static int NumberOfPlayersReady()
        {
            try
            {
                string connectionString = "Data Source=TAVDESK033;Initial Catalog=TicTacToeGame;User ID=sa;Password=test123!@#";

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Game")
                {
                    Connection = sqlConnection
                };
                int RecordCount = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return RecordCount;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int GetCurrentUserID()
        {
            try
            {
                string connectionString = "Data Source=TAVDESK033;Initial Catalog=TicTacToeGame;User ID=sa;Password=test123!@#";

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select ID from Game where AccessToken = " + currentPlayerToken + "")
                {
                    Connection = sqlConnection
                };

                int currentUserID = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return currentUserID;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string GetCurrentUserToken(int id)
        {
            try
            {
                string connectionString = "Data Source=TAVDESK033;Initial Catalog=TicTacToeGame;User ID=sa;Password=test123!@#";

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select Token from Game where ID = " + id + "")
                {
                    Connection = sqlConnection
                };

                string currentUserToken = sqlCommand.ExecuteScalar().ToString();
                return currentUserToken;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string GetToken()
        {

            currentPlayerToken = Guid.NewGuid().ToString();

            return currentPlayerToken;
        }
        
    }
}


