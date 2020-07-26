using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {

	public static T Random<T>(this List<T> list) where T:class
	{
		if(list.Count==0){
			return null;
		}else{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
	}

	public static int Random(this List<int> list)
	{
		if(list.Count==0){
			return -1;
		}else{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
	}

	public static T Random<T>(this T[] list){
		return list[UnityEngine.Random.Range(0, list.Length)];
	}

	public static List<int> GetNumberedList(int size){
		var list = new List<int>();
		for(int i = 0; i<size;i++){
			list.Add(i);
		}
		return list;
	}

	public static bool IsInRange(this float v, float left, float right){
		return v <= right && v >= left;
	}

	public static float Round(this float v,int signs){
		if(signs == 0){
			return Mathf.FloorToInt(v);
		}else{
			int p = Mathf.FloorToInt(Mathf.Pow(10, signs));
			return Mathf.Floor(v*p)/p;
		}
	}

	public static float FloorToSpecificInt(this float v,int baseV){
		return Mathf.FloorToInt((v/baseV))*baseV;
	}
}
