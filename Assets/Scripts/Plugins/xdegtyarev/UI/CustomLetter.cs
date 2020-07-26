using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomLetter : MonoBehaviour, IUIContainerItem {
	[SerializeField] Image num;
	[SerializeField] Image shadow;
	[SerializeField] Sprite[] nums;
	[SerializeField] Sprite[] shadows;
	int number;
	public void Setup(object itemData, IUIContainer container) {
		number = (itemData as int?).GetValueOrDefault();
		UpdateState();
	}
	public void UpdateState() {
		shadow.sprite = shadows[number];
		num.sprite = nums[number];
	}

	public GameObject GetGameObject() {
		return gameObject;
	}
}
