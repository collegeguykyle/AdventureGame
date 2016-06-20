using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventChoice : MonoBehaviour {

    public Character.Hero hero = Character.Hero.None;
    public int heroNumber = 0;
    public int spriteNum;
    public GameObject parent;

    // Use this for initialization
    void Start () {
	    if (heroNumber > 0 && Character_Select.group.Count > 0) hero = Character_Select.group[heroNumber];
        setSprite();
    }


    public void onclick()
    {
        parent.GetComponent<EventChoice>().heroNumber = heroNumber;
    }

    void setSprite()
    {
        Object[] sprites;
        sprites = Resources.LoadAll<Sprite>("Characters/FFTPorts");
        switch (hero)
        {
            case Character.Hero.Clansman: spriteNum = 73; break;
            case Character.Hero.Knight: spriteNum = 47; break;
            case Character.Hero.Templar: spriteNum = 19; break;
            case Character.Hero.Squire: spriteNum = 23; break;
            case Character.Hero.Assassin: spriteNum = 22; break;
            case Character.Hero.Hunter: spriteNum = 12; break;
            case Character.Hero.Scout: spriteNum = 86; break;
            case Character.Hero.Thief: spriteNum = 24; break;
            case Character.Hero.Wizard: spriteNum = 11; break;
            case Character.Hero.Priest: spriteNum = 87; break;
            case Character.Hero.Druid: spriteNum = 10; break;
            case Character.Hero.Necro: spriteNum = 98; break;
            case Character.Hero.Blacksmith: spriteNum = 59; break;
            case Character.Hero.Alchemist: spriteNum = 36; break;
            case Character.Hero.Tinker: spriteNum = 71; break;
            case Character.Hero.Noble: spriteNum = 65; break;
        }
        gameObject.GetComponent<Image>().sprite = (Sprite)sprites[spriteNum];
    }
}
