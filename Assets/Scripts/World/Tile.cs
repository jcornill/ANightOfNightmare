using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	//The string for the spriteSheet of the tile
	public string spriteSheet;

	// Bool if the tile is in the shadow
	public bool isShadow;// {get; set;}

	// Script attached to the shadow
	public ShadowFadeAnim shadowAnim;

	// Bool if the tile is a border
	public bool isBorder;// {get; private set;}

	// Bool if the tile is a wall
	public bool isWall;// {get; private set;}

	public Vector2 pos;

	public RoomObject objet;// { get; set;}

	private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Awake () {
		pos = new Vector2 (transform.position.x, transform.position.y);
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		Sprite[] vSprites = Resources.LoadAll <Sprite>(Constants.PATH_TILE_SPRITE_SHEET + spriteSheet);
		int vRng = Random.Range (0, vSprites.Length);
		ChangeSprite (vSprites [vRng]);
		shadowAnim = GetComponentInChildren<ShadowFadeAnim> ();
		isShadow = true;
		shadowAnim.tile = this;
	}

	void Start()
	{
		FreeShadowStartingTile ();
	}

	private void FreeShadowStartingTile()
	{
		if (pos.x == 12 && pos.y == 13) {
			FreeShadowTile ();
		} else if (pos.x == 12 && pos.y == 12) {
			FreeShadowTile ();
		} else if (pos.x == 13 && pos.y == 12) {
			FreeShadowTile ();
		} else if (pos.x == 13 && pos.y == 13) {
			FreeShadowTile ();
		}
	}

	private void ChangeSprite(Sprite pSprite)
	{
		_spriteRenderer.sprite = pSprite;
	}

	public Tile[] GetAdjacentShadowTiles()
	{
		List<Tile> vTilesList = new List<Tile> ();
		World vWorld = ManagerController.Instance.world;
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				if (i == 0 && j == 0)
					continue;
				Tile vTile = vWorld.GetTile (pos.x + i, pos.y + j);
				if (vTile == null) {
					Debug.LogWarning ("Tile.GetAdjacentShadowTiles() => Trying to expand above the map limit, returning null.");
					return null;
				}
				if (vTile.isShadow)
					vTilesList.Add (vTile);
			}
		}
		return (vTilesList.ToArray ());
	}

	public void FreeShadowTile()
	{
		shadowAnim.playAnim ();
	}
}
