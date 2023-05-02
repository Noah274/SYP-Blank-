
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
