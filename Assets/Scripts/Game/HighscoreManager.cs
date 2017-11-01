using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour {

    // NewHighscore
    public GameObject newHighScoreWindow;
    public Text newHighscoreDrawText;
    public InputField highscoreWinner;

    // NoHighscore
    public GameObject noHighScoreWindow;
    public Text noHighscoreDrawText;

    // HighScoreList 
    public Text highscoreList;

    public GameMaster gameMaster;
    public GameMenu gameMenu;
    	
    public void GameWon(int draws, bool isHighscore)
    {
        Debug.Log("isHighScore: " + isHighscore);
        this.gameMenu.gameObject.SetActive(true);

        if(isHighscore)
        {
            newHighscoreDrawText.text = string.Format("You have solved the\nPuzzle in {0} draws!", draws);
            newHighScoreWindow.SetActive(true);
        } else
        {
            noHighscoreDrawText.text = string.Format("You have solved the\nPuzzle in {0} draws!", draws);
            noHighScoreWindow.SetActive(true);
        }
    }

    public void ResetAndReturn()
    {
        this.gameMenu.hud.SetActive(false);
        this.gameMenu.gameObject.SetActive(true);
        this.gameMaster.ResetGame();
        this.gameMenu.ChangeMenu(0);
    }

    public void AddHighscore()
    {
        var newScore = this.gameMaster.totalDraws;
        var newWinner = this.highscoreWinner.text; ;
        var oldScore = 0;
        var oldWinner = string.Empty;
        for (var i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey(i + "HighScore"))
            {
                var checkScore = PlayerPrefs.GetInt(i + "HighScore");
                if (checkScore >= newScore || checkScore == 0)
                {
                    // we got new highscore!
                    oldScore = PlayerPrefs.GetInt(i + "HighScore");
                    oldWinner = PlayerPrefs.GetString(i + "HighScoreName");
                    PlayerPrefs.SetInt(i + "HighScore", newScore);
                    PlayerPrefs.SetString(i + "HighScoreName", newWinner);
                    // Debug.Log ("Rank" + i + " draws:" + newScore + " winner: " + newWinner);
                    newScore = oldScore;
                    newWinner = oldWinner;
                }
            }
            else
            {
                // Debug.Log ("highscore did not exist!" + PlayerPrefs.HasKey (i + "HighScore"));
                // Debug.Log ("Added new highscore to playerPref: " + i + "HighScore" + " the score: " + newScore + " by winner: " + newWinner);
                PlayerPrefs.SetInt(i + "HighScore", newScore);
                PlayerPrefs.SetString(i + "HighScoreName", newWinner);
                newScore = 0;
                newWinner = string.Empty;
            }
        }

        this.gameMaster.ResetGame();
        this.gameMenu.hud.SetActive(false);
        this.gameMenu.ChangeMenu(1);
    }

    public void UpdateHighscores()
    {
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey(i + "HighScore"))
            {
                var draws = PlayerPrefs.GetInt(i + "HighScore");
                var playerName = PlayerPrefs.GetString(i + "HighScoreName");
                if (draws == 0)
                {
                    continue;
                }

                sb.Append(string.Format("{0} {1} draws!\n", draws, playerName));
            }
        }

        this.highscoreList.text = sb.ToString();
    }

    public void ResetHighscores()
    {
        PlayerPrefs.DeleteAll();
        this.UpdateHighscores();
    }
}
