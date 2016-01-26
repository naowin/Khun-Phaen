using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {

	public float RotationSpeed = 75f;
	private bool rotate = true;
	// Use this for initialization
	void Start () {
	
	}

	public void StartRotate() {
		rotate = true;
	}

	public void StopRotate() {
		rotate = false;
	}

	public void ResetGame()
	{
		GameMaster.gameMaster.ResetGame();
	}

	// Update is called once per frame
	void Update () {
		if(rotate) 
		{
			transform.Rotate (new Vector3(0, 0, -1) * RotationSpeed * Time.deltaTime);
		}
	}
}
