using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFXTimer : MonoBehaviour
{
    public float upTime;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(upTime);
        Destroy(gameObject);
    }

    
}
