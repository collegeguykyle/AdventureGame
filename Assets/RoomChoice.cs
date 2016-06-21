using UnityEngine;
using System.Collections;


public class RoomChoice : MonoBehaviour {

    bool charsOnScreen = false;


	void Start () {

        Vector2 blah = new Vector2(0, 0);  //testing
        Draw.line("test Line", blah, blah, true); //testing
    }
	

	void Update () {
        //Bring characters onto screen at start of room
        if (!charsOnScreen) charsOnScreen = Movement.enterRoom(3f);
        
	}


}
