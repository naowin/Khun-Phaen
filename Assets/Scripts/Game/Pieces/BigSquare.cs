﻿using UnityEngine;
using System.Collections;

public class BigSquare : MonoBehaviour {
	
	Vector3 StartPosition;
	int threshold = 9;
	bool moved = false;

	Vector3 currentPosition; 
	// Use this for initialization
	void Start () {
		this.ResetPosition ();
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
			Vector3 checkLeftPos = currentPosition;
			Vector3 checkRightPos = currentPosition;
			checkLeftPos.x -= 2;
			checkLeftPos.z += 6;
			
			checkRightPos.x += 2;
			checkRightPos.z += 6;
			
			if (GameMaster.gameMaster.isEmpty(checkLeftPos) && GameMaster.gameMaster.isEmpty(checkRightPos)){
				Vector3 destination = currentPosition;
				destination.z += 4;
				transform.position = Vector3.Lerp(currentPosition, destination, 10);
				Vector3 unlock1 = currentPosition;
				Vector3 unlock2 = currentPosition;
				unlock1.z -= 2;
				unlock1.x -= 2;
				unlock2.z -= 2;
				unlock2.x += 2;
				Vector3 lock_eye1 = destination;
				Vector3 lock_eye2 = destination;
				lock_eye1.x -= 2;
				lock_eye1.z += 2;
				lock_eye2.x += 2;
				lock_eye2.z += 2;
				
				unlockEyes(unlock1, lock_eye1);
				unlockEyes(unlock2, lock_eye2);
				currentPosition = transform.position;

				moved = true;
			}	
		}
		else if (f <= -0.5) {
			//Debug.Log("Down");
			
			Vector3 checkLeftPos = currentPosition;
			Vector3 checkRightPos = currentPosition;
			checkLeftPos.x -= 2;
			checkLeftPos.z -= 6;
			
			checkRightPos.x += 2;
			checkRightPos.z -= 6;

            if (GameMaster.gameMaster.isEmpty(checkLeftPos) && GameMaster.gameMaster.isEmpty(checkRightPos) || CheckBot(checkLeftPos.z, checkRightPos.z)){
				Vector3 destination = currentPosition;
				destination.z -= 4;
				transform.position = Vector3.Lerp(currentPosition, destination, 10);
				Vector3 unlock1 = currentPosition;
				Vector3 unlock2 = currentPosition;
				unlock1.z += 2;
				unlock1.x -= 2;
				unlock2.z += 2;
				unlock2.x += 2;
				Vector3 lock_eye1 = destination;
				Vector3 lock_eye2 = destination;
				lock_eye1.x -= 2;
				lock_eye1.z -= 2;
				lock_eye2.x += 2;
				lock_eye2.z -= 2;
				
				unlockEyes(unlock1, lock_eye1);
				unlockEyes(unlock2, lock_eye2);
				currentPosition = transform.position;

				moved = true;
				GameMaster.gameMaster.CheckWin();
			}
		}
		else {
			f = Vector3.Dot(v3, Vector3.right);
			if (f >= 0.5) {
				// Debug.Log("Right");
				
				Vector3 checkLeftPos = currentPosition;
				Vector3 checkRightPos = currentPosition;
				checkLeftPos.x += 6;
				checkLeftPos.z -= 2;
				
				checkRightPos.x += 6;
				checkRightPos.z += 2;
				
				if (GameMaster.gameMaster.isEmpty(checkLeftPos) && GameMaster.gameMaster.isEmpty(checkRightPos)){
					Vector3 destination = currentPosition;
					destination.x += 4;
					transform.position = Vector3.Lerp(currentPosition, destination, 10);
					Vector3 unlock1 = currentPosition;
					Vector3 unlock2 = currentPosition;
					unlock1.z += 2;
					unlock1.x -= 2;
					unlock2.z -= 2;
					unlock2.x -= 2;
					Vector3 lock_eye1 = destination;
					Vector3 lock_eye2 = destination;
					lock_eye1.x += 2;
					lock_eye1.z += 2;
					lock_eye2.x += 2;
					lock_eye2.z -= 2;

					unlockEyes(unlock1, lock_eye1);
					unlockEyes(unlock2, lock_eye2);
					currentPosition = transform.position;

					moved = true;
				}	
			}
			else {
				// Debug.Log("Left");
				
				Vector3 checkLeftPos = currentPosition;
				Vector3 checkRightPos = currentPosition;
				checkLeftPos.x -= 6;
				checkLeftPos.z -= 2;
				
				checkRightPos.x -= 6;
				checkRightPos.z += 2;
				
				if (GameMaster.gameMaster.isEmpty(checkLeftPos) && GameMaster.gameMaster.isEmpty(checkRightPos)){
					Vector3 destination = currentPosition;
					destination.x -= 4;
					transform.position = Vector3.Lerp(currentPosition, destination, 10);
					Vector3 unlock1 = currentPosition;
					Vector3 unlock2 = currentPosition;
					unlock1.z += 2;
					unlock1.x += 2;
					unlock2.z -= 2;
					unlock2.x += 2;
					Vector3 lock_eye1 = destination;
					Vector3 lock_eye2 = destination;
					lock_eye1.x -= 2;
					lock_eye1.z += 2;
					lock_eye2.x -= 2;
					lock_eye2.z -= 2;
					
					unlockEyes(unlock1, lock_eye1);
					unlockEyes(unlock2, lock_eye2);
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

	public void ResetPosition() {
		currentPosition = transform.position;
	}

	private bool CheckBot(float LeftZ, float RightZ) {
		if(LeftZ < -11 && LeftZ > -18 && RightZ < -11 && RightZ > -18) {
			return true;	
		}

		return false;		
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
