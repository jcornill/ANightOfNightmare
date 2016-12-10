using UnityEngine;
using System.Collections;

public class Entity : RoomObject {
	public float currentLife;
	public float maximumLife;
	public float damage;

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
			tile.objet = null;
			tile = vDestTile;
			tile.objet = this;
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
		Debug.Log (this + " take damage: " + pDamage);

		if (maximumLife == -1)
			return;
		currentLife -= pDamage;
		if (currentLife <= 0) {
			currentLife = 0;
			Death ();
		}
	}

	public virtual void Death() {
		tile.objet = null;
		GameObject.Destroy (gameObject);
	}

	/**
	 * Face the orientation of the object depending of the orientation given
	 * usefull when the player speak to a npc
	 */
	public void FaceOrientation(int pOrientation)
	{
		if (pOrientation == Constants.ORIENTATION_UP)
			orientation = Constants.ORIENTATION_BOTTOM;
		else if (pOrientation == Constants.ORIENTATION_LEFT)
			orientation = Constants.ORIENTATION_RIGHT;
		else if (pOrientation == Constants.ORIENTATION_BOTTOM)
			orientation = Constants.ORIENTATION_UP;
		else if (pOrientation == Constants.ORIENTATION_RIGHT)
			orientation = Constants.ORIENTATION_LEFT;
		UpdateRotation ();
	}

	/**
	 * Reverse the rotation given in parameter
	 */
	public int ReverseOrientation(int pOrientation)
	{
		if (pOrientation == Constants.ORIENTATION_UP)
			return Constants.ORIENTATION_BOTTOM;
		else if (pOrientation == Constants.ORIENTATION_LEFT)
			return Constants.ORIENTATION_RIGHT;
		else if (pOrientation == Constants.ORIENTATION_BOTTOM)
			return Constants.ORIENTATION_UP;
		else if (pOrientation == Constants.ORIENTATION_RIGHT)
			return Constants.ORIENTATION_LEFT;
		return -1;
	}

	public int DistanceFrom(Entity pEntity)
	{
		int distance = 0;
		distance += (int)Mathf.Abs (pEntity.tile.pos.x - tile.pos.x);
		distance += (int)Mathf.Abs (pEntity.tile.pos.y - tile.pos.y);
		return distance;
	}

	public void MoveTo(Entity pEntity)
	{
		int x1 = (int)tile.pos.x;
		int x2 = (int)pEntity.tile.pos.x;
		int y1 = (int)tile.pos.y;
		int y2 = (int)pEntity.tile.pos.y;
		if (x1 > x2)
			MoveLeft ();
		else if (x1 < x2)
			MoveRight ();
		else if (y1 > y2)
			MoveBottom ();
		else if (y1 < y2)
			MoveUp ();
	}
}
