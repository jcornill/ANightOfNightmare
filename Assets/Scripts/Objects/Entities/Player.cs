using UnityEngine;
using System.Collections;

public class Player : Entity {
	public override bool CheckTile (Tile pTile)
	{
		if (pTile != null && !pTile.isWall && !pTile.isShadow && pTile.objet == null) {
			ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_HIDE_DIALOG, null);
			return true;
		}
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
		}

	}
}
