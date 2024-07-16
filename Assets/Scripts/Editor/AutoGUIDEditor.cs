using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AutoGUID))]
public class AutoGUIDEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AutoGUID autoGUID = (AutoGUID)target;

        if (string.IsNullOrEmpty(autoGUID.GUID) || !IsGUIDUnique(autoGUID.GUID))
        {
            autoGUID.SetGUID(GenerateUniqueGUID());
            EditorUtility.SetDirty(autoGUID);
        }

        EditorGUILayout.LabelField("GUID", autoGUID.GUID);

        if (GUILayout.Button("Generate New GUID"))
        {
            autoGUID.SetGUID(GenerateUniqueGUID());
            EditorUtility.SetDirty(autoGUID);
        }
    }

    private bool IsGUIDUnique(string guidToCheck)
    {
        AutoGUID[] allAutoGUIDs = FindObjectsOfType<AutoGUID>();

        foreach (AutoGUID autoGUID in allAutoGUIDs)
        {
            if (autoGUID != (AutoGUID)target && autoGUID.GUID == guidToCheck)
                return false;
        }

        return true;
    }

    private string GenerateUniqueGUID()
    {
        string newGUID;

        do
        {
            newGUID = System.Guid.NewGuid().ToString();
        }
        while (!IsGUIDUnique(newGUID));

        return newGUID;
    }
}