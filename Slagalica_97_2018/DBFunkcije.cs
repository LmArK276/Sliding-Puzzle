using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Slagalica_97_2018
{
    public class DBFunkcije
    {
        private SqlConnection connection = DbConnection.Connection;

        public List<ScoreTip> dajTop10()
        {
            List<ScoreTip> top10 = new List<ScoreTip>();

            connection.Open();

            string query = @"SELECT TOP 10 Nickname,Score,Moves,TimeSeconds FROM Scoreboard_tbl ORDER BY score DESC";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                string nickname = reader[0].ToString();
                int score = int.Parse(reader[1].ToString());
                int potezi = int.Parse(reader[2].ToString());
                int vreme = int.Parse(reader[3].ToString());

                top10.Add(new ScoreTip(nickname, score, potezi, vreme));
            }

            connection.Close();

            return top10;
        }

        public void dodajRezultat(string Nickname, int Score, int Moves , int TimeSeconds)
        {
            connection.Open();

            string query = @"INSERT INTO Scoreboard_tbl VALUES (@Nickname,@Score,@Moves,@TimeSeconds)";

            SqlCommand command = new SqlCommand(query, connection);

           

            command.Parameters.AddWithValue("@Nickname", Nickname);
            command.Parameters.AddWithValue("@Score", Score);
            command.Parameters.AddWithValue("@Moves", Moves);
            command.Parameters.AddWithValue("@TimeSeconds", TimeSeconds);


            //Debug.WriteLine(Nickname);
            //Debug.WriteLine(Score);
            //Debug.WriteLine(Moves);
            //Debug.WriteLine(TimeSeconds);

            command.ExecuteNonQuery();


            connection.Close();
        }


    }
}
