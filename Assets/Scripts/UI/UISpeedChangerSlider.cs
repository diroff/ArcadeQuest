using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISpeedChangerSlider : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _speedText;

    private void Start()
    {
        ChangePlayerSpeed(_player.MovementSpeed);
    }

    public void ChangePlayerSpeed(float value)
    {
        _player.SetSpeed(value);
        _speedText.text = "Speed: " + _player.MovementSpeed;
    }
}