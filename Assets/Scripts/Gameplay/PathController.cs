// using System;
// using System.Collections.Generic;
// using UnityEngine;

// public class PathController : MonoBehaviour {
//     public static PathController Instance;
//     [SerializeField] float trackGenerationStep;
//     [SerializeField] Transform[] rotations;
//     // [SerializeField] iTweenPath path;
//     float[] nodesDistations;

//     Vector3[] additionalPointPositions;
//     Vector3[] controlPointRotations;

//     public float pathLength;
//     float step;


//     void Awake() {
//         Instance = this;
//         // additionalPointPositions = GenerateFinalPath(path.nodes.ToArray());
//         // additionalPointPositions = path.nodes.ToArray();
//         nodesDistations = PathDeltas(additionalPointPositions);
//         pathLength = iTween.PathLength(additionalPointPositions);
//         step = trackGenerationStep / pathLength;
//     }

//     //WHUT?
//     // Vector3[] GenerateFinalPath(Vector3[] positions) {
//     //     var finalPositions = new List<Vector3> { positions[positions.Length - 2] };
//     //     finalPositions.AddRange(positions);
//     //     finalPositions.Add(positions[1]);

//     //     return finalPositions.ToArray();
//     // }



//     public static float[] PathDeltas(Vector3[] path) {
//         List<float> pathLength = new List<float>();
//         pathLength.Add(0);

//         Vector3[] vector3s = iTween.PathControlPointGenerator(path);

//         //Line Draw:
//         Vector3 prevPt = iTween.Interp(vector3s, 0);
//         int smoothFactor = 20;
//         int smoothAmount = path.Length * smoothFactor;
//         float delta = 0;
//         for (int i = 1; i <= smoothAmount; i++) {
//             float pm = (float)i / smoothAmount;
//             Vector3 currPt = iTween.Interp(vector3s, pm);
//             delta += Vector3.Distance(prevPt, currPt);
//             if (i % smoothFactor == 0) {
//                 pathLength.Add(pathLength[pathLength.Count - 1] + delta);
//                 delta = 0;
//             }
//             prevPt = currPt;
//         }

//         float fullPathLength = iTween.PathLength(path);

//         for (int i = 1; i < pathLength.Count; i++) {
//             pathLength[i] = pathLength[i] / fullPathLength;
//             // Debug.Log("Path Progress" + pathLength[i]);
//         }
//         return pathLength.ToArray();
//     }


//     // public Vector3[] GetPath() {
//     //     return additionalPointPositions;
//     // }

//     public Vector3 GetRotation(float percent) {
//         int index = 0;
//         if (percent >= 1) {
//             return rotations[rotations.Length - 1].eulerAngles;
//         }
//         while (nodesDistations[index] <= percent) {
//             index++;
//         }

//         float distBetweenNodes = nodesDistations[index] - nodesDistations[index - 1];
//         float distForPoint = percent - nodesDistations[index - 1];
//         float percentOfMutation = distForPoint / distBetweenNodes;
//         Debug.Log("percent of mutation : " + percentOfMutation + " " + index);
//         return Quaternion.Slerp(rotations[index - 1].rotation, rotations[index].rotation, percentOfMutation)
//             .eulerAngles;


// //        var v = MutationOfVectors(rotations[index - 1].eulerAngles, rotations[index].eulerAngles, percentOfMutation);
// //        Debug.Log("between=" + distBetweenNodes + ",for point=" + distForPoint + ",percent=" + percentOfMutation + "v0=" + VD(rotations[index- 1])+ "v1=" + VD(rotations[index]) + "vect=" + VD(v));
// //        return v;
//     }

//     static float MinOfAngle(float e0, float e1) {
//         float a = e1 - e0;
//         float b = e0 - (360 - e1);
//         Debug.Log("min of angles : " + e0 + "    " + e1 + "    " + a + "    " + b);
//         return Math.Abs(a) < Math.Abs(b) ? a : b;
//     }

//     static Vector3 MutationOfVectors(Vector3 a, Vector3 b, float percent) {
//         Debug.Log("try mutation : " + VD(a) + "   " + VD(b));
//         Vector3 v = new Vector3(
//                               MinOfAngle(a.x, b.x),
//                               MinOfAngle(a.y, b.y),
//                               MinOfAngle(a.z, b.z)
//                           );

//         Debug.Log(" mutation vector : " + VD(v * percent) + " start angle : " + VD(a) + " difference : " + VD(v));

//         var mutationOfVectors = a + v * percent;
//         Debug.Log("result : " + VD(mutationOfVectors));
//         return mutationOfVectors;
//     }

//     static String VD(Vector3 a) {
//         return a.x + ", " + a.y + ", " + a.z;
//     }

//     static String VD(Transform t) {
//         return VD(t.rotation.eulerAngles);
//     }

//     public float GetPathLength() {
//         return iTween.PathLength(additionalPointPositions);
//     }

//     public Vector3[] CalcPathForMesh() {
//         var output = new List<Vector3>();
//         var currentStep = 0f;

//         while (currentStep <= 1.001f) {
//             output.Add(iTween.Interp(additionalPointPositions, currentStep));
//             currentStep += step;
//         }

//         return output.ToArray();
//     }

//     public Vector3 GetWorldPosition(float percentOfPath) {
//         return iTween.Interp(additionalPointPositions, percentOfPath);
//     }

//     public float GetStepLengthBySpeed(float speed) {
//         return speed / pathLength;
//     }

//     float[] GetNodesDistaions() {
//         float[] output = new float[rotations.Length];
//         float length = 0;
//         output[0] = 0;
//         for (int i = 0; i < rotations.Length - 1; i++) {
//             var delta = Vector3.Distance(rotations[i].position, rotations[i + 1].position);
//             output[i + 1] = output[i] + delta;
//             length += delta;
//         }
//         output[rotations.Length - 1] = 1;

//         for (int i = 1; i < rotations.Length - 1; i++) {
//             output[i] = output[i] / length;
//             Debug.Log(output[i]);
//         }

//         return output;
//     }
// }
