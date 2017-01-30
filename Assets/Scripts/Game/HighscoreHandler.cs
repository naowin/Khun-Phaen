using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour {

	public void NewHighscore(string username) {
		GameMaster.gameMaster.AddHighscore(username);
	}
}
