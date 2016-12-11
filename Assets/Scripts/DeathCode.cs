using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathCode : MonoBehaviour, IObserver {

	private Animation _animation;
	public float alpha;
	public bool start;
	public bool life;
	private Image img;
	private Text text;
	public AudioClip endMusic;
	public AudioSource music;
	public AudioSource songDeath;

	// Use this for initialization
	void Start () {
		_animation = GetComponent<Animation> ();
		img = GetComponent<Image> ();
		text = GetComponentInChildren<Text> ();
		if (life) {
			start = true;
			_animation.Play ("Life");
		} else {
			ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_PLAYER_DEATH);
		}
	}

	void Update()
	{
		if (start) {
			Color vColor = img.color;
			vColor.a = alpha;
			img.color = vColor;
			vColor = text.color;
			vColor.a = alpha;
			text.color = vColor;

		}
	}

	public void PlayEvent (string pEvent, Arg pArg) {
		start = true;
		_animation.Play ("Death");
		music.clip = endMusic;
		music.Play ();
		music.volume = 0.42f;
		songDeath.Play ();
	}
}
