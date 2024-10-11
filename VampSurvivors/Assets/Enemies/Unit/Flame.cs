using Managers;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.AI;

public class Flame : MonoBehaviour, iSpotted
{
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    private Unit stats;
    private Animator animator;

    Color col;

    [SerializeField]
    bool isSpotted = false;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats = GetComponent<Unit>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isSpotted = false;
        
        animator.SetBool("Spotted", false);
        col = spriteRenderer.color;

        if(gameObject.activeSelf)
            agent.isStopped = true;
    }

    private void OnDisable()
    {        
        isSpotted = false;
        animator.SetBool("Spotted", false);
        spriteRenderer.color = col;
    }

    private void Update()
    {
        if(isSpotted)
            agent.SetDestination(new Vector3(UnitManager.Instance.GetPlayerPosition().x, UnitManager.Instance.GetPlayerPosition().y, 0));
    }

    public void Spotted()
    {
        if (Vector2.Distance(transform.position, UnitManager.Instance.player.transform.position) > 5)
        {
            return;
        }

        Color tmp = col;
        tmp.a = 1f;
        spriteRenderer.color = tmp;

        animator.SetBool("Spotted", true);

        isSpotted = true;
    }
}
