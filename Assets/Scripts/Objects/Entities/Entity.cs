using UnityEngine;
using System.Collections;

public class Entity : RoomObject {
	public float currentLife;
	public float maximumLife;

	// Rotate the player if don't face the good direction else move the player
	public void MoveUp()
	{
		if (orientation == Constants.ORIENTATION_UP) {
			Move (orientation);
		} else {
			orientation = Constants.ORIENTATION_UP;
		}
		UpdateRotation ();
	}

	// Rotate the player if don't face the good direction else move the player
	public void MoveRight()
	{
		if (orientation == Constants.ORIENTATION_RIGHT) {
			Move (orientation);
		} else {
			orientation = Constants.ORIENTATION_RIGHT;
		}
		UpdateRotation ();
	}

	// Rotate the player if don't face the good direction else move the player
	public void MoveBottom()
	{
		if (orientation == Constants.ORIENTATION_BOTTOM) {
			Move (orientation);
		} else {
			orientation = Constants.ORIENTATION_BOTTOM;
		}
		UpdateRotation ();
	}

	// Rotate the player if don't face the good direction else move the player
	public void MoveLeft()
	{
		if (orientation == Constants.ORIENTATION_LEFT) {
			Move (orientation);
		} else {
			orientation = Constants.ORIENTATION_LEFT;
		}
		UpdateRotation ();
	}

	// Move to a direction
	private void Move(int pDir)
	{
		Tile vDestTile = null;
		if (pDir == Constants.ORIENTATION_UP)
			vDestTile = _refWorld.GetTile (tile.pos.x, tile.pos.y + 1); 
		else if (pDir == Constants.ORIENTATION_RIGHT)
			vDestTile = _refWorld.GetTile (tile.pos.x + 1, tile.pos.y);
		else if (pDir == Constants.ORIENTATION_BOTTOM)
			vDestTile = _refWorld.GetTile (tile.pos.x, tile.pos.y - 1); 
		else if (pDir == Constants.ORIENTATION_LEFT)
			vDestTile = _refWorld.GetTile (tile.pos.x - 1, tile.pos.y); 
		else 
			Debug.LogWarning("Entity.Move("+pDir+") => Unknow dir");
		if (CheckTile (vDestTile)) {
			ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_PLAYER_MOVE, new ArgType<Tile> (vDestTile));
			tile = vDestTile;
			transform.parent.position = tile.transform.position;
		}
	}

	/** 
	 * Check if a entity can move to this tile
	 * Return true if the entity can go to this tile, false either
	 * This function is use for attack and speak
	 */
	public virtual bool CheckTile(Tile pTile)
	{
		if (pTile != null && !pTile.isWall && !pTile.isShadow)
			return true;
		return false;
	}

	// Update visually the player rotation
	private void UpdateRotation()
	{
		transform.localEulerAngles = new Vector3 (0, 0, orientation * -90f);
	}

	public void TakeDamage(float pDamage)
	{
		if (maximumLife == -1)
			return;
		currentLife -= pDamage;
		if (currentLife <= 0) {
			currentLife = 0;
			Death ();
		}
	}

	public virtual void Death() {}
}
