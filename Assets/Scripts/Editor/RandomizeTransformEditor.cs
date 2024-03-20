using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomizeTransformEditor : ScriptableObject
{
    private static float _positionXMinValue = -2f;
    private static float _positionXMaxValue = 2f;

    private static float _positionYMinValue = 7f;
    private static float _positionYMaxValue = 8f;

    private static float _positionZMinValue = -2.5f;
    private static float _positionZMaxValue = 2.5f;

    private static float _rotationXMinValue = -90f;
    private static float _rotationXMaxValue = 90f;

    private static float _rotationYMinValue = -90f;
    private static float _rotationYMaxValue = 90f;

    private static float _rotationZMinValue = -90f;
    private static float _rotationZMaxValue = 90f;

    private const float MinDistance = 1f;

    [MenuItem("Tools/Randomize Transform/Position and Rotation with Distance")]
    static void RandomizePositionRotationWithDistance()
    {
        var selectedObjects = Selection.gameObjects;

        List<Vector3> newPositions = new List<Vector3>();

        foreach (GameObject obj in selectedObjects)
        {
            Vector3 randomPosition = Vector3.zero;
            bool positionValid = false;

            while (!positionValid)
            {
                randomPosition = new Vector3(
                    Random.Range(_positionXMinValue, _positionXMaxValue),
                    Random.Range(_positionYMinValue, _positionYMaxValue),
                    Random.Range(_positionZMinValue, _positionZMaxValue));

                positionValid = true;

                foreach (var pos in newPositions)
                {
                    if (Vector3.Distance(pos, randomPosition) < MinDistance)
                    {
                        positionValid = false;
                        break;
                    }
                }
            }

            newPositions.Add(randomPosition);

            Quaternion randomRotation = Quaternion.Euler(
                Random.Range(_rotationXMinValue, _rotationXMaxValue),
                Random.Range(_rotationYMinValue, _rotationYMaxValue),
                Random.Range(_rotationZMinValue, _rotationZMaxValue));

            Undo.RecordObject(obj.transform, "Randomize Position and Rotation with Distance");
            obj.transform.position = randomPosition;
            obj.transform.rotation = randomRotation;
        }
    }
}