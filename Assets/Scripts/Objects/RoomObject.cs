using UnityEngine;
using System.Collections;

public class RoomObject : MonoBehaviour, IObserver
{
	// 0 = up; 1 = right; 2 = bottom; 3 = left;
	public int orientation; 
	// Tile where the objet is
	public Tile tile;

	protected World _refWorld;

	void Awake()
	{
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_MANAGER_INIT);
	}

	public void PlayEvent(string pEvent, Arg pArg)
	{
		_refWorld = ManagerController.Instance.world;
		tile = _refWorld.GetTile (transform.position.x, transform.position.y);
	}

	void Update()
	{

	}
}
