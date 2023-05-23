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
    }
    
    void TakeDamage(int damage)
    {
        if (damage < 0 && (currentHealth-damage) >= maxHealth){
            currentHealth = maxHealth;
        }
        else if (damage > 0 && (currentHealth - damage) <= 0){
            currentHealth = 0;
        }
        else{
            currentHealth -= damage;
        }
        healthBar.setHealth(currentHealth);
    }
}