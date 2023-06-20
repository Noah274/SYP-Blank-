using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float distanceBetween;
    public float hitPoints;
    public float damage;
    public List<GameObject> spawnPool;
    public GameObject projectilePrefab;
    public float attackCooldown;
    public float projectileSpeed;
    public bool isArcher;

    private bool canAttack = true;
    private float distance;
    private Vector3 position;

    private void Start()
    {
        player = GameObject.Find("Player");
        position = transform.position;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
           // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        if (isArcher)
        {
            if (canAttack)
            {
                LaunchProjectile();
                StartCoroutine(AttackCooldown());
            }
        }
    }

    public void SpawnObjects()
    {
        int randomItem = Random.Range(0, spawnPool.Count);
        GameObject toSpawn = spawnPool[randomItem];


        GameObject obj = Instantiate(toSpawn, transform.position, Quaternion.identity);
        GameObject createdObjectsContainer = GameObject.Find("createdObjects");
        obj.transform.SetParent(createdObjectsContainer.transform);
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

    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        GameObject createdObjectsContainer = GameObject.Find("createdObjects");
        projectile.transform.SetParent(createdObjectsContainer.transform);
        
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            projectileRb.velocity = direction * projectileSpeed;
        }
    }


    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
