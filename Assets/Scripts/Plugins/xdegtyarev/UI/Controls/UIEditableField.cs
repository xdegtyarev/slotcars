using UnityEngine;
using System;
using UnityEngine.UI;

public class UIEditableField : MonoBehaviour {
	[SerializeField] Text label;
	[SerializeField] InputField inputField;
	Action onCompleteAction;

	public void Setup(string text, string val,Action callback = null,InputField.ContentType contentType = InputField.ContentType.Standard){
		label.text = text;
		inputField.contentType = contentType;
		inputField.text = string.IsNullOrEmpty(val)?"":val;
		onCompleteAction = callback;
	}

	public void OnCompleteInput(){
		onCompleteAction();
	}

	public string GetValue(){
		return inputField.text;
	}
}
