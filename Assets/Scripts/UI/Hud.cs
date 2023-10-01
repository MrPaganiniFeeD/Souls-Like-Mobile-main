using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Hud : MonoBehaviour
{
    [SerializeField] private Image _joystickArea;
    
    [Inject]
    private void Construct(IInputService inputService)
    {
        inputService.SetAimArea(GetComponentInChildren<AimArea>());    
    }
    public void ClosedPanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

}
