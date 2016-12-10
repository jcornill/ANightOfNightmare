using UnityEngine;
using System.Collections;

public class Npc : Entity {
	public int dialogToPrint = 0;

	public void ShowDialog()
	{
		ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_SHOW_DIALOG, new ArgType<string> (dialogToPrint.ToString()));
	}

}
