using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PredictionTrack : MonoBehaviour {

	List<GameObject> predictionPointViews;
	List<SplineResult> predictionPointSplineData;
	[SerializeField] GameObject predictionPointPrefab;
	[SerializeField] public int predictionPointsCount;
	[SerializeField] Gradient predictionGradient;

	void Start() {
		InitPredictionPoints();
	}

	void Update() {
		UpdatePredictionPointsView();
	}

	void InitPredictionPoints() {
		predictionPointViews = new List<GameObject>();
		predictionPointSplineData = new List<SplineResult>();
		GameObject point;
		SplineResult pointSplineData;
		for (int i = 0; i < predictionPointsCount; i++) {
			point = Instantiate(predictionPointPrefab);
			pointSplineData = Track.Instance.GetTrackDataAt((float)i / (float)predictionPointsCount);

			predictionPointViews.Add(point);
			predictionPointSplineData.Add(pointSplineData);

			point.transform.parent = transform;
			point.transform.position = pointSplineData.position;
			point.transform.localScale = new Vector3(2f,2f,2f);
			point.transform.localRotation = Quaternion.LookRotation(pointSplineData.direction,pointSplineData.normal);
			point.transform.Rotate(Vector3.right, 90);
			point.transform.localPosition+=Vector3.up*0.1f;
		}
	}

	void OnDrawGizmos(){
		if(predictionPointViews!=null && predictionPointViews.Count>0){
			for (int i = 0; i < predictionPointsCount; i++) {
				Gizmos.color = Color.red;
				Gizmos.DrawRay(predictionPointViews[i].transform.position,predictionPointSplineData[i].direction.normalized);
				Gizmos.color = Color.green;
				Gizmos.DrawRay(predictionPointViews[i].transform.position,predictionPointSplineData[i].normal.normalized);
			}
		}
	}



	void UpdatePredictionPointsView() {
		for (int i = 1; i < predictionPointsCount; i++) {
			predictionPointViews[i].GetComponent<MeshRenderer>().material.color = predictionGradient.Evaluate(
				Mathf.Clamp01(
					Car.player.GetThrowoutPulse(predictionPointSplineData[i-1].direction.normalized, predictionPointSplineData[i].direction.normalized)
					/
					Car.player.criticalThrowoutValue)
				);
		}
		predictionPointViews[0].GetComponent<MeshRenderer>().material.color = predictionGradient.Evaluate(
				Mathf.Clamp01(
					Car.player.GetThrowoutPulse(predictionPointSplineData[predictionPointsCount-1].direction.normalized, predictionPointSplineData[0].direction.normalized)
					/
					Car.player.criticalThrowoutValue)
				);
	}
}
