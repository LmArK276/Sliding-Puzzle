using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Slagalica_97_2018
{
    class DbConnection
    {
        private static SqlConnection _connection = null;

        public static SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    //string path = AppDomain.CurrentDomain.BaseDirectory.ToLower().Replace("\\bin", "").Replace("\\debug", "").Replace("\\release", "").TrimEnd('\\');
                    //Debug.WriteLine(Properties.Settings.Default.ScoreboardConnectionString);
                    //_connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+path+@"\Scoreboard.mdf;Integrated Security=True");
                    _connection = new SqlConnection(@"Data Source=(localdb)\lokalna_instanca;Initial Catalog=SCOREBOARD;Integrated Security=True");
                }
                return _connection;
            }
        }
    }
}
