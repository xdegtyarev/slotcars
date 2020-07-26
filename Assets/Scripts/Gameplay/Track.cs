using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class TrackPoint{
	public Vector3 position;
	public Vector3 forward;
	public Vector3 up;
}

public class Track : MonoBehaviour {
	public static Track Instance;
	// [SerializeField] MeshInformer meshInformer;
	// [SerializeField] PathController pathController;
	[SerializeField] GameObject carPrefab;
	[SerializeField] SplineComputer spline;

	float trackLength;

	// bool showReplay;
	//	 List<Vector3> bestPositions;
	// GameObject bestCar;
	// int bestCarStep;

	// ReplayHandler bestReplayHandler;

	// bool replaySaved = false;
	// const float HALF_HEIGHT_OF_CAR = 0.25f;

	void Update(){

	}

	void Awake(){
		Instance = this;
		trackLength = spline.CalculateLength();
		// bestReplayHandler = GameReplaySaver.INSTANCE.Load();
		// if (bestReplayHandler != null && !bestReplayHandler.IsEmpty()) {
		// 	bestCar = Instantiate(carPrefab, Vector3.zero, Quaternion.identity);
		// 	showReplay = true;
		// } else {
		// 	// UIController.INSTANCE.DisableEnemyProgress();
		// }

		// var shapes = new List<List<Vector3>>();
		// for (int i = 0; i < 4; i++) {
		// 	var vertecies = meshInformer.GetVertecies(i);
		// 	shapes.Add(vertecies);
		// }

//		for (int i = 0; i < 4; i++) {
//			var mesh = MeshCreator.extrudeAlongPath(meshPath, trackWidth, shapes[i]);
//			var meshFilter = GameObject.Find("Track" + i).GetComponent<MeshFilter>();
//			meshFilter.mesh = mesh;
//			AssetDatabase.CreateAsset(meshFilter.mesh, "Assets/Objects/mesh" + i + ".asset");
//			AssetDatabase.SaveAssets();
//		}
	}

	public float GetRelativeSpeed(float speed){
		return speed/trackLength;
	}

	public SplineResult GetTrackDataAt(double normalizedTrackProgress){
		return spline.Evaluate(normalizedTrackProgress);
		// PathController.Instance.GetWorldPosition(normalizedTrackProgress)
	}

	void Start(){
		// Vector3[] meshPath = pathController.CalcPathForMesh();
		// var oneLapPathList = new List<Vector3>(pathController.GetPath());
		// var firstPoint = oneLapPathList[0];
		// var lastPoint = oneLapPathList[oneLapPathList.Count - 1];
		// oneLapPathList.RemoveAt(0);
		// oneLapPathList.RemoveAt(oneLapPathList.Count - 1);
		// var localPathList = new List<Vector3> { firstPoint };
		// for (int i = 0; i < lapsNum; i++) {
		// 	localPathList.AddRange(oneLapPathList);
		// }
		// localPathList.Add(lastPoint);

		// localPathList.RemoveAt(localPathList.Count - 1);
		// pathForGizmos = localPathList.ToArray();
	}
}
