using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Update()
    {
        agent.SetDestination(new Vector3(UnitManager.Instance.GetPlayerPosition().x, UnitManager.Instance.GetPlayerPosition().y, 0));
    }
}
