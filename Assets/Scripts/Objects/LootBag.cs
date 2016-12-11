using UnityEngine;
using System.Collections;

public class LootBag : RoomObject {
	public string idLoot;

	public void TakeLoot(Entity pEntity)
	{
		if (idLoot == "0") {
			pEntity.AddDamage (3);
			ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_UPDATE_DIALOG, null);
		}
		Death ();
	}

}
