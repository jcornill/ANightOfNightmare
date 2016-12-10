using UnityEngine;
using System.Collections;

public class ManagerController {

	public EventManager eventManager { get; private set;}

	private static ManagerController _instance;
	public static ManagerController Instance {
		get {
			if (_instance == null) {
				_instance = new ManagerController ();
				_instance.eventManager = new EventManager ();
			}
			return _instance;
		}
	}

	public World world { get; private set; }
	public QuestsDetector quest { get; private set;}

	public void InitManagers()
	{
		world = GameObject.Find ("World").GetComponent<World> ();
		world.TestCreatingWorld ();
		quest = GameObject.Find ("Quest").GetComponent<QuestsDetector> ();
		eventManager.NotifyObservers (EventManager.EVENT_MANAGER_INIT, null);
	}
		
}
