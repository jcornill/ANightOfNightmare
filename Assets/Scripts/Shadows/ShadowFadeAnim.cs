using UnityEngine;
using System.Collections;

public class ShadowFadeAnim : MonoBehaviour {

	public float alpha;
	private SpriteRenderer _spriteRenderer;
	private Animation _animation;

	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		_animation = GetComponent<Animation> ();
	}

	// Update is called once per frame
	void Update () {
		Color vColor = _spriteRenderer.color;
		vColor.a = alpha;
		_spriteRenderer.color = vColor;
		if (alpha == 0)
			setSate (false);
	}

	public void setSate(bool pPositive)
	{
		enabled = pPositive;
		gameObject.SetActive (pPositive);
	}

	public void playAnim()
	{
		_animation.Play ("ShadowFadeLight");
	}

}
