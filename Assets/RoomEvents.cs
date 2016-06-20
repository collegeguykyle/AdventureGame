using UnityEngine;
using System.Collections;

public class RoomEvents : MonoBehaviour {

    public GameObject parent;
    public int numberOfEvents = 1;
    public int activeEvent = 0;

	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1) && activeEvent != 99 && parent == null) activeEvent = 0;

    }

    public void onClick()
    {
        parent.GetComponent<RoomEvents>().activeEvent = activeEvent;
    }
}
