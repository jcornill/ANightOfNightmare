﻿using UnityEngine;
using System.Collections;

public class Loot : RoomObject {
	public string idLoot;

	public void TakeLoot(Entity pEntity)
	{
		if (idLoot == "0") {
			pEntity.AddDamage (3);
			pEntity.ChangeSprite (pEntity.GetSpriteFromId (Constants.SPRITE_PLAYER_KNIFE));
			ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_UPDATE_DIALOG, new ArgType<int> (1));
		} else if (idLoot == "1") {
			pEntity.AddArmor (2);
			pEntity.ChangeSprite (pEntity.GetSpriteFromId (Constants.SPRITE_PLAYER_KNIFE_ARMOR));
			world.SpawnMonster (world.GetTile (13f, 14f), Constants.MONSTER_BOOK, -1, pEntity);
			world.SpawnMonster (world.GetTile (11f, 14f), Constants.MONSTER_BOOK, Constants.SPRITE_LOOT_APPLE, pEntity);
			world.SpawnMonster (world.GetTile (12f, 13f), Constants.MONSTER_BOOK, -1, pEntity);
		} else if (idLoot == "2") {
			pEntity.RegenHealth (20f);
		}
		Death ();
	}
}
