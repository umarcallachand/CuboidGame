using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Data;
using Mono.Data.Sqlite;
using Projects.Script;
using Rect = UnityEngine.Rect;

public class ScoreDB : MonoBehaviour
{
    public static ScoreDB scoreDb;
    
    private static int _score = 0;
    private string _connectionString;
    private IDbConnection dbConnection;

    private void Awake()
    {

        if (scoreDb == null)
        {
            DontDestroyOnLoad(gameObject);
            scoreDb = this;
        }

        else if (scoreDb != this)
        {
            Destroy(gameObject);
        }
        
    }
    public int GetScore()
    {
        return _score;
    }

    public void SetScore(int value)
    {
        _score = value;
    }


    private void Start()
    {
        //DisplayScore();
        _connectionString = "URI=file:" + Application.dataPath + "/Projects/Database/CuboidHighScore.db";
        InitializeDb();
        //_score = 300;
        //SaveScore();
        ReadingFromDatabase();
    }

    public void InitializeDb()
    {
        dbConnection = new SqliteConnection(_connectionString);

        string commandText = "create table IF NOT EXISTS  CuboidHighScore " +
                             "(ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                             "Name VARCHAR(255), " +
                             "Score INTEGER," +
                             " Date DATE);";

        if (dbConnection != null)
        {
            dbConnection.Open();
            try
            {
                using (IDbCommand command = dbConnection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
            dbConnection.Close();
        }
    }

    public bool SaveScore(int score, string playerName)
    {
        int hasSaved = 0;
        if (dbConnection != null)
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                try
                {
                    command.CommandText =
                        "insert into CuboidHighScore ( ID, Name, Score, Date) "
                        + " Values" +
                        "( NULL, \"" + playerName + "\" , " + score + ", \"" + DateTime.Now + "\" ); "; //ID is set null, but will be incremented in db

                    hasSaved = command.ExecuteNonQuery(); //returns 1 if command has executed
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            dbConnection.Close();
        }

        return (hasSaved == 1); //returns true if command is executed
    }

    public void UpdateScore()
    {
        _score++;
        
    }

    

    public List<Score> ReadingFromDatabase()
    {
        List<Score> scores = new List<Score>();
        if (dbConnection != null)
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM CuboidHighScore ORDER BY Score; ";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Score score = new Score(
                            Convert.ToInt32(reader["Id"]),
                            Convert.ToString(reader["Name"]),
                            Convert.ToInt32(reader["Score"]),
                            DateTime.Now
                            );

                        scores.Add(score);
                    }
                }
            }
            dbConnection.Close();
        }
        return scores;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0,0,100,100), "Score: " + _score.ToString() );
    }

    public bool DeleteFromDb(int id)
    {
        int hasDeleted = 0;
        if (dbConnection != null)
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                try
                {
                    command.CommandText =
                        "DELETE FROM CuboidHighScore" +
                        " WHERE id = " + id + " ; ";

                    hasDeleted = command.ExecuteNonQuery(); //returns 1 if command has executed
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            dbConnection.Close();
        }

        return (hasDeleted == 1); //returns true if command is executed
    }
}
