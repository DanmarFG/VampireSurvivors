using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour, iSpotted
{
    private NavMeshAgent agent;
    private Unit stats;
    private Animator animator;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats = GetComponent<Unit>();

        animator.SetBool("Spotted", false);

        if (gameObject.activeSelf)
            agent.isStopped = false;
    }

    private void OnDisable()
    {
        animator.SetBool("Spotted", false);
    }

    private void Update()
    {
        agent.SetDestination(new Vector3(UnitManager.Instance.GetPlayerPosition().x, UnitManager.Instance.GetPlayerPosition().y, 0));
    }

    public void Spotted()
    {
        StopAllCoroutines();
        agent.isStopped = true;
        StartCoroutine(ResetSpotted());
        animator.SetBool("Spotted", true);
    }

    public IEnumerator ResetSpotted()
    {
        yield return new WaitForSeconds(0.1f);
        agent.isStopped = false;
        animator.SetBool("Spotted", false);
    }
}
