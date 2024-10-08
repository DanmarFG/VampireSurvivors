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
        if (PlayerPrefs.GetInt("Gif") == 1)
            PlayerPrefs.SetInt("Gif", 0);
        else
        {
            PlayerPrefs.SetInt("Gif", 1);
        }
    }   
}
