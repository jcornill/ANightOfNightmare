using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestsDetector : MonoBehaviour, IObserver {

	public string[] questInfo;
	public string[] eventComplete;
	public string[] eventArgs;

	public Text questText;

	public int missionIndex;
	private EventManager _refEventManger;


	// Use this for initialization
	void Awake () {
		if (questInfo.Length != eventComplete.Length)
			Debug.LogWarning("QuestsDetector => The number of questInfo is not the same of the number of eventComplete.");
		missionIndex = 0;
		_refEventManger = ManagerController.Instance.eventManager;
		for (int i = 0; i < eventComplete.Length; i++) {
			string vEvent = eventComplete [i];
			_refEventManger.AddObserver (this, vEvent);
		}
		UpdateText ();
	}

	public void PlayEvent(string pEvent, Arg pArg)
	{
		string vArgString = "";
		if (pArg != null) {
			if (pArg is ArgType<string>)
				vArgString = ((ArgType<string>)pArg).value;
		}
		if (eventComplete [missionIndex] == pEvent && eventArgs[missionIndex] == vArgString) {
			_refEventManger.RemoveObserver (this, pEvent);
			ManagerController.Instance.world.ExpandRoom ();
			missionIndex++;
			UpdateText ();
			if (missionIndex == eventComplete.Length)
				Debug.Log ("No More quest to do => Win");
		}
	}

	private void UpdateText()
	{
		string vText = GetCurrentQuestInfo ();
		if (vText != null)
			questText.text = vText;
		else 
			questText.text = "No more quest to do => Win ?";
	}

	public string GetCurrentQuestInfo()
	{
		if (missionIndex < questInfo.Length)
			return (questInfo [missionIndex]);
		return null;
	}
}
