using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public struct EventPair
	{
		public string eventValue;
		public IObserver observer;


		public EventPair(IObserver pObserver, string pEventValue)
		{
			eventValue = pEventValue;
			observer = pObserver;
		}
	}


	public class EventManager 
	{
		public const string EVENT_MANAGER_INIT = "EventManagerInit";
		public const string EVENT_PLAYER_MOVE = "EventPlayerMove";
		public const string EVENT_SHOW_DIALOG = "EventShowDialog";
		public const string EVENT_HIDE_DIALOG = "EventHideDialog";
		public const string EVENT_DIALOG_HIDDEN = "EventDialogHidden";

		public Dictionary<string, List<IObserver>> eventMap { get; private set; }

		private static int _COUNT = 1;

		private ushort _working = 0;

		private List<EventPair> _addQueue;

		private List<EventPair> _removeQueue;

		public EventManager()
		{
			eventMap = new Dictionary<string, List<IObserver>> (_COUNT);
			_addQueue = new List<EventPair> ();
			_removeQueue = new List<EventPair> ();
		}

		public void AddObserver(IObserver pObserver, string pEvent)
		{
			if (pObserver != null) 
			{
				if (_working == 0)
					DoAdd (pObserver, pEvent);
				else
					_addQueue.Add (new EventPair (pObserver, pEvent));
			}
		}

		public void ClearObservers()
		{
			eventMap.Clear ();
		}

		private void DoAdd(IObserver pObserver, string pEvent)
		{
			List<IObserver> vObservers = new List<IObserver>();
			if ( eventMap.ContainsKey(pEvent) && eventMap.TryGetValue (pEvent, out vObservers)) 
			{
				if (!vObservers.Contains (pObserver)) 
				{
					vObservers.Add (pObserver);
				}
			}
			else 
			{
				vObservers.Add(pObserver);
				eventMap.Add(pEvent, vObservers);
			}
		}

		private void DoRemove(IObserver pObserver, string pEvent)
		{
			List<IObserver> vObservers = new List<IObserver>();
			if ( eventMap.ContainsKey(pEvent) && eventMap.TryGetValue (pEvent, out vObservers)) 
			{
				if (vObservers.Contains (pObserver)) 
				{
					vObservers.Remove (pObserver);
				}
			}
		}

		private void HandleQueues()
		{
			foreach (EventPair vAdd in _addQueue) 
			{
				DoAdd (vAdd.observer, vAdd.eventValue);
			}
			_addQueue.Clear ();


			foreach (EventPair vRemove in _removeQueue) 
			{
				DoRemove (vRemove.observer, vRemove.eventValue);
			}
			_removeQueue.Clear ();
		}

		public void NotifyObservers(string pEvent, Arg pArg)
		{
			List<IObserver> vObservers = new List<IObserver>();
			if ( eventMap.ContainsKey(pEvent) && eventMap.TryGetValue (pEvent, out vObservers)) 
			{
				_working++;
				foreach ( IObserver vObserver in vObservers )
				{
					vObserver.PlayEvent(pEvent, pArg);
				}
				_working--;
				if (_working == 0)
				{
					HandleQueues();
				}
			}
		}

		public delegate void AsyncCallback();
		private void OnCoroutinePerformed()
		{
			_working--;
			if(_working == 0)
			{
				HandleQueues();
			}
		}


		public void RemoveObserver(IObserver pObserver, string pEvent)
		{
		if (pObserver != null) {
			if (_working == 0)
				DoRemove (pObserver, pEvent);
			else
				_removeQueue.Add (new EventPair (pObserver, pEvent));
		}
	}
}
