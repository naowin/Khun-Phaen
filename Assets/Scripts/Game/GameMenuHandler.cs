using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuHandler : MonoBehaviour {

	public int menuId;
	void start() {
		
	}

	public void SetupMenus() {
		this.start ();
	}

	public void SwitchMenus(int menuId) {
		GameMaster.gameMaster.SwitchMenu(menuId);
	}		
}
