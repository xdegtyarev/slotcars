using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour {
	[SerializeField] UIWindow[] windows;
	Dictionary<System.Type,UIWindow> windowsDatabase;

	protected virtual void Awake(){
		windowsDatabase = new Dictionary<System.Type,UIWindow>();
		foreach(var o in windows){
			windowsDatabase.Add(o.GetType(), o);
		}
	}

	protected virtual void OnEnable(){

	}

	protected virtual void OnDisable(){

	}

	public T GetWindow<T>() where T:UIWindow{
		if(windowsDatabase.ContainsKey(typeof(T))){
			return windowsDatabase[typeof(T)] as T;
		}else{
			Debug.Log("No Such Window:" + typeof(T));
			return null;
		}
	}

	public virtual void Toggle(){
		gameObject.SetActive(true);
	}
}
