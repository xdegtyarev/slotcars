using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour {

	public virtual void OnEnable(){
		Debug.Log("OnEnable" + gameObject.name);
	}

	public virtual void Toggle(){
		Debug.Log("Toggling " + gameObject.name);
		gameObject.SetActive(!gameObject.activeSelf);
	}

	public virtual void OnDisable(){
		Debug.Log("OnDisable" + gameObject.name);
	}
}
