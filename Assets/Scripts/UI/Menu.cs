using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void ClosedPanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

}
