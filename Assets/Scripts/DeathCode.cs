using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathCode : MonoBehaviour, IObserver {

	private Animation _animation;
	public float alpha;
	private bool start;
	private Image img;
	private Text text;

	// Use this for initialization
	void Start () {
		_animation = GetComponent<Animation> ();
		img = GetComponent<Image> ();
		text = GetComponentInChildren<Text> ();
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_PLAYER_DEATH);
	}

	void Update()
	{
		if (start) {
			Color vColor = img.color;
			vColor.a = alpha;
			text.color = vColor;
			img.color = vColor;
		}
	}

	public void PlayEvent (string pEvent, Arg pArg) {
		start = true;
	}
}
