using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// The sprite component of the tile
	public Sprite sprite;// {get; private set;}

	// Bool if the tile is in the shadow
	public bool isShadow;// {get; private set;}

	// Bool if the tile is a border
	public bool isBorder;// {get; private set;}

	// Bool if the tile is a wall
	public bool isWall;// {get; private set;}

	public Vector2 pos;

	// Use this for initialization
	void Awake () {
		pos = new Vector2 (transform.position.x, transform.position.y);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
