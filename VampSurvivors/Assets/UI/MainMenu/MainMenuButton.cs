using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.Instance.StartGame);
    }
}
