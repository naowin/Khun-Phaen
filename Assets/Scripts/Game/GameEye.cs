using UnityEngine;
using System.Collections;

public class GameEye {

	public float x = 0;
	public float z = 0;
	public bool empty = false;

	public GameEye(float x, float z, bool empty)
	{
		this.x = x;
		this.z = z;
		this.empty = empty;
	}
}
