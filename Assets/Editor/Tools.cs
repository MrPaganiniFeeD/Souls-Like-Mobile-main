using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEditor;
using UnityEngine;

public class Toolss
{
    [MenuItem("Tools/Clear prefs")]
    public static void CleatPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
