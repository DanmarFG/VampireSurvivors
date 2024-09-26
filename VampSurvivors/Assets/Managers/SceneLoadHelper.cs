using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHelper : MonoBehaviour
{
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(2));
        GameObject.Find("DancingCat").SetActive(PlayerPrefs.GetInt("Gif") == 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
