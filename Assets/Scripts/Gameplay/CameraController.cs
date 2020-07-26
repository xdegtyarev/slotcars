using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController Instance;
    [SerializeField]Vector3 delta;
    [SerializeField]Transform target;
    [SerializeField]bool followMove;
    [SerializeField]bool followLook;

    Camera camera;

    void Awake() {
        Instance = this;
        camera = GetComponent<Camera>();
    }


    void LateUpdate(){
        if(followMove){
            transform.position = target.transform.position - target.transform.forward*delta.z + target.transform.up*delta.y;
        }
        if(followLook){
            transform.LookAt(target);
        }
    }

    public void ToggleFollowMove(){
        followMove = !followMove;
    }

    // public static CameraController INSTANCE;

    // private const int PULL_DISTANCE = 5;

    // private bool pullOn = false;
    // public IEnumerator PullOff() {
    //     // for (float f = PULL_DISTANCE; f >= 0; f -= 0.1f) {
    //     //     transform.position -= transform.forward * 0.1f;
    //     //     yield return new WaitForSeconds(0.02f);
    //     // }
    // }

    // public IEnumerator PullOn() {
    //     // for (float f = PULL_DISTANCE; f >= 0; f -= 0.1f) {
    //     //     transform.position += transform.forward * 0.1f;
    //     //     yield return new WaitForSeconds(0.02f);
    //     // }
    // }

    // public Vector3 GetEnenmyIndicatorPosition(Vector3 transformPosition) {
    //     return _camera.WorldToScreenPoint(transformPosition);
    // }
}
