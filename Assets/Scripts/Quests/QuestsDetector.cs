using UnityEngine;
using System.Collections;

public class QuestsDetector : MonoBehaviour, IObserver {

	public string[] questInfo;
	public string[] eventComplete;

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
	}

	public void PlayEvent(string pEvent, Arg pArg)
	{
		if (eventComplete [missionIndex] == pEvent) {
			_refEventManger.RemoveObserver (this, pEvent);
			ManagerController.Instance.world.ExpandRoom ();
			missionIndex++;
			if (missionIndex == eventComplete.Length)
				Debug.Log ("No More quest to do => Win");
		}
	}

	public string GetCurrentQuestInfo()
	{
		return (questInfo[missionIndex]);
	}
}
