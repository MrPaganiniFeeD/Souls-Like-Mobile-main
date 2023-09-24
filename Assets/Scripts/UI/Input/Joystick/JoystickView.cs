using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JoystickView : MonoBehaviour
{
    [SerializeField] private Color _inActiveJoystickColor;
    [SerializeField] private Color _activeJoystickColor;

    [FormerlySerializedAs("_joystick")] [SerializeField] private JoystickOld joystickOld;

    private bool _joystickIsActive = false;

    private void OnEnable()
    {
        if(joystickOld != null)
            joystickOld.ClickChanged += OnClickChanged;
    }
    private void OnDisable()
    {
        if(joystickOld != null)
            joystickOld.ClickChanged -= OnClickChanged;
    }
    private void OnClickChanged(Image joystick)
    {
        if (!_joystickIsActive)
        {
            joystick.color = _inActiveJoystickColor;
            _joystickIsActive = true;
        }
        else
        {
            joystick.color = _activeJoystickColor;
            _joystickIsActive = false;
        }
    }
}
