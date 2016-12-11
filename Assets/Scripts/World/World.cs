using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	// Number of tile for the width and the height of the world
	public int worldWith;
	public int worldHeight;

	public Transform tilePrefab;
	public Transform lootBagPrefab;
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
		SpawnObjects ();
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

	private void SpawnObjects()
	{
		SpawnLoot (GetTile (12f, 17f), "1", 1);
	}

	public void SpawnLoot(Tile pTile, string pIdLoot, int pIdSPrite)
	{
		Transform vTransform = (Transform)GameObject.Instantiate(lootBagPrefab, new Vector3(pTile.pos.x, pTile.pos.y, 0), Quaternion.identity, transform);
		vTransform.name = "LootBag_" + pIdLoot;
		Loot vLoot = vTransform.GetComponentInChildren<Loot> ();
		vLoot.tile = pTile;
		vLoot.idLoot = pIdLoot;
		pTile.objet = vLoot;
		vLoot.world = this;
		vLoot.ChangeSprite (vLoot.GetSpriteFromId (pIdSPrite));
	}

//	public void SpawnLoot(Tile pTile, int pIdMob)
//	{
//		Transform vTransform = (Transform)GameObject.Instantiate(lootBagPrefab, new Vector3(pTile.pos.x, pTile.pos.y, 0), Quaternion.identity, transform);
//		vTransform.name = "Monster_" + pIdMob;
//		Monster vMonster = vTransform.GetComponentInChildren<Monster> ();
//		vMonster.tile = pTile;
//		pTile.objet = vMonster;
//		vMonster.world = this;
//		vMonster.ChangeSprite (vMonster.GetSpriteFromId (pIdSPrite));
//	}
//
//	public Monster UpdateMobInfo(Monster pMonster, int pIdMob){
//
//	}

}
