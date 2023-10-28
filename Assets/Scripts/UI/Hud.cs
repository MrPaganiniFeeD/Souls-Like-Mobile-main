using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Hud : MonoBehaviour
{
    private Canvas _canvas;

    [Inject]
    private void Construct(IInputService inputService, CanvasCamera canvasCamera)
    {
        inputService.SetRotationZone(GetComponentInChildren<AimArea>().GetComponent<RectTransform>());
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = canvasCamera.Camera;
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
