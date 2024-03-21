using UnityEngine;
using UnityEditor;

public class RandomizeTransformEditorWindow : EditorWindow
{
    private static Vector3 _minPosition = new Vector3(-2f, 7f, -2.5f);
    private static Vector3 _maxPosition = new Vector3(2f, 8f, 2.5f);

    private static Vector3 _minRotation = new Vector3(-90f, -90f, -90f);
    private static Vector3 _maxRotation = new Vector3(90f, 90f, 90f);

    private static float _minDistance = 1f;

    [MenuItem("Tools/Randomize Transform/Settings")]
    public static void ShowWindow()
    {
        GetWindow<RandomizeTransformEditorWindow>("Randomize Transform Settings");
    }

    private void OnGUI()
    {
        GUILayout.Label("Position Settings", EditorStyles.boldLabel);
        _minPosition = EditorGUILayout.Vector3Field("Min Position", _minPosition);
        _maxPosition = EditorGUILayout.Vector3Field("Max Position", _maxPosition);

        GUILayout.Label("Rotation Settings", EditorStyles.boldLabel);
        _minRotation = EditorGUILayout.Vector3Field("Min Rotation", _minRotation);
        _maxRotation = EditorGUILayout.Vector3Field("Max Rotation", _maxRotation);

        GUILayout.Label("Distance Settings", EditorStyles.boldLabel);
        _minDistance = EditorGUILayout.FloatField("Min Distance Between Objects", _minDistance);

        if (GUILayout.Button("Apply Randomization"))
        {
            RandomizePositionRotationWithDistance();
        }
    }

    static void RandomizePositionRotationWithDistance()
    {
        var selectedObjects = Selection.gameObjects;
        var newPositions = new System.Collections.Generic.List<Vector3>();

        foreach (GameObject obj in selectedObjects)
        {
            Vector3 randomPosition = Vector3.zero;
            
            bool positionValid = false;
            int attemptCount = 0;

            while (!positionValid && attemptCount < 100)
            {
                attemptCount++;

                randomPosition = new Vector3(
                    Random.Range(_minPosition.x, _maxPosition.x),
                    Random.Range(_minPosition.y, _maxPosition.y),
                    Random.Range(_minPosition.z, _maxPosition.z));

                positionValid = true;

                foreach (var pos in newPositions)
                {
                    if (Vector3.Distance(pos, randomPosition) < _minDistance)
                    {
                        positionValid = false;
                        break;
                    }
                }
            }

            newPositions.Add(randomPosition);

            Quaternion randomRotation = Quaternion.Euler(
                Random.Range(_minRotation.x, _maxRotation.x),
                Random.Range(_minRotation.y, _maxRotation.y),
                Random.Range(_minRotation.z, _maxRotation.z));

            Undo.RecordObject(obj.transform, "Randomize Position and Rotation with Distance");
            obj.transform.position = randomPosition;
            obj.transform.rotation = randomRotation;
        }
    }
}