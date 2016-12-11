using UnityEngine;
using System.Collections;

public class Monster : Entity, IObserver {
	public Entity target { get; set; }
	public int MobId;
	public int lootId;
	private Animation _animation;

	public override void Init ()
	{
		base.Init ();
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_ACTION_DONE);
		_animation = GetComponentInChildren<Animation> ();
	}

	public override void TakeDamage (float pDamage)
	{
		base.TakeDamage (pDamage);
		_animation.Play ("DamageTaken");
	}

	private void ProcessAction()
	{
		if (target == null)
			return;
		if (DistanceFrom (target) == 1)
			target.TakeDamage (damage);
		if (DistanceFrom (target) > 1)
			MoveTo (target);
	}

	public override void PlayEvent(string pEvent, Arg pArg)
	{
		base.PlayEvent (pEvent, pArg);
		ProcessAction ();
	}

	public override void Death ()
	{
		ManagerController.Instance.eventManager.RemoveObserver (this, EventManager.EVENT_ACTION_DONE);
		ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_MOB_DEATH, new ArgType<string> (MobId.ToString ()));
		if (MobId == Constants.MONSTER_BOOK)
			ManagerController.Instance.eventManager.NotifyObservers (EventManager.EVENT_UPDATE_DIALOG, new ArgType<int> (2));
		tile.objet = null;
		if (lootId != -1)
			world.SpawnLoot (tile, "0", lootId);
		GameObject.Destroy (gameObject);
	}

}
