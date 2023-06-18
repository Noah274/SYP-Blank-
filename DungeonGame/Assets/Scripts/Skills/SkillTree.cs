using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescription;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;
    public int SkillPoint;

    private void Start()
    {
        

        SkillLevels = new int[6];
        SkillCaps = new[] { 1, 5, 1, 5, 1, 1 };

        SkillNames = new[] { "Fire Element", "More Hp", "Fire Ball", "More Damage", "Wall of Fire", "Meteor strike", };
        SkillDescription = new[]
        {
            "Don't burn your self",
            "So you don't die",
            "A flaming ball of fire",
            "You are a strong Boy",
            "That's strait out of  Valorant",
            "Who doesn't like explosions",
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>())
        {
            SkillList.Add(skill);
        }
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>())
        {
            ConnectorList.Add(connector.gameObject);
        }

        for (var i = 0; i < SkillList.Count; i++)
        {
            SkillList[i].id = i;
        }

        SkillList[0].ConnectedSkills = new[] { 1, 2, 3 };
        SkillList[2].ConnectedSkills = new[] { 4, 5};
        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList)
        {
            skill.UpdateUI();
        }
        
    }
}
