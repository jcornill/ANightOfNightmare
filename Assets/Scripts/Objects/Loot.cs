using UnityEngine;
using System.Collections;

public class Loot : RoomObject {
	public string idLoot;

	public void TakeLoot(Entity pEntity)
	{
		if (idLoot == "0") {
			pEntity.AddDamage (3);
			pEntity.ChangeSprite (pEntity.GetSpriteFromId (Constants.SPRITE_PLAYER_KNIFE));
			ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_UPDATE_DIALOG, null);
		} else if (idLoot == "1") {
			pEntity.AddArmor (2);
			pEntity.ChangeSprite (pEntity.GetSpriteFromId (Constants.SPRITE_PLAYER_KNIFE_ARMOR));
		}
		Death ();
	}
}
