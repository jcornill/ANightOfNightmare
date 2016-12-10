using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	//The string for the spriteSheet of the tile
	public string spriteSheet;

	// Bool if the tile is in the shadow
	public bool isShadow;// {get; private set;}

	// Bool if the tile is a border
	public bool isBorder;// {get; private set;}

	// Bool if the tile is a wall
	public bool isWall;// {get; private set;}

	public Vector2 pos;

	private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Awake () {
		pos = new Vector2 (transform.position.x, transform.position.y);
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		Sprite[] vSprites = Resources.LoadAll <Sprite>(Constants.PATH_TILE_SPRITE_SHEET + spriteSheet);
		int vRng = Random.Range (0, vSprites.Length);
		ChangeSprite (vSprites [vRng]);
	}

	private void ChangeSprite(Sprite pSprite)
	{
		_spriteRenderer.sprite = pSprite;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
