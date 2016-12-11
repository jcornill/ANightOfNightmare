using UnityEngine;
using System.Collections;

public class Npc : Entity {
	public int dialogToPrint {get;set;}

	public override void Init ()
	{
		base.Init ();
		dialogToPrint = 0;
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_UPDATE_DIALOG);
	}

	public void ShowDialog()
	{
		ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_SHOW_DIALOG, new ArgType<string> (dialogToPrint.ToString()));
	}

	public override void PlayEvent (string pEvent, Arg pArg)
	{
		if (pEvent == EventManager.EVENT_UPDATE_DIALOG) {
			int vDialog = ((ArgType<int>)pArg).value;
			dialogToPrint = vDialog;
		} else {
			base.PlayEvent (pEvent, pArg);
		}
	}

}
