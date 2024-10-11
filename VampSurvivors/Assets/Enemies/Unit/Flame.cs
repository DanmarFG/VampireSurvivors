using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flame : MonoBehaviour, iSpotted
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        agent.isStopped = true;
        animator.SetBool("Spotted", false);
    }

    public void Spotted()
    {
        Color tmp = spriteRenderer.color;
        tmp.a = 1f;
        spriteRenderer.color = tmp;

        animator.SetBool("Spotted", true);

        agent.isStopped = false;
    }
}
