using UnityEngine;
using System.Collections;

public class Npc : Entity {
	public int dialogToPrint {get;set;}
	public bool playerInterract;

	public override void Init ()
	{
		base.Init ();
		dialogToPrint = 0;
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_UPDATE_DIALOG);
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_DIALOG_HIDDEN);
	}

	public void ShowDialog()
	{
		ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_SHOW_DIALOG, new ArgType<string> (dialogToPrint.ToString()));
		playerInterract = false;
	}

	public override void PlayEvent (string pEvent, Arg pArg)
	{
		if (pEvent == EventManager.EVENT_UPDATE_DIALOG) {
			int vDialog = ((ArgType<int>)pArg).value;
			dialogToPrint = vDialog;
		} else if (pEvent == EventManager.EVENT_DIALOG_HIDDEN) {
			if (((ArgType<string>)pArg).value == "3" && this == world.oldNpc) {
				world.SpawnMonster (world.GetTile(tile.pos.x, tile.pos.y + 1), Constants.MONSTER_EYE, -1, null);
				Death ();
			}
		} else {
			base.PlayEvent (pEvent, pArg);
		}
	}
		

}
