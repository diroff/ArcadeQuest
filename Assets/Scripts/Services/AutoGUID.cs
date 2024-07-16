using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

[ExecuteInEditMode]
public class AutoGUID : MonoBehaviour
{
    [SerializeField] private string _guid;

    public string GUID
    {
        get
        {
            if (string.IsNullOrEmpty(_guid))
            {
#if UNITY_EDITOR
                _guid = GenerateUniqueGUID();
#endif
            }
            return _guid;
        }
        private set { _guid = value; }
    }

    private void Awake()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(_guid))
        {
            _guid = GenerateUniqueGUID();
        }
#endif
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(_guid) || !IsGUIDUnique(_guid))
        {
            _guid = GenerateUniqueGUID();
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
        }
    }

    public void SetGUID(string GUID)
    {
        _guid = GUID;
    }

    private string GenerateUniqueGUID()
    {
        string newGUID;
        do
        {
            newGUID = System.Guid.NewGuid().ToString();
        } while (!IsGUIDUnique(newGUID));

        return newGUID;
    }

    private bool IsGUIDUnique(string guidToCheck)
    {
        AutoGUID[] allAutoGUIDs = FindObjectsOfType<AutoGUID>();
        foreach (AutoGUID autoGUID in allAutoGUIDs)
        {
            if (autoGUID != this && autoGUID.GUID == guidToCheck)
            {
                return false;
            }
        }
        return true;
    }
#endif
}
