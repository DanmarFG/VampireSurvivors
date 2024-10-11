using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

[System.Serializable]
public struct PlayerStats
{
    public float speed, health, maxHealth, maxFov;

    public float fireballShootDelay, fireballDamage, fireballSpeed;

    public float punchDamage, punchReach;
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private float health, maxHealth; 
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private bool canMove = true, canTakeDamage = true;

    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject InterractText;
    bool canInterract = false;

    [SerializeField] TileBase chestTile, closedLadderTile, ladderTile;
    
    [Header("Fireball")]
    [SerializeField] private float fireballShootDelay = 1f;
    [SerializeField] private float fireBallSpeed = 200f;
    [SerializeField] private float fireBallDamage = 200f;
    
    [Header("Active Sword Attack")]
    [SerializeField] private Punch punch;
    [SerializeField] private float attackDowntime = 2f;

    [Header("Animator")]
    [SerializeField] private Animator playerAnimator;
    

    private Rigidbody2D _rigidbody;

    private Vector2 _inputVector;

    private Vector3Int ladderPosition;

    Tilemap obstacleMap;

    private IEnumerator Start()
    {
        health = maxHealth;
        
        _rigidbody = GetComponent<Rigidbody2D>();

        if (UnitManager.Instance)
        {
            StartCoroutine(UnitManager.Instance.AssignPlayer(this));
        }
            
        healthSlider.value = health;
        healthSlider.maxValue = maxHealth;

        yield return null;

        obstacleMap = GameObject.Find("Obstacles").GetComponent<Tilemap>();

        EventManager.Instance.OnFinishedLadderEvent += ReplaceHatchWithLadder;

        //StartCoroutine(ShootFireball());
    }

    private void Update()
    {
        _rigidbody.velocity = _inputVector * playerSpeed;

        
        
    }

    private void OnMove(InputValue value)
    {
        if (!canMove)
        {
            _inputVector = new Vector2();
            return;
        }

        _inputVector = value.Get<Vector2>();

        if (_inputVector == Vector2.zero)
            playerAnimator.SetBool("Moving", false);
        else
            playerAnimator.SetBool("Moving", true);


    }

    private void OnPunch()
    {
        punch.Attack(playerAim.GetAimDirection());
    }

    private void OnPause()
    {
        if(!GameManager.Instance.isInLevelUp)
            EventManager.Instance.PauseGame();
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        if(!canTakeDamage)
            return;
        
        health -= damage;
        EventManager.Instance.PlayerTookDamage(damage);
        healthSlider.value = health;
        PlayerDeath();
        StartCoroutine(DamageCoolDown());
    }

    IEnumerator DamageCoolDown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    void PlayerDeath()
    {
        if (!(health <= 0)) return;
        
        StopAllCoroutines();
        EventManager.Instance.PlayerDied();
        Destroy(gameObject);
    }

    private IEnumerator SwordStrike()
    {
        
        yield return new WaitForSeconds(attackDowntime);
    }

    private IEnumerator ShootFireball()
    {
        while (true)
        {
            var shot = ProjectileBag.Instance.FindProjectile(Managers.ProjectileType.Fireball);
            var projectile = shot.GetComponent<Projectile>();
            
            projectile.SetSpeed(fireBallSpeed);
            projectile.SetDamage(fireBallDamage);
            projectile.ShootProjectile(playerAim.GetAimDirection(), transform.position);

            yield return new WaitForSeconds(fireballShootDelay);
        }
    }

    public void AllowInterraction(bool allowed)
    {
        InterractText.SetActive(allowed);

        canInterract = allowed;
    }

    private void OnInterract()
    {
        if (!canInterract)
            return;       

        int x = (int)Mathf.Floor(transform.position.x);
        int y = (int)Mathf.Floor(transform.position.y);

        bool shouldBreak = false;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector3Int pos = new Vector3Int((int)Mathf.Floor(transform.position.x)+ i, (int)Mathf.Floor(transform.position.y) + j, 0);
                TileBase tile = obstacleMap.GetTile(pos);

                if (tile == chestTile)
                {
                    if (GameManager.Instance.currentCoinCount < GameManager.Instance.currentChestCount)
                        return;

                    obstacleMap.SetTile(pos, null);
                    GameManager.Instance.ChangeState(new STGetUppgradePlayer());
                    GameManager.Instance.currentCoinCount -= GameManager.Instance.currentChestCount;
                    shouldBreak = true;
                    break;
                }

                if(tile == closedLadderTile)
                {
                    EventManager.Instance.StartLadderEvent();

                    Debug.Log("Sent event");

                    ladderPosition = pos;
                    canInterract = false;
                }

                if (tile == ladderTile)
                {
                    EventManager.Instance.StageComplete();
                    GameManager.Instance.ChangeState(new STLoadScene(2));
                }
            }
            if (shouldBreak)
                break;
        }

        canInterract = false;
    }

    private void ReplaceHatchWithLadder()
    {
        EventManager.Instance.OnFinishedLadderEvent -= ReplaceHatchWithLadder;

        obstacleMap.SetTile(ladderPosition, ladderTile);
    }

    public void LevelUp()
    {
        
        maxHealth += maxHealth * 0.1f;
        healthSlider.maxValue = maxHealth;
        fireBallDamage += fireBallDamage * 0.1f;
        punch.damage += punch.damage * 0.1f;
    }

    public void AddStats(PlayerStats statsToAdd)
    {
        maxHealth += statsToAdd.maxHealth;
        health += statsToAdd.health;
        playerSpeed += statsToAdd.speed;
        fireBallDamage += statsToAdd.fireballDamage;

        if(GetComponentInChildren<FieldOfView>().viewAngle < 180)
        {
            GetComponentInChildren<FieldOfView>().viewAngle += statsToAdd.maxFov;
            GetComponentInChildren<Light>().spotAngle += statsToAdd.maxFov;
        }
        fireballShootDelay += statsToAdd.fireballShootDelay;
        fireBallSpeed += statsToAdd.fireballSpeed;
        punch.damage += statsToAdd.punchDamage;
    }
}
