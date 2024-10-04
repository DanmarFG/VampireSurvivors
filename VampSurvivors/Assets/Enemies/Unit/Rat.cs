using System.Collections;
using Managers;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);


            Vector2 dir = (Vector2)transform.position - UnitManager.Instance.GetPlayerPosition();

            float angle = Vector2.SignedAngle(dir, Vector2.right) + 180;

            angle += 45;
            angle %= 360;

            animator.SetFloat("Angle", angle);
        }
    }
}
