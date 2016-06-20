using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public enum Facing { w, sw, s, nw, n, e, se, ne};
    public Facing facing = Facing.s;

    public enum Hero { Clansman, Knight, Templar, Squire, Assassin, Hunter, Scout, Thief, Wizard, Priest, Druid, Necro, Blacksmith, Alchemist, Tinker, Noble, None };
    public Hero hero;
    public int heroNumber;

    private string spriteName;
    private SpriteRenderer sprRend;


    // Use this for initialization
    void Start () {
        sprRend = gameObject.GetComponent<SpriteRenderer>();
        if (heroNumber == 5) heroNumber = 0;
        hero = Character_Select.group[heroNumber];
	}
	
	// Update is called once per frame
	void Update () {
        setSprite();
        spriteUpdate();
	}



    public static Facing setFace(Vector2 pos1, Vector2 pos2)
    {
        float angle = SignedAngleBetween(Vector2.up, pos2 - pos1, -Vector3.forward);
        if (angle < 0) { angle += 360f; }
        Facing face;
        if (angle > 337.5 || angle <= 22.5) face = Facing.n;
        else if (angle > 22.5 && angle <= 67.5) face = Facing.ne;
        else if (angle > 67.5 && angle <= 112.5) face = Facing.e;
        else if (angle > 112.5 && angle <= 157.5) face = Facing.se;
        else if (angle > 157.5 && angle <= 202.5) face = Facing.s;
        else if (angle > 202.5 && angle <= 247.5) face = Facing.sw;
        else if (angle > 247.5 && angle <= 292.5) face = Facing.w;
        else if (angle > 292.5 && angle <= 337.5) face = Facing.nw;
        else { face = Facing.n; Debug.Log("Facing Error Found"); }
        return face;
    }

    static float SignedAngleBetween(Vector2 a, Vector2 b, Vector3 n)
    {
        float angle = Vector2.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
        return angle * sign;
    }

    void spriteUpdate()
    {
        int num = (int)facing;
        bool flip = false;
        if (num == 5)  // e
        {
            flip = true;
            num = 0;
        }
        else if (num == 6) //se
        {
            flip = true;
            num = 1;
        }
        else if (num == 7) //ne
        {
            flip = true;
            num = 3;
        }

        if (flip) sprRend.flipX = true;
        else sprRend.flipX = false;
        Object[] sprites;
        sprites = Resources.LoadAll<Sprite>("Characters/" + spriteName);
        sprRend.sprite = (Sprite)sprites[num];
        //sprRend.sprite = Resources.Load<Sprite>("Characers/" + spriteName + num);
    }

    void setSprite()
    {
        switch (hero)
        {
            case Hero.Clansman: spriteName = "barbarian_spr"; break;
            case Hero.Knight: spriteName = "knight_spr"; break;
            //case Tamplar:
            case Hero.Squire: spriteName = "rookie_spr"; break;
            case Hero.Assassin: spriteName = "assassin_spr"; break;
            case Hero.Hunter: spriteName = "hunter_spr"; break;
            case Hero.Scout: spriteName = "scout_spr"; break;
            case Hero.Thief: spriteName = "thief_spr"; break;
            case Hero.Wizard: spriteName = "wizard_spr"; break;
            case Hero.Priest: spriteName = "priest_spr"; break;
            case Hero.Druid: spriteName = "druid_spr"; break;
            case Hero.Necro: spriteName = "necro_spr"; break;
            case Hero.Blacksmith: spriteName = "blacksmith_spr"; break;
            case Hero.Alchemist: spriteName = "chemist_spr"; break;
            case Hero.Tinker: spriteName = "tinker_spr"; break;
            case Hero.Noble: spriteName = "noble_spr1"; break;
        }

    }
}
