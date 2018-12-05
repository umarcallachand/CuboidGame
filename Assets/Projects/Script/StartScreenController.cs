using System.Collections;
using System.Collections.Generic;
using System.IO;
using Projects.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    public Button ContinueButton;
    public TextMeshProUGUI HighScoreTitle;
    public TextMeshProUGUI HighScore1;
    public TextMeshProUGUI HighScore2;
    public TextMeshProUGUI HighScore3;
    public TextMeshProUGUI HighScore4;
    public TextMeshProUGUI HighScore5;

    private List<TextMeshProUGUI> _highscores;

    private void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/saveFile.dat"))
        {
            ContinueButton.interactable = false;
        }

        _highscores = new List<TextMeshProUGUI>() { HighScoreTitle, HighScore1, HighScore2, HighScore3, HighScore4, HighScore5 };

        //set inactive for if DB is empty
        foreach (TextMeshProUGUI textMeshProUgui in _highscores)
        {
            textMeshProUgui.gameObject.SetActive(false);
        }

        DisplayScores();
    }

    private void DisplayScores()
    {
        GameObject gameScore = GameObject.FindGameObjectWithTag("Score");
        ScoreDB scoreDb = gameScore.GetComponent<ScoreDB>();
        scoreDb.InitializeDb();
        List<Score> scores = scoreDb.ReadingFromDatabase();

        _highscores[0].gameObject.SetActive(true); //title

        for (int i = 0; i < scores.Count; i++)
        {
            _highscores[i + 1].gameObject.SetActive(true);
            _highscores[i + 1].text = (i + 1) + ". " + scores[i].PlayerName + ": " + scores[i].ScoreValue.ToString();
        }
    }

    public void LoadGame()
    {
        SaveGame.saveGame.Load();
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
