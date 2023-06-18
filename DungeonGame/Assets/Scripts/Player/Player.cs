using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    enum SkillDef
    {
        Non,
        Fireball,
        FireWall,
        Meteor
    };

    public int maxHealth;
    private int currentHealth;
    public HealthBar healthBar;
    private SkillDef[] skill;

    public void SetPrimarySkill(int skillNum)
    {
        if (skillNum == 2)
        {
            skill[0] = SkillDef.Fireball;
        }
    }

    void Start()
    {
        skill = new SkillDef[3] { SkillDef.Non, SkillDef.Non, SkillDef.Non };
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void MovePlayer()
    {
        Debug.Log("test");
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("spawnPointPlayer");
        Debug.Log(spawnPoints.Length);
        GameObject playerSpawnPoint = null;
        
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (spawnPoint.CompareTag("spawnPointPlayer"))
            {
                playerSpawnPoint = spawnPoint;
                break;
            }
        }
        
        if (playerSpawnPoint != null)
        {
            transform.position = playerSpawnPoint.transform.position;
        }
        Debug.Log("test 2");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            SceneManager.LoadScene("SkillTree");
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("OptionMenu");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(-10);
        }
    }

    void TakeDamage(int damage)
    {
        if (damage < 0 && (currentHealth - damage) >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (damage > 0 && (currentHealth - damage) <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }

        healthBar.setHealth(currentHealth);
    }
}