using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = transform.position- t.transform.position;

        float angle = Vector2.SignedAngle(dir, Vector2.right) + 180;

        angle += 45;
        angle %= 360;


        if(angle is >= 0 and < 90)
            Debug.Log("Right");
        else if (angle is >= 90 and < 180)
            Debug.Log("down");
        else if (angle is >= 180 and < 270)
            Debug.Log("left");
        else
            Debug.Log("up");


        Debug.Log(angle);
    }
}
