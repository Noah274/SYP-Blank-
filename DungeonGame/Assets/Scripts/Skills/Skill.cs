using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;
    Player player;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"    {skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}    \n{skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescription[id]}";

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow
            : skillTree.SkillPoint > 0 ? Color.green : Color.white;
        foreach (var connectedSkill in ConnectedSkills)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id] > 0);

        }
    }

    public void Buy()
    {
        if (skillTree.SkillPoint < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id])
        {
            return;
        }
        /*if (id == 2)
        {
            player.SetPrimarySkill(2);
        }*/
        skillTree.SkillPoint -= 1;
        skillTree.SkillLevels[id]++;
        skillTree.UpdateAllSkillUI();
    }
}
