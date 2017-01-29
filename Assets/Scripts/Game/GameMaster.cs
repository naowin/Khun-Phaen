using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public static GameMaster gameMaster;
	public int totalDraws;
	public Text draws; 

	public Canvas WinWindow;

	// Pieces
	public BigSquare BigSquare;
	public DRect DRect;
	public Rectangle Rect1;
	public Rectangle Rect2;
	public Rectangle Rect3;
	public Rectangle Rect4;
	public Square Square1;
	public Square Square2;
	public Square Square3;
	public Square Square4;

	// menus
	public GameObject mainMenuPanel;
	public GameObject highscorePanel;
	public GameObject aboutPanel;
	public GameObject gameText;
	public GameObject blackScreen;
	private List<GameObject> gamePanels;

	private int highscoreTreshold = 100;

	// GameEye[] gameBoard = new GameEye[20];
	private Dictionary<string, GameEye> gameBoard;

	void Awake()
	{
		totalDraws = 0;
		gameMaster = this;
	}

	void Start () {
		draws = draws.GetComponent<Text>();
		BigSquare = BigSquare.GetComponent<BigSquare>();
		DRect = DRect.GetComponent<DRect>();
		Rect1 = Rect1.GetComponent<Rectangle>();
		Rect2 = Rect2.GetComponent<Rectangle>();
		Rect3 = Rect3.GetComponent<Rectangle>();
		Rect4 = Rect4.GetComponent<Rectangle>();
		Square1 = Square1.GetComponent<Square>();
		Square2 = Square2.GetComponent<Square>();
		Square3 = Square3.GetComponent<Square>();
		Square4 = Square4.GetComponent<Square>();

		setGameBoard ();

		gamePanels = new List<GameObject> ();
		gamePanels.Add(mainMenuPanel);
		gamePanels.Add(highscorePanel);
		gamePanels.Add(aboutPanel);
		gamePanels.Add(gameText);
	}

	public void setGameBoard(){
		this.gameBoard = new Dictionary<string, GameEye>();
		// First Row
		gameBoard.Add ("-6-8", new GameEye (-6, -8, false));
		gameBoard.Add("-2-8", new GameEye (-2, -8, false));
		gameBoard.Add("2-8", new GameEye (2, -8, false));
		gameBoard.Add("6-8", new GameEye (6, -8, false));
		// Second Row
		gameBoard.Add("-6-4", new GameEye (-6, -4, false));
		
		gameBoard.Add("-2-4", new GameEye (-2, -4, false));
		gameBoard.Add("2-4", new GameEye (2, -4, false));
		gameBoard.Add("6-4", new GameEye (6, -4, false));
		// Third Row
		gameBoard.Add("-60", new GameEye (-6, 0, true));
		gameBoard.Add("-20", new GameEye (-2, 0, false));
		gameBoard.Add("20", new GameEye (2, 0, false));
		gameBoard.Add("60", new GameEye (6, 0, true));
		// Forth Row
		gameBoard.Add("-64", new GameEye (-6, 4, false));
		gameBoard.Add("-24", new GameEye (-2, 4, false));
		gameBoard.Add("24", new GameEye (2, 4, false));
		gameBoard.Add("64", new GameEye (6, 4, false));
		// Fifth Row
		gameBoard.Add("-68", new GameEye (-6, 8, false));
		gameBoard.Add("-28", new GameEye (-2, 8, false));
		gameBoard.Add("28", new GameEye (2, 8, false));
		gameBoard.Add("68", new GameEye (6, 8, false));

		gameBoard.Add("-2-12", new GameEye (-2, -12, false));
		gameBoard.Add("-2-16", new GameEye (-2, -16, false));
		gameBoard.Add("2-12", new GameEye (2, -12, false));
		gameBoard.Add("2-16", new GameEye (2, -16, false));
	}

	public bool isEmpty(Vector3 pos) {
		var key = string.Format("{0}{1}", pos.x, pos.z);
		if( gameBoard.ContainsKey(key))
		{
			return gameBoard[key].empty;
		}

		return false;
	}

	public void unlockEyes(Vector3 unlock_eye, Vector3 lock_eye) 
	{

		var unlock_key = string.Format("{0}{1}", unlock_eye.x, unlock_eye.z);
		if( gameBoard.ContainsKey(unlock_key))
		{
			gameBoard[unlock_key].empty = true;
		}

		var lock_key = string.Format("{0}{1}", lock_eye.x, lock_eye.z);
		if( gameBoard.ContainsKey(lock_key))
		{
			gameBoard[lock_key].empty = false;
		}

		Debug.Log ("unlocK: " + unlock_eye + "      lock: " + lock_eye);
	}

	public void AddMove() {
		this.totalDraws++;	
		draws.text = string.Format("Draws: {0}", totalDraws);
	}

	public void CheckWin() {
		if(BigSquare.transform.position.x == 0 && BigSquare.transform.position.z <= -7 )
		{
			this.gameText.SetActive (false);
			Text scoreText = WinWindow.transform.Find ("MainPanel/TextOfWin").GetComponent<Text>();
			int draws = this.totalDraws + 1;
			scoreText.text = string.Format ("Total draws used: {0}", draws);
			if (draws < highscoreTreshold) {
				WinWindow.transform.Find ("MainPanel/NewHighscorePanel").gameObject.SetActive(true);
			}

			WinWindow.gameObject.SetActive(true);
		}
	}

	public void SwitchMenu(int menuID) {
		foreach (var panel in gamePanels) {
			panel.SetActive (false);
		}

		switch (menuID) {
			case 0:
				gamePanels [menuID].SetActive (true);
				break;
			case 1:
				gamePanels [menuID].SetActive (true);
				this.UpdateHighscores ();
				break;
			case 2:
				gamePanels [menuID].SetActive (true);
				break;
			case 3:
				gamePanels [1].SetActive (true);
				WinWindow.gameObject.SetActive (false);
				break;
			default:
				// this.ResetGame();
				blackScreen.SetActive (false);
				gamePanels [3].SetActive (true);
				break;
		}
	}

	public void AddHighscore(string winner) {
		var newScore = this.totalDraws;
		var newWinner = winner;
		var oldScore = 0;
		var oldWinner = string.Empty;
		for (var i = 0; i < 5; i++) {
			if (PlayerPrefs.HasKey (i + "HighScore")) {
				if (PlayerPrefs.GetInt (i + "HighScore") < newScore) {
					// we got new highscore!
					oldScore = PlayerPrefs.GetInt (i + "HighScore");
					oldWinner = PlayerPrefs.GetString (i + "HighScoreName");
					PlayerPrefs.SetInt (i + "HighScore", newScore);
					PlayerPrefs.SetString (i + "HighScoreName", newWinner);
					newScore = oldScore;
					newWinner = oldWinner;		
				}
			} 
			else 
			{
				PlayerPrefs.SetInt (i + "HighScore", newScore);
				PlayerPrefs.SetString (i + "HighScoreName", newWinner);
				newScore = 0;
				newWinner = string.Empty;
			}
		}

		this.SwitchMenu (3);
	}

	public void ResetGame() {
		this.totalDraws = 0;
		draws.text = string.Format("Draws: {0}", totalDraws);

		// Reset Pieces;		
		BigSquare.transform.position = new Vector3 (0, 1, 6);
		DRect.transform.position = new Vector3 (0, 1.5f, 0);
		Rect1.transform.position = new Vector3 (-6, 1.5f, -6);
		Rect2.transform.position = new Vector3 (6, 1.5f, -6);
		Rect3.transform.position = new Vector3 (-6, 1.5f, 6);
		Rect4.transform.position = new Vector3 (6, 1.5f, 6);
		Square1.transform.position = new Vector3 (-2, 1.5f, -8);
		Square2.transform.position = new Vector3 (2, 1.5f, -8);
		Square3.transform.position = new Vector3 (-2, 1.5f, -4);
		Square4.transform.position = new Vector3 (2, 1.5f, -4);

		BigSquare.ResetPosition ();
		DRect.ResetPosition ();
		Rect1.ResetPosition ();
		Rect2.ResetPosition ();
		Rect3.ResetPosition ();
		Rect4.ResetPosition ();
		Square1.ResetPosition ();
		Square2.ResetPosition ();
		Square3.ResetPosition ();
		Square4.ResetPosition ();

		setGameBoard ();
	}

	private void UpdateHighscores() {
		// var hightscorePanelTexts = highscorePanel.GetComponentsInChildren<Text> ();
		var ranks = GameObject.FindGameObjectsWithTag("Rank");
		for (var i = 0; i < 5; i++) {
			if (PlayerPrefs.HasKey (i + "HighScore")) {
				var draws = PlayerPrefs.GetInt (i + "HighScore");
				var name = PlayerPrefs.GetString (i + "HighScoreName");
				var txtComponent = ranks [0].GetComponent<Text> ();
				txtComponent.text = string.Format ("#{0} {1} Draws: {2}", i, name, draws);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
