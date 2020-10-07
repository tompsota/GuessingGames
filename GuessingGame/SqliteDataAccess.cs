using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace GuessingGame
{
    /// <summary>
    /// Communication with database
    /// </summary>
    public class SqliteDataAccess
    {
        public List<Player> GetAllPlayers()
        {
            using (IDbConnection connection = new SQLiteConnection(Utils.CnnVal("PlayersDBCS")))
            {
                return connection.Query<Player>("select * from PlayersTable ORDER BY Points DESC", new DynamicParameters()).ToList();
            }
        }

        public List<Player> GetPlayer(Player player)
        {
            using (IDbConnection connection = new SQLiteConnection(Utils.CnnVal("PlayersDBCS")))
            {
                return connection.Query<Player>("SELECT * FROM PlayersTable WHERE PlayerName = @PlayerName", player).ToList();
            }
        }

        public void InsertPlayer(Player player)
        {
            using (IDbConnection connection = new SQLiteConnection(Utils.CnnVal("PlayersDBCS")))
            {
                connection.Execute("INSERT INTO PlayersTable (PlayerName, Points) VALUES (@PlayerName,@Points)", player);
                connection.Close();
            }
        }

        public int GetPlayerPointsOrDefault(Player player)
        {
            return GetPlayer(player).Count == 1 ? GetPlayer(player)[0].Points : -1;
        }

        public void UpdatePlayerPoints(Player player)
        {
            using (IDbConnection connection = new SQLiteConnection(Utils.CnnVal("PlayersDBCS")))
            {
                connection.Execute("UPDATE PlayersTable SET Points = @Points WHERE PlayerName = @PlayerName; ", player);
                connection.Close();
            }
        }

        public void ClearDB(Player player)
        {
            using (IDbConnection connection = new SQLiteConnection(Utils.CnnVal("PlayersDBCS")))
            {
                connection.Execute("DELETE FROM PlayersTable WHERE PlayerName != @PlayerName; ", player);
                connection.Close();
            }
        }
    }
}
