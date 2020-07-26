using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameSettingsWindow : UIWindow {
	[SerializeField] UIEditableField minSpeedField;
	[SerializeField] UIEditableField maxSpeedField;
	[SerializeField] UIEditableField accelerationField;
	[SerializeField] UIEditableField decelerationField;
	[SerializeField] UIEditableField crashDurationField;
	[SerializeField] UIEditableField criticalThrowoutValueField;

	bool isSetting;

	void OnEnable() {
		Setup();
	}

	void Setup() {
		isSetting = true;
		minSpeedField.Setup("minSpeed", Car.player.minSpeed.ToString(), OnApply, UnityEngine.UI.InputField.ContentType.DecimalNumber);
		maxSpeedField.Setup("maxSpeed", Car.player.maxSpeed.ToString(), OnApply, UnityEngine.UI.InputField.ContentType.DecimalNumber);
		accelerationField.Setup("acceleration", Car.player.acceleration.ToString(), OnApply, UnityEngine.UI.InputField.ContentType.DecimalNumber);
		decelerationField.Setup("deceleration", Car.player.deceleration.ToString(), OnApply, UnityEngine.UI.InputField.ContentType.DecimalNumber);
		criticalThrowoutValueField.Setup("throwout", Car.player.criticalThrowoutValue.ToString(), OnApply, UnityEngine.UI.InputField.ContentType.DecimalNumber);
		crashDurationField.Setup("crashDuration", Car.player.crashDuration.ToString(), OnApply, UnityEngine.UI.InputField.ContentType.DecimalNumber);
		isSetting = false;
	}

	void OnApply() {
		if (!isSetting) {
			float floatRes;
			Car.player.minSpeed = float.TryParse(minSpeedField.GetValue(), out floatRes) ? floatRes : 0f;
			Car.player.maxSpeed = float.TryParse(maxSpeedField.GetValue(), out floatRes) ? floatRes : 0f;
			Car.player.acceleration = float.TryParse(accelerationField.GetValue(), out floatRes) ? floatRes : 0f;
			Car.player.deceleration = float.TryParse(decelerationField.GetValue(), out floatRes) ? floatRes : 0f;
			Car.player.criticalThrowoutValue = float.TryParse(criticalThrowoutValueField.GetValue(), out floatRes) ? floatRes : 0f;
			Car.player.crashDuration = float.TryParse(crashDurationField.GetValue(), out floatRes) ? floatRes : 0f;
		}
	}

	public void OnRestart() {
		gameObject.SetActive(false);
	}
}
