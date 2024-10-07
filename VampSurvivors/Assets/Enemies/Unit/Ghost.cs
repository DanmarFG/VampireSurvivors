using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Unit stats;
    [SerializeField] private Animator animator;

    private float acceleration;

    private void OnEnable()
    {
        acceleration = agent.acceleration;
        animator.SetBool("Spotted", false);
    }

    public void Spotted()
    {
        StopAllCoroutines();
        agent.SetDestination(transform.position);
        agent.speed = 0;
        agent.acceleration = 0;
        stats.canMove = false;
        animator.SetBool("Spotted", true);
        StartCoroutine(SetBackSpeed());
    }

    IEnumerator SetBackSpeed()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Running");
        agent.acceleration = acceleration;
        agent.speed = stats.speed;
        stats.canMove = true;
        animator.SetBool("Spotted", false);
    }
}
