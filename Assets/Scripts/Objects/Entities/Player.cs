using UnityEngine;
using System.Collections;

public class Player : Entity {
	public override bool CheckTile (Tile pTile)
	{
		if (pTile.objet != null)
			DoAction (pTile.objet);
		if (pTile != null && !pTile.isWall && !pTile.isShadow && pTile.objet != null)
			return false;
		return true;
	}

	private void DoAction(RoomObject pObject)
	{

	}
}
