using System;
using UnityEngine;

[Serializable]
public class SerializableTransform
{
    [SerializeField] public float positionX;
    [SerializeField] public float positionY;
    [SerializeField] public float positionZ;

    [SerializeField] public float eulerX;
    [SerializeField] public float eulerY;
    [SerializeField] public float eulerZ;


    public SerializableTransform(Transform transform)
    {
        this.positionX = transform.position.x;
        this.positionY = transform.position.y;
        this.positionZ = transform.position.z;
        this.eulerX = transform.rotation.eulerAngles.x;
        this.eulerY = transform.rotation.eulerAngles.y;
        this.eulerZ = transform.rotation.eulerAngles.z;
    }

    public Vector3 GetPosition() {
        return new Vector3(positionX, positionY, positionZ);
    }


    public Vector3 GetRotation() {
        return new Vector3(eulerX, eulerY, eulerZ);
    }
}