using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour, IObserver {

	EventManager _refEventManager;

	public GameObject[] dialogContainer;
	public Npc linkedNpc;

	private int openedDialog;

	// Use this for initialization
	void Awake () {
		_refEventManager = ManagerController.Instance.eventManager;
		_refEventManager.AddObserver (this, EventManager.EVENT_SHOW_DIALOG);
		_refEventManager.AddObserver (this, EventManager.EVENT_HIDE_DIALOG);
	}

	public void PlayEvent(string pEvent, Arg pArg)
	{
		if (pEvent == EventManager.EVENT_SHOW_DIALOG) {
			if (linkedNpc.playerInterract) {
				int vId = int.Parse (((ArgType<string>)pArg).value);
				openedDialog = vId;
				if (openedDialog == 3 && linkedNpc == ManagerController.Instance.world.oldNpc)
					openedDialog = 2;
				dialogContainer [openedDialog].SetActive (true);
			}
		} else if (pEvent == EventManager.EVENT_HIDE_DIALOG) {
			if (dialogContainer [openedDialog] == null)
				return;
			if (openedDialog == 2)
				ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_UPDATE_DIALOG, new ArgType<int> (3));
			if (dialogContainer [openedDialog].activeSelf) {
				dialogContainer [openedDialog].SetActive (false);
				_refEventManager.NotifyObservers (EventManager.EVENT_DIALOG_HIDDEN, new ArgType<string> (openedDialog.ToString ()));
			}

		}
	}
}
