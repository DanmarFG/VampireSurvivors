using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Gif", 0);
    }

    public void ToggleGif()
    {
        PlayerPrefs.SetInt("Gif", PlayerPrefs.GetInt("Gif") == 0 ? 1 : 0);
    }   
}
