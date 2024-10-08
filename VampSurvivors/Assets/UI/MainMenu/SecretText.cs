using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretText : MonoBehaviour
{
    public GameObject text;
    void Start()
    {
        if (PlayerPrefs.GetInt("Gif") == 1)
        {
            text.SetActive(true);
        }
        else
        {
            text.SetActive(false);
        }
    }

    public void ToggleText()
    {
        if(PlayerPrefs.GetInt("Gif") == 1)
            text.SetActive(true);
        else
        {
            text.SetActive(false);
        }
    }
}
