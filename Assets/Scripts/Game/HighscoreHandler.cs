using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void NewHighscore(string username) {
		GameMaster.gameMaster.AddHighscore(username);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
