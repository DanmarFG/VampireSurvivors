using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spritesToSwapBetween;

    private void OnEnable()
    {
        StartCoroutine(Animation());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
