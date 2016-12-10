using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	// Number of tile for the width and the height of the world
	public int worldWith;
	public int worldHeight;

	public Transform tilePrefab;
	public bool createNewWorld;
	private Tile[,] tiles;

	// Use this for initialization
	void Start () {
		if (createNewWorld)
			CreateWorld ();
		else
			UpdateTileFromScene ();
	}

	private void UpdateTileFromScene()
	{
		tiles = new Tile[worldWith,worldHeight];
		for (int i = 0; i < transform.childCount; i++) {
			Transform vChild = transform.GetChild(i);
			Tile vTile = vChild.GetComponent<Tile> ();
			tiles [(int)vTile.pos.x, (int)vTile.pos.y] = vTile;
		}
	}

	private void CreateWorld()
	{
		tiles = new Tile[worldWith,worldHeight];
		for (int i = 0; i < worldWith; i++) {
			for (int j = 0; j < worldHeight; j++) {
				Transform vTransform = (Transform)GameObject.Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
				vTransform.name = "Tile_" + i + "_" + j;
				tiles[i, j] = vTransform.GetComponent<Tile> ();
			}
		}
	}

	public Tile GeTtile(int pX, int pY)
	{
		if (pX >= worldWith || pY >= worldWith || pX < 0 || pY < 0) {
			Debug.LogWarning ("World.GeTtile ("+pX+","+pY+") => Tile out of the world. Returning null");
			return null;
		}
		return tiles [pX, pY];
	}
}
