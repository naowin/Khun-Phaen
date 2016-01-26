using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public static GameMaster gameMaster;
	public int totalMoves;
	public Text moves; 

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

	// GameEye[] gameBoard = new GameEye[20];
	private Dictionary<string, GameEye> gameBoard;

	void Awake()
	{
		totalMoves = 0;
		gameMaster = this;
	}

	void Start () {
		moves = moves.GetComponent<Text>();
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
		this.totalMoves++;	
		moves.text = string.Format("Moves: {0}", totalMoves);
	}

	public void CheckWin() {
		if(BigSquare.transform.position.x == 0 && BigSquare.transform.position.z <= -7 )
		{
			WinWindow.gameObject.SetActive(true);
		}
	}

	public void ResetGame() {
		this.totalMoves = 0;
		moves.text = string.Format("Moves: {0}", totalMoves);

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

	// Update is called once per frame
	void Update () {
	
	}
}
