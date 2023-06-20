using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("Basic Info")]
    private GameObject player;
    public float hitPoints;
    public float speed;
    public float distanceBetween;
    public List<GameObject> spawnPool;

    [Header("AttackInfo")]
    public float damage;
    public GameObject projectilePrefab;
    public GameObject slowProjectilePrefab;
    public float attackCooldown;
    public float projectileSpeed;
    public float slowProjectileSpeed;
    public float slowProjectileDuration;
    public float slowProjectileCooldown;
    public bool isArcher;

    private bool canAttack = true;
    private bool canLaunchSlowProjectile = true;
    private bool isFrozen = true;
    private float distance;
    private Vector3 position;

    private void Start()
    {
        player = GameObject.Find("Player");
        position = transform.position;
    }

    void Update()
    {
        if (!isFrozen)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distance < distanceBetween)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }

            if (isArcher)
            {
                if (canAttack)
                {
                    LaunchProjectile();
                    StartCoroutine(AttackCooldown());
                }

                if (canLaunchSlowProjectile)
                {
                    LaunchSlowProjectile();
                    StartCoroutine(SlowProjectileCooldown());
                }
            }
        }
    }

    public void SpawnObjects()
    {
        int randomItem = Random.Range(0, spawnPool.Count);
        GameObject toSpawn = spawnPool[randomItem];

        Instantiate(toSpawn, transform.position, Quaternion.identity);
    }

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            Player playerHealth = c2d.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        if (c2d.CompareTag("Bullet"))
        {
            hitPoints -= 10;
            if (hitPoints <= 0)
            {
                SpawnObjects();
                Destroy(gameObject);
            }
        }
    }

    private Coroutine projectileCooldownCoroutine;

    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        GameObject createdObjectsContainer = GameObject.Find("createdObjects");
        projectile.transform.SetParent(createdObjectsContainer.transform);
        if (projectileRb != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            projectileRb.velocity = direction * projectileSpeed;

            projectileCooldownCoroutine = StartCoroutine(ProjectileCooldown());
        }
    }

    IEnumerator ProjectileCooldown()
    {
        yield return new WaitForSeconds(3f);
        // Disable the projectile
        if (projectileCooldownCoroutine != null)
        {
            StopCoroutine(projectileCooldownCoroutine);
            projectileCooldownCoroutine = null;
        }

        yield return new WaitForSeconds(2f);
        // Resume the projectile
        LaunchProjectile();
    }
    void LaunchSlowProjectile()
    {
        GameObject slowProjectile = Instantiate(slowProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D slowProjectileRb = slowProjectile.GetComponent<Rigidbody2D>();GameObject createdObjectsContainer = GameObject.Find("createdObjects");
        slowProjectile.transform.SetParent(createdObjectsContainer.transform);
        if (slowProjectileRb != null)
        {
            StartCoroutine(FollowPlayerForDuration(slowProjectileRb, slowProjectileSpeed, 2f));
        }
    }

    IEnumerator FollowPlayerForDuration(Rigidbody2D rb, float initialSpeed, float duration)
    {
        Vector2 initialDirection = (player.transform.position - transform.position).normalized;
        float timer = 0f;

        while (timer < duration)
        {
            // Check if the slow projectile's rigidbody still exists
            if (rb == null)
                yield break;

            // Adjust velocity to track the player
            Vector2 updatedDirection = ((Vector2)player.transform.position - rb.position).normalized;

            rb.velocity = updatedDirection * initialSpeed;

            timer += Time.deltaTime;
            yield return null;
        }

        // Disable the slow projectile
        if (rb != null)
        {
            Destroy(rb.gameObject);
        }
    }

    
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator SlowProjectileCooldown()
    {
        canLaunchSlowProjectile = false;
        yield return new WaitForSeconds(slowProjectileCooldown);
        canLaunchSlowProjectile = true;
    }

    public void StartAttack()
    {
        isFrozen = false;
    }
}
