using UnityEngine;
using System.Collections;

public class Entity : RoomObject {
	public float currentLife;
	public float maximumLife;

	// Rotate the player if don't face the good direction else move the player
	public void MoveUp()
	{
		if (orientation == Constants.ORIENTATION_UP) {
			//Do Move Action
		} else {
			orientation = Constants.ORIENTATION_UP;
		}
		UpdateRotation ();
	}

	// Rotate the player if don't face the good direction else move the player
	public void MoveRight()
	{
		if (orientation == Constants.ORIENTATION_RIGHT) {
			//Do Move Action
		} else {
			orientation = Constants.ORIENTATION_RIGHT;
		}
		UpdateRotation ();
	}

	// Rotate the player if don't face the good direction else move the player
	public void MoveBottom()
	{
		if (orientation == Constants.ORIENTATION_BOTTOM) {
			//Do Move Action
		} else {
			orientation = Constants.ORIENTATION_BOTTOM;
		}
		UpdateRotation ();
	}

	// Rotate the player if don't face the good direction else move the player
	public void MoveLeft()
	{
		if (orientation == Constants.ORIENTATION_LEFT) {
			//Do Move Action
		} else {
			orientation = Constants.ORIENTATION_LEFT;
		}
		UpdateRotation ();
	}

	// Update visually the player rotation
	private void UpdateRotation()
	{
		transform.localEulerAngles = new Vector3 (0, 0, orientation * -90f);
	}

	public void TakeDamage(float pDamage)
	{
		currentLife -= pDamage;
		if (currentLife <= 0) {
			currentLife = 0;
			Death ();
		}
	}

	public virtual void Death() {}
}
