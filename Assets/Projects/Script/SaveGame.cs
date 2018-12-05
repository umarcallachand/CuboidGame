using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    public static SaveGame saveGame;
    private static SaveData _saveData;

    public float PlayerX { get; private set; }
    public float PlayerY { get; private set; }
    public float PlayerZ { get; private set; }
    public float PlayerXRotation { get; private set; }
    public float PlayerYRotation { get; private set; }
    public float PlayerZRotation { get; private set; }
    public int Score { get; private set; }
    public int CurrentLevel { get; private set; }
    private bool _isLoaded = false;
    private GameObject _player;
    public GameObject GameScore { get; private set; }
    public Direction TopDirection { get; private set; }

    private void Awake()
    {
        
        if (saveGame == null)
        {
            DontDestroyOnLoad(gameObject);
            saveGame = this;
        }

        else if (saveGame != this)
        {
            Destroy(gameObject);
        }


    }

   
    private void LateUpdate()
    {
        if (_isLoaded)
        {
            LoadPlayerPosition();
        }
    }

    public void Save()
    {
        FindPlayerObject();
        FindGameScoreObject();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveFile.dat");
        
        PlayerX = _player.transform.position.x;
        PlayerY = _player.transform.position.y;
        PlayerZ = _player.transform.position.z;
        Score = GameScore.GetComponent<ScoreDB>().GetScore();
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerXRotation = _player.transform.rotation.eulerAngles.x;
        PlayerYRotation = _player.transform.rotation.eulerAngles.y;
        PlayerZRotation = _player.transform.rotation.eulerAngles.z;
        TopDirection = _player.GetComponent<PlayerRoll>().TopDirection;



        _saveData = new SaveData
        {
            PlayerX = PlayerX,
            PlayerY = PlayerY,
            PlayerZ = PlayerZ,
            Score = Score,
            CurrentLevel = CurrentLevel,
            PlayerXRotation = PlayerXRotation,
            PlayerYRotation = PlayerYRotation,
            PlayerZRotation = PlayerZRotation,
            TopDirection = TopDirection
        };
        binaryFormatter.Serialize(file, _saveData);
        file.Close();

    }

    private void FindGameScoreObject()
    {
        if (GameScore == null)
        {
            GameScore = GameObject.FindGameObjectWithTag("Score");
        }
    }

    private void FindPlayerObject()
    {
            _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Load()
    {
        FindPlayerObject();
        FindGameScoreObject();

        if (File.Exists(Application.persistentDataPath + "/saveFile.dat"))
        {

            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + "/saveFile.dat", FileMode.Open);

            var saveData = (SaveData) binaryFormatter.Deserialize(file);
            file.Close();

            PlayerX = saveData.PlayerX;
            PlayerY = saveData.PlayerY;
            PlayerZ = saveData.PlayerZ;
            Score = saveData.Score;
            CurrentLevel = saveData.CurrentLevel;
            PlayerXRotation = saveData.PlayerXRotation;
            PlayerYRotation = saveData.PlayerYRotation;
            PlayerZRotation = saveData.PlayerZRotation;
            TopDirection = saveData.TopDirection;

            if (SceneManager.GetActiveScene().buildIndex != CurrentLevel)
            {
                _isLoaded = true;
                SceneManager.LoadScene(CurrentLevel);
            }

            else
            {
                LoadPlayerPosition();
                
            }
        }
    }

    private void LoadPlayerPosition()
    {
        FindPlayerObject();
        FindGameScoreObject();
        _player.transform.position = new Vector3(
            PlayerX,
            PlayerY,
            PlayerZ
        );
        _player.transform.rotation = Quaternion.Euler(new Vector3(PlayerXRotation, PlayerYRotation,
            PlayerZRotation));
        GameScore.GetComponent<ScoreDB>().SetScore(Score);
        _player.GetComponent<PlayerRoll>().TopDirection = TopDirection;
        _isLoaded = false;
    }
}
