using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : UIScreen {
    [SerializeField] Text timeLabel;
    [SerializeField] Slider trackProgress;
    [SerializeField] Image speedImage;
    [SerializeField] Text speedLabel;
    [SerializeField] Text lapLabel;
    [SerializeField] Text bestTime;
    [SerializeField] Text lastTime;
    [SerializeField] Text throwout;
    // [SerializeField] Image enemyIndicatorImage;
    // const float indicatorFactor = 164/144f;

    void Update() {
        LapTimeUpgrade();
        SpeedUpdate(Car.player.currentSpeed, Car.player.maxSpeed);
        LapProgressUpdate(Car.player.normalizedTrackProgress);
        throwout.text = Car.player.currentThrowout.ToString();
    }

    void LapTimeUpgrade() {
        timeLabel.text = FloatTimeToProgress(Car.player.currentTime);
        bestTime.text = "best: " + FloatTimeToProgress(Car.player.currentBestTime);
        lastTime.text = "last: " + FloatTimeToProgress(Car.player.lastLapTime);
    }

    string FloatTimeToProgress(float time){
        int fullseconds = Mathf.FloorToInt(time);
        int ms = Mathf.FloorToInt((time - fullseconds)*1000);
        int fullhours = fullseconds/3600;
        int fullminutes = fullseconds%3600/60;
        fullseconds = fullseconds%60;
        return fullhours.ToString("d2")+":"+fullminutes.ToString("d2")+":"+fullseconds.ToString("d2")+"."+ms.ToString("d3");
    }

    void SpeedUpdate(float currentSpeed, float maxSpeed) {
        speedLabel.text = (currentSpeed / maxSpeed * 100).ToString("0.");
        speedImage.fillAmount = currentSpeed / maxSpeed;
    }

    void LapProgressUpdate(float position) {
        trackProgress.value = (position - Mathf.Floor(position));
        lapLabel.text = "Lap: " + (Mathf.FloorToInt(Car.player.currentLap) + 1);
    }

    public void SetEnemyIndicatorPosition(Vector3 enemyIndicatorPosition, float distance) {
        // Vector2 scRes = screenResolution / 2.0f;
//        float eps = 1f;
        // float x = enemyIndicatorPosition.x;
//        if (Mathf.Abs(x) > scRes.x  * eps) {
//            x = Mathf.Sign(x) * scRes.x * eps;
//        }
        // float y = enemyIndicatorPosition.y;
//        if (Mathf.Abs(y) > scRes.y * eps) {
//            y = Mathf.Sign(y) * scRes.y * eps;
//        }
        // Vector3 position = new Vector3(x, y, 0);
        // enemyIndicatorImage.rectTransform.position = position;
        // var sizeDelta = enemyIndicatorSize * (10 / distance);
        // var rectTransformSizeDelta = new Vector2(
        //     Mathf.Max(Mathf.Min(144, Mathf.Round(sizeDelta.x)), 40),
        //     Mathf.Max(Mathf.Min(164, Mathf.Round(sizeDelta.y)), 40 * indicatorFactor)
        //     );
        // enemyIndicatorImage.rectTransform.sizeDelta = rectTransformSizeDelta;
    }

    public void EnableFinishText() {
        Debug.Log("Finish");
    }

    public void EnableCrashTimeText(int time) {
        Debug.Log("Crash");
    }

    public void OnSettingsButtonClick(){
        GetWindow<UIGameSettingsWindow>().Toggle();
    }

    public void OnRestartClick(){
        Car.player.Reset();
    }


}
