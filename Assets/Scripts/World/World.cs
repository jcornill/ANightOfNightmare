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

	void Start()
	{
		ManagerController.Instance.InitManagers ();
	}

	/**
	 * 	Try to create the world depending of the bool set in the inspector
	 * 	Function call by the ManagerController
	 */
	public void TestCreatingWorld () {
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
			tiles [(int)vTile.transform.position.x, (int)vTile.transform.position.y] = vTile;
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

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.P))
			ExpandRoom ();
	}

	public void ExpandRoom()
	{
		foreach (Tile vTile in tiles) {
			if (vTile.isShadow)
				continue;
			foreach (Tile vTileShadow in vTile.GetAdjacentShadowTiles()) {
				vTileShadow.FreeShadowTile();
			}
		}
	}

	public Tile GetTile(float pX, float pY)
	{
		int vX = (int)pX;
		int vY = (int)pY;
		if (vX >= worldWith || vY >= worldWith || vX < 0 || vY < 0) {
			Debug.LogWarning ("World.GeTtile ("+vX+","+vY+") => Tile out of the world. Returning null");
			return null;
		}
		return tiles [vX, vY];
	}
}
