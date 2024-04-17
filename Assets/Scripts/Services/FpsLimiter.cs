using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    [SerializeField] private int _fpsCount;

    public static FpsLimiter Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetTargetFps();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetTargetFps()
    {
        Application.targetFrameRate = _fpsCount;
        Debug.Log("FPS limit set to " + _fpsCount);
    }
}