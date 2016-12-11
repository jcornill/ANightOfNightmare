using UnityEngine;
using System.Collections;

public class RoomObject : MonoBehaviour, IObserver
{
	// 0 = up; 1 = right; 2 = bottom; 3 = left;
	public int orientation;
	// Tile where the objet is
	public Tile tile {get;set;}

	public World world;
	protected SpriteRenderer _refRenderer;
	public Sprite[] SpriteList;

	public void Awake()
	{
		Init ();
	}

	public virtual void Init()
	{
		ManagerController.Instance.eventManager.AddObserver (this, EventManager.EVENT_MANAGER_INIT);
		_refRenderer = GetComponentInChildren<SpriteRenderer> ();
	}

	public virtual void PlayEvent(string pEvent, Arg pArg)
	{
		if (pEvent == EventManager.EVENT_MANAGER_INIT) {
			world = ManagerController.Instance.world;
			tile = world.GetTile (transform.position.x, transform.position.y);
			tile.objet = this;
		}
	}
		
	public void ChangeSprite(Sprite pNewSprite)
	{
		if (pNewSprite != null)
			_refRenderer.sprite = pNewSprite;
		else
			Debug.LogWarning ("RoomObject.ChangeSprite(null) => The sprite is null, the spirte is not changed");
	}

	public Sprite GetSpriteFromId(int pId)
	{
		if (pId >= SpriteList.Length) {
			Debug.LogWarning ("RoomObject.GetSpriteFromId(" + pId + ") => The room objet don't have this sprite id");
			return null;
		}
		return (SpriteList [pId]);
	}

	public virtual void Death() {
		tile.objet = null;
		GameObject.Destroy (gameObject);
	}

}
