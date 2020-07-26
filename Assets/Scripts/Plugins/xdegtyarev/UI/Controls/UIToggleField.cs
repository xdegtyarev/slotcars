using UnityEngine;
using System;
using UnityEngine.UI;

public class UIToggleField : MonoBehaviour {
	[SerializeField] Text label;
	[SerializeField] Toggle inputToggle;
	Action onCompleteAction;

	public void Setup(string text, bool val,Action callback = null){
		label.text = text;
		inputToggle.isOn = val;//will miss first selection
		onCompleteAction = callback;
	}

	public void OnCompleteInput(){
		if(onCompleteAction!=null){
			onCompleteAction();
		}
	}

	public bool GetValue(){
		return inputToggle.isOn;
	}
}
