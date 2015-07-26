using System;
using System.Collections.Generic;
using UnityEngine;


public class EventManager
{
	private static Dictionary<string, List<GameObject>> Messages;

	static EventManager()
	{
		Messages = new Dictionary<string, List<GameObject>>();
	}
	public static void RegisterListener(string message, GameObject obj)
	{
			if (!Messages.ContainsKey (message)) {
				Messages[message] = new List<GameObject>();
			}
			Messages [message].Add (obj);
	}
	public static void UnregisterListener(string message, GameObject obj)
	{
			if (Messages.ContainsKey (message)) {
				if(Messages[message].Contains(obj))
					Messages[message].Remove(obj);
			}
	}
	public static void SendMessage(string message)
	{
		if(Messages.ContainsKey(message))
		{
			foreach(var obj in Messages[message])
			{
				obj.SendMessage(message);
			}
		}
		if(message == "OnGameOver") {
			Messages.Clear();
		}
	}
	public static void SendMessage(string message, object param)
	{
		if(Messages.ContainsKey(message))
		{
			foreach(var obj in Messages[message])
			{
				obj.SendMessage(message, param);
			}
		}
	}
}
