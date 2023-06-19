using System;
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

    public float maxHealth;
    private float currentHealth;
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
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("spawnPointPlayer");
        
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogError("Spawn point for player not found!");
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            SceneManager.LoadScene("SkillTree");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(-10);
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0 && (currentHealth - damage) >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (damage > 0 && (currentHealth - damage) <= 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene("EndScreen");
        }
        else
        {
            currentHealth -= damage;
        }

        healthBar.setHealth(currentHealth);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("TeleportDoor"))
        {
            Quaternion rotation = collider.transform.rotation;
            //Debug.Log("rotation: " + rotation.eulerAngles.z);
            //Debug.Log("TeleportDoor - Rotation: " + rotation.eulerAngles);
            
            RoomLogic logic = FindObjectOfType<RoomLogic>();
            if (logic != null)
            {
                logic.TeleportToNextRoom(rotation);
            }
            else
            {
                Debug.LogError("Error: Main script not found!");
            }
        }
    }

}