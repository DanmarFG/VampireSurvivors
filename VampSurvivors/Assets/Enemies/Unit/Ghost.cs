using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Unit stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
       
    }
    public void Spotted()
    {
        StopAllCoroutines();
        agent.speed = 0; 
        StartCoroutine(SetBackSpeed());
    }

    IEnumerator SetBackSpeed()
    {
        yield return new WaitForSeconds(0.1f);
        agent.speed = stats.speed;
    }
}
