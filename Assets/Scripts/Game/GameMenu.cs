using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

    public GameObject[] gameMenus;
    public HighscoreManager highscoreManager;
    public GameObject hud;

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }

    public void ChangeMenu(int gameMenu)
    {
        for (var i = 0; i < gameMenus.Length; i++)
        {
            if(gameMenu == 1)
            {
                this.highscoreManager.UpdateHighscores();
            }

            if (i == gameMenu)
            {
                gameMenus[i].SetActive(true);
            }
            else
            {
                gameMenus[i].SetActive(false);
            }
        }
    }		

    public void StartGame()
    {
        this.gameObject.SetActive(false);
        this.gameMenus[0].SetActive(false);
        this.hud.SetActive(true);

    }
}
