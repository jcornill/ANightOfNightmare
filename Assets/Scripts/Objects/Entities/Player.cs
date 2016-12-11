using UnityEngine;
using System.Collections;

public class Player : Entity {

	public override bool CheckTile (Tile pTile)
	{
		ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_ACTION_DONE, null);
		if (pTile != null && !pTile.isWall && !pTile.isShadow && pTile.objet == null) {
			ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_HIDE_DIALOG, null);
			return true;
		}
		if (pTile == null || pTile.isShadow)
			return false;
		if (pTile.objet != null)
			DoAction (pTile.objet);
		return false;
	}

	private void DoAction(RoomObject pObject)
	{
		if (pObject is Npc) {
			Npc vNpc = (Npc)pObject;
			vNpc.FaceOrientation (orientation);
			vNpc.ShowDialog ();
		} else if (pObject is Monster) {
			Monster vMonster = (Monster)pObject;
			vMonster.TakeDamage (damage);
			vMonster.FaceOrientation (orientation);
			vMonster.target = this;
		} else if (pObject is Loot) {
			Loot vLoot = (Loot)pObject;
			vLoot.TakeLoot (this);
		}

	}
}
