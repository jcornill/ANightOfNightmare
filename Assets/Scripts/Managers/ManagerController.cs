using UnityEngine;
using System.Collections;

public class ManagerController {

	public EventManager eventManager { get; private set;}

	private static ManagerController _instance;
	public static ManagerController Instance {
		get {
			if (Instance == null) {
				_instance = new ManagerController ();
				_instance.InitManagers ();
			}
			return _instance;
		}
	}

	public World world { get; private set; }

	private void InitManagers()
	{
		eventManager = new EventManager ();
		eventManager.NotifyObservers (EventManager.EVENT_MANAGER_INIT, null);
	}
		
}
