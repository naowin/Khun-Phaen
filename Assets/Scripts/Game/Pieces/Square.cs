﻿using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	Vector3 StartPosition;
	int threshold = 100;
	bool moved = false;
	
	Vector3 currentPosition; 
	// Use this for initialization
	void Start () {
		ResetPosition ();
	}

	void OnMouseUp() {
		if(moved) {
			moved = false;
			GameMaster.gameMaster.AddMove();
		}
	}

	void OnMouseDown() {
		StartPosition = Input.mousePosition;
	}
	
	void OnMouseDrag() {
		Vector3 v3 = Input.mousePosition - StartPosition;
		v3.Normalize();
		float f = Vector3.Dot(v3, Vector3.up);
		if (Vector3.Distance(StartPosition, Input.mousePosition) < threshold) {
			// Debug.Log("No movement");
			return;
		}
		
		if (f >= 0.5) {
			// Debug.Log("Up");
			Vector3 destination = currentPosition;
			destination.z += 4;
			
			if (GameMaster.gameMaster.isEmpty(destination)){
				transform.position = Vector3.Lerp(currentPosition, destination, 10);
				Vector3 unlock = currentPosition;
				unlockEyes(unlock, destination);
				currentPosition = transform.position;

				moved = true;
			}
		}
		else if (f <= -0.5) {
			// Debug.Log("Down");
			Vector3 destination = currentPosition;
			destination.z -= 4;
			
			if (GameMaster.gameMaster.isEmpty(destination)){
				transform.position = Vector3.Lerp(currentPosition, destination, 10);
				Vector3 unlock = currentPosition;
				unlockEyes(unlock, destination);
				currentPosition = transform.position;

				moved = true;
			}
		}
		else {
			f = Vector3.Dot(v3, Vector3.right);
			if (f >= 0.5) {
				// Debug.Log("Right");
				
				Vector3 destination = currentPosition;
				destination.x += 4;
				if (GameMaster.gameMaster.isEmpty(destination)){
					transform.position = Vector3.Lerp(currentPosition, destination, 10);
				
					Vector3 unlock = currentPosition;
					unlockEyes(unlock, destination);
					currentPosition = transform.position;

					moved = true;
				}
			}
			else {
				// Debug.Log("Left");
				
				Vector3 destination = currentPosition;
				destination.x -= 4;
				if (GameMaster.gameMaster.isEmpty(destination)){
					transform.position = Vector3.Lerp(currentPosition, destination, 10);
					
					Vector3 unlock = currentPosition;
					unlockEyes(unlock, destination);
					currentPosition = transform.position;

					moved = true;
				}
			}
		}
		
		StartPosition = Input.mousePosition;
	}

	void unlockEyes(Vector3 unlock, Vector3 destination)
	{
		GameMaster.gameMaster.unlockEyes(unlock, destination);
	}

	public void ResetPosition()  {
		currentPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
