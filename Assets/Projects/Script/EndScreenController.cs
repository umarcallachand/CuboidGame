using System;
using System.Collections;
using System.Collections.Generic;
using Projects.Script;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    public GameObject GameScore { get; private set; }
    private new string _name;
    public InputField inputField;
    public Button OkButton;
    public TextMeshProUGUI HighScoreTitle;
    public TextMeshProUGUI HighScore1;
    public TextMeshProUGUI HighScore2;
    public TextMeshProUGUI HighScore3;
    public TextMeshProUGUI HighScore4;
    public TextMeshProUGUI HighScore5;

    private List<TextMeshProUGUI> _highscores;

    // Use this for initialization
    void Start ()
	{
	    Time.timeScale = 1;

        _highscores = new List<TextMeshProUGUI>() {HighScoreTitle,HighScore1,HighScore2,HighScore3,HighScore4,HighScore5};
	    foreach (TextMeshProUGUI textMeshProUgui in _highscores)
	    {
	        textMeshProUgui.gameObject.SetActive(false);
	    }

	    GameScore = GameObject.FindGameObjectWithTag("Score");
        
    }

    public void TextChange(string newText)
    {
        _name = newText;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OkBtn()
    {
        //GameScore.GetComponent<ScoreDB>().SetScore(5);
        Debug.Log(GameScore.GetComponent<ScoreDB>().GetScore());

        if (inputField.text != "")//ignore empty input
        {
            List<Score> scores = SaveScore();

            DisplayScores(scores);
        }
    }

    private List<Score> SaveScore()
    {
        ScoreDB scoreDb = GameScore.GetComponent<ScoreDB>();

        Score score = new Score(_name, scoreDb.GetScore(), DateTime.Now);
        
        //scoreDb.InitializeDb();
        List<Score> scores = scoreDb.ReadingFromDatabase();

        foreach (Score score1 in scores)
        {
            if (score.ScoreValue < score1.ScoreValue || scores.Count < 5)
            {
                scoreDb.SaveScore(score.ScoreValue, _name);
                scores = scoreDb.ReadingFromDatabase(); // update scores

                //save only 5 best in db
                if (scores.Count > 5)
                {
                    scoreDb.DeleteFromDb(scores[5].Id);
                }
                break;
            }
        }

        return scores;
    }

    private void DisplayScores(List<Score> scores)
    {
        inputField.gameObject.SetActive(false);
        OkButton.gameObject.SetActive(false);
        _highscores[0].gameObject.SetActive(true); //title

        for (int i = 0; i < scores.Count-1; i++)
        {
            _highscores[i+1].gameObject.SetActive(true);
            _highscores[i + 1].text = (i+1) + ". " + scores[i].PlayerName + ": " + scores[i].ScoreValue.ToString();
        }
    }
}
