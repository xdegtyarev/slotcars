using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReplayHandler {

    public float totalTime;
    
    public List<SerializableTransform> _serializableTransforms;
    public List<float> _progressPositions;

    public ReplayHandler() {
        _serializableTransforms = new List<SerializableTransform>();
        _progressPositions = new List<float>();
    }

    public void Add(Transform transform, float currentPosition) {
        _serializableTransforms.Add(new SerializableTransform(transform));
        _progressPositions.Add(currentPosition);
    }

    public void AddRange(List<Transform> transforms, List<float> progressPositions) {
        for (int i = 0; i < transforms.Count; i++) {
            Add(transforms[i], progressPositions[i]);
        }
    }

    public bool IsEmpty() {
        return _serializableTransforms.Count == 0;
    }

    public Vector3 GetPositionAt(int index) {
        if (_serializableTransforms.Count <= index) {
            return _serializableTransforms[_serializableTransforms.Count - 1].GetPosition();
        }
        return _serializableTransforms[index].GetPosition();
    }
    
    public Vector3 GetRotationAt(int index) {
        if (_serializableTransforms.Count <= index) {
            return _serializableTransforms[_serializableTransforms.Count - 1].GetRotation();
        }
        return _serializableTransforms[index].GetRotation();
    }

    public float GetProgressPosition(int index) {
        if (_progressPositions.Count <= index) {
            return _progressPositions[_progressPositions.Count - 1];
        }
        return _progressPositions[index];
    }
}