using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Entity {

	private Animation _animation;
	public Image healthBar;

	public override void Init ()
	{
		base.Init ();
		_animation = GetComponentInChildren<Animation> ();
	}

	public override void TakeDamage (float pDamage)
	{
		base.TakeDamage (pDamage);
		healthBar.fillAmount = (currentLife / maximumLife);
	}

	public override void RegenHealth (float pHeal)
	{
		base.RegenHealth (pHeal);
		healthBar.fillAmount = (currentLife / maximumLife);
	}

	public override void Death ()
	{
		ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_PLAYER_DEATH, null);
		base.Death ();
	}

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
			vNpc.playerInterract = true;
			vNpc.FaceOrientation (orientation);
			vNpc.ShowDialog ();
		} else if (pObject is Monster) {
			Monster vMonster = (Monster)pObject;
			_animation.Play ("PlayerAttack");
			vMonster.TakeDamage (damage);
			vMonster.FaceOrientation (orientation);
			vMonster.target = this;
		} else if (pObject is Loot) {
			Loot vLoot = (Loot)pObject;
			vLoot.TakeLoot (this);
		}

	}
}
