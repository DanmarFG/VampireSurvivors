using System.Collections;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperienceOrb : MonoBehaviour
{
    private Vector3 _targetPosition;

    [SerializeField]
    private float baseSpeed;
    
    private float _speed;

    public float expAmmount = 1f;

    [SerializeField]
    private bool gotoPlayer = false;
    
    [SerializeField]
    private Collider2D colliders;
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        
        if (gotoPlayer)
            _targetPosition = UnitManager.Instance.GetPlayerPosition();
            
    }
    
    private void OnEnable()
    {
        _speed = baseSpeed;
        gotoPlayer = false;
    }

    public void Spawn(Vector3 position)
    {
        var x = Random.Range(-1.0f, 1.0f);
        var y = Random.Range(-1.0f, 1.0f);
        
        _targetPosition = new Vector3(position.x + x, position.y + y, 0);
        
        transform.position = position;
        
        gameObject.SetActive(true);

        StartCoroutine(toggleColliders());
    }

    public IEnumerator toggleColliders()
    {
        colliders.enabled = false;
        yield return new WaitForSeconds(0.5f);
        colliders.enabled = true;
    }

    public void GoToPlayer()
    {
        _speed += 20.0f;
        gotoPlayer = true;
    }

    private void CollectExperience()
    {
        EventManager.Instance.AddExperience(expAmmount);
        EventManager.Instance.CoinCollected();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponentInParent<Player>()) return;

        StopAllCoroutines();
        CollectExperience();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _targetPosition);
    }
}
