using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float damageAmount = 20f;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Wall" )){
            Destroy(gameObject);
        }
        if (c2d.CompareTag("Player"))
        {
            Player player = c2d.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
    }
}