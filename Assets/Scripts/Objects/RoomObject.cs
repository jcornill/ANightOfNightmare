using UnityEngine;
using System.Collections;

public class RoomObject : MonoBehaviour, IObserver
{
	// 0 = up; 1 = right; 2 = bottom; 3 = left;
	public int orientation;
	// Tile where the objet is
	public Tile tile {get;set;}

	protected World _refWorld;

	public void Awake()
	{
		Init ();
	}

	public virtual void Init()
	{
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_MANAGER_INIT);
	}

	public virtual void PlayEvent(string pEvent, Arg pArg)
	{
		if (pEvent == EventManager.EVENT_MANAGER_INIT) {
			_refWorld = ManagerController.Instance.world;
			tile = _refWorld.GetTile (transform.position.x, transform.position.y);
			tile.objet = this;
		}
	}
		
}
