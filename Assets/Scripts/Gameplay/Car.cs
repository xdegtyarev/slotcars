using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Car : MonoBehaviour {
	[SerializeField] Gradient predictionGradient;
	public static Car player;
	const float PREDICTION_CAR_SPEED_THRESHOLD = 0.01f;
	[SerializeField] bool isPlayer;
	[SerializeField] GameObject carView;
	[SerializeField] GameObject trigger;
	[SerializeField] public float minSpeed;
	[SerializeField] public float maxSpeed;
	[SerializeField] public float acceleration;
	[SerializeField] public float deceleration;
	[SerializeField] public float crashDuration;
	[SerializeField] public float criticalThrowoutValue;
	[SerializeField] public float triggerDelta;
	[SerializeField] MeshRenderer recolorMat;
	[SerializeField] GameObject explodeFX;
	public float normalizedTrackProgress;
	public float currentSpeed;
	public int currentLap;
	public float currentTime;
	public float lastLapTime;
	public float currentBestTime;

	public float currentThrowout;
	public SplineResult currentTrackPoint;

	bool isCrashing;
	float crashEndTime;

	SplineResult nextTrackPoint;

	Transform cachedTransform;
	bool started;

	void Awake() {
		if (isPlayer) {
			player = this;
		}
		cachedTransform = GetComponent<Transform>();
	}

	void Start() {
		Reset();
	}

	public void Reset() {
		Debug.Log("Reset Car " + name);
		started = false;
		currentLap = 0;
		currentSpeed = 0f;
		currentTime = 0f;
		currentBestTime = 0f;
		lastLapTime = 0f;
		normalizedTrackProgress = 0f;
		currentTrackPoint = Track.Instance.GetTrackDataAt(normalizedTrackProgress);
		FixedUpdateTransform();
	}

	void Update() {
		if (isCrashing) {
			UpdateCrashingTransfom();
			UpdateCrashingTime();
		} else {
			if (isPlayer) {
				UpdateInput();
			}
		}
		UpdateTime();
	}

	float prevPulse = 0f;
	int predPointCount = 10;
	void FixedUpdateMat(){
		float totalAccumVal = 0f;
		//SPEED BOUND float trigDelta = triggerDelta*currentSpeed*Time.fixedDeltaTime;
		float trigDelta = triggerDelta;
		for(int i = 0; i<predPointCount; i++){
			totalAccumVal += GetThrowoutPulse(currentTrackPoint.direction.normalized,Track.Instance.GetTrackDataAt(normalizedTrackProgress+(trigDelta/predPointCount)*i).direction.normalized);
		}

		recolorMat.material.color = predictionGradient.Evaluate(Mathf.Clamp01(
				(totalAccumVal/(float)predPointCount)
				/
				Car.player.criticalThrowoutValue)
			);
	}

	void FixedUpdate() {
		if (!isCrashing) {
			FixedUpdateMovement();
			FixedUpdateTransform();
			if(isPlayer){
				// FixedUpdateMat();
			}
		}
	}



	void UpdateTime() {
		if (started) {
			currentTime += Time.deltaTime;
		}
	}

	void UpdateCrashingTime() {
		if (Time.timeSinceLevelLoad > crashEndTime) {
			crashEndTime = 0f;
			isCrashing = false;
			currentSpeed = minSpeed;
			FixedUpdateTransform();
			normalizedTrackProgress += 0.01f;
			GetComponent<BoxCollider>().enabled = true;
			carView.SetActive(true);
			if(isPlayer){
				CameraController.Instance.ToggleFollowMove();
			}
		}
	}

	void UpdateCrashingTransfom() {
		cachedTransform.localPosition += currentTrackPoint.direction.normalized * currentSpeed * Time.deltaTime;
	}

	public float GetThrowoutPulse(Vector3 normalizedStartForward, Vector3 normalizedEndForward) {
		return currentSpeed * (Car.player.currentSpeed > PREDICTION_CAR_SPEED_THRESHOLD ? Mathf.Abs(Mathf.Sin(Vector3.Angle(normalizedStartForward, normalizedEndForward))) : 0f);
	}

	SplineResult triggerPoint;
	void FixedUpdateMovement() {
		currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

		if(isPlayer){
			// SPEED BOUNDtriggerPoint = Track.Instance.GetTrackDataAt(normalizedTrackProgress+triggerDelta*currentSpeed*Time.fixedDeltaTime);
			triggerPoint = Track.Instance.GetTrackDataAt(normalizedTrackProgress+triggerDelta);
			// trigger.transform.position = triggerPoint.position;
		}

		normalizedTrackProgress += Track.Instance.GetRelativeSpeed(currentSpeed * Time.fixedDeltaTime);

		if (normalizedTrackProgress >= 1f) {
			normalizedTrackProgress -= 1f;
			currentLap++;
			if (currentBestTime <= 0.01f || currentTime <= currentBestTime) {
				currentBestTime = currentTime;
			}
			lastLapTime = currentTime;
			currentTime = 0f;
		}

		nextTrackPoint = Track.Instance.GetTrackDataAt(normalizedTrackProgress);

		currentThrowout = GetThrowoutPulse(currentTrackPoint.direction, nextTrackPoint.direction);
		if (currentThrowout >= criticalThrowoutValue) {
			// Crash();
		}else{
			currentTrackPoint = nextTrackPoint;
		}
	}

	void FixedUpdateTransform() {
		cachedTransform.position = currentTrackPoint.position;
		cachedTransform.localRotation = currentTrackPoint.rotation;
	}

	void UpdateInput() {
		if (Input.touchCount > 0 || Input.GetKey(KeyCode.Space)) {
			started = true;
			currentSpeed += acceleration * Time.deltaTime;
		} else {
			currentSpeed -= deceleration * Time.deltaTime;
		}
	}

	public void Crash() {
		if(isPlayer){
			CameraController.Instance.ToggleFollowMove();
		}

		Destroy(GameObject.Instantiate(explodeFX,transform.position,Quaternion.identity),4f);
		isCrashing = true;
		carView.SetActive(false);
		GetComponent<BoxCollider>().enabled = false;
		crashEndTime = Time.timeSinceLevelLoad + crashDuration;
	}

	// void FixedUpdate() {
	// if (normalizedTrackProgress > lapsNum) {
	// 	UIManager.Instance.GetScreen<UIGameScreen>().EnableFinishText();
	// 	// if (!replaySaved) {
	// 	// 	float bestTotalTime = bestReplayHandler != null ? bestReplayHandler.totalTime : float.MaxValue;
	// 	// 	if (totalTime < bestTotalTime) {
	// 	// 		GameReplaySaver.INSTANCE.Save(totalTime);
	// 	// 	}
	// 	// 	replaySaved = true;
	// 	// }
	// 	return;
	// }
	// if (showReplay) {
	// 	var transformPosition = bestReplayHandler.GetPositionAt(bestCarStep);
	// 	bestCar.transform.position = transformPosition;
	// 	var enemyIndicatorPosition = CameraController.INSTANCE.GetEnenmyIndicatorPosition(bestCar.transform.position + Vector3.up * 5f);
	// 	var magnitude = (bestCar.transform.position - cachedTransform.position).magnitude;
	// 	UIManager.Instance.GetScreen<UIGameScreen>().SetEnemyIndicatorPosition(enemyIndicatorPosition, magnitude);
	// 	bestCar.transform.rotation = Quaternion.Euler(bestReplayHandler.GetRotationAt(bestCarStep));
	// 	bestCarStep++;
	// }
	// GameReplaySaver.INSTANCE.AddControlPoint(gameObject.transform, normalizedTrackProgress / lapsNum);
	// int currentLap = Mathf.FloorToInt(normalizedTrackProgress / (1.0f / lapsNum));
	// currentTime = Time.time;
	// if (isCrashing) {
	// 	if (Time.time - crashStartTime >= crashTimeOut) {
	// 		isCrashing = false;
	// 		currentSpeed = minSpeed;
	// 		// UIManager.Instance.GetScreen<UIGameScreen>().DisableCrashTimeText();
	// 		StartCoroutine(CameraController.INSTANCE.PullOn());
	// 	} else {
	// 		// UIManager.Instance.GetScreen<UIGameScreen>().SetCrashTimeText(Mathf.FloorToInt(crashTimeOut - (Time.time - crashStartTime)) + 1);
	// 		cachedTransform.position += cachedTransform.forward * currentSpeed;
	// 		currentSpeed -= deceleration * 10;
	// 		if (currentSpeed < minSpeed) {
	// 			currentSpeed = minSpeed;
	// 		}
	// 	}
	// 	return;
	// }
	// }
}
