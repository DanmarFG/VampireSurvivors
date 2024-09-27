using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Unit stats;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spritesToSwapBetween;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(Animation());
    }

    void Update()
    {
       
    }
    public void Spotted()
    {
        StopAllCoroutines();
        agent.speed = 0;
        spriteRenderer.sprite = spritesToSwapBetween[0];
        StartCoroutine(SetBackSpeed());
    }

    IEnumerator SetBackSpeed()
    {
        yield return new WaitForSeconds(0.1f);
        agent.speed = stats.speed;
        
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        while (true) 
        {
            Debug.Log("Swap");
            spriteRenderer.sprite = spritesToSwapBetween[0];
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = spritesToSwapBetween[1];
            yield return new WaitForSeconds(0.5f);
        }
    }
}
