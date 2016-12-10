using UnityEngine;
using System.Collections;

public class Monster : Entity, IObserver {
	public Entity target { get; set; }

	public override void Init ()
	{
		base.Init ();
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_ACTION_DONE);
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

}
