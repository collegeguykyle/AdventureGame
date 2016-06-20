using UnityEngine;
using System.Collections;


public class RoomChoice : MonoBehaviour {

    bool charsOnScreen = false;
    public Character.Hero hero = Character.Hero.None;

    // Use this for initialization
	void Start () {

        Vector2 blah = new Vector2(0, 0);
        Draw.line("test Line", blah, blah, true);
    }
	
	// Update is called once per frame
	void Update () {
        //Bring characters onto screen at start of room
        if (!charsOnScreen) charsOnScreen = Movement.enterRoom(3f);
        

	}
}
