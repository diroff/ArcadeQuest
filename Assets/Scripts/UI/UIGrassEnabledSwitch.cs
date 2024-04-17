using System.Collections.Generic;
using UnityEngine;

public class UIGrassEnabledSwitch : MonoBehaviour
{
    [SerializeField] private List<GameObject> _grass;

    private bool _isGrassActive = true;

    public void ChangeGrassState()
    {
        if (_isGrassActive)
            DisableGrass();
        else
            EnableGrass();
    }

    private void DisableGrass()
    {
        foreach (var grass in _grass)
        {
            grass.SetActive(false);
        }

        _isGrassActive = false;
    }

    private void EnableGrass()
    {
        foreach (var grass in _grass)
        {
            grass.SetActive(true);
        }

        _isGrassActive = true;
    }
}