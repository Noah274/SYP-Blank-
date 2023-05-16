/*
public class Player
{
    public enum Skill
    {
        NAN,
        Fireball, 
        Firewall,
        Meteor
    }
    
    private int _health;
    private Skill[] _skill {get, set};
    Skill.Skill[] _skill = new Skill.Skill[3]{Skill.NAN,Skill.NAN,Skill.NAN};

}
*/

using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public float speed;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(-10);
        }
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX *speed, inputY *speed, 0);


        movement *= Time.deltaTime;

        transform.Translate(movement);
    }
    
    void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        healthBar.setHealth(currentHealth);
    }
}