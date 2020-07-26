using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	public static UIManager Instance;
	[SerializeField] UIScreen[] screens;
	public Dictionary<System.Type,UIScreen> screensDatabase;

	void Awake(){
		Instance = this;
		screensDatabase = new Dictionary<System.Type,UIScreen>();
		foreach(var o in screens){
			screensDatabase.Add(o.GetType(),o);
		}
	}

	public T GetScreen<T>() where T:UIScreen{
		if(screensDatabase.ContainsKey(typeof(T))){
			return screensDatabase[typeof(T)] as T;
		}else{
			Debug.Log("No Such Screen:" + typeof(T));
			return null;
		}
	}
}
