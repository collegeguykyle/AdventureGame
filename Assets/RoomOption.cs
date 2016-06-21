using UnityEngine;
using System.Collections;

public class RoomOption : MonoBehaviour {

    public enum Type { explore, search, task, group, camp, none}
    public Type type = Type.none;
    public GameObject GameControl;
    public GameObject who;


    void Start()
    {
        GameControl = GameObject.Find("EventSystem");
    }

    void Update ()
    {
        if (who) Movement.moveUnit(who, gameObject.transform.position, 2f);
    }

	public void onclick()
    {
        Debug.Log("blaaaah");
        who = GameControl.GetComponent<Unit_Select>().selected;
        print(who);
    }

}
