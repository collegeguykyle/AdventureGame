using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character_Select : MonoBehaviour {

    //public static List<string> group = new List<string>();
    public static List<Character.Hero> group = new List<Character.Hero>();
    public bool inGroup = false;

    public static int partySize = 0;
    public static int partyMax = 5;
    public Text groupSize;
    private string Name;

    public Character.Hero hero;

    public int characterSlot = 99;

    // Use this for initialization
    void Start () {
        AssignCharSlots();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void onClick()
    {
        if (group.Contains(hero)) inGroup = true;
        else inGroup = false;

        if (inGroup == false && partySize < 5)
        {
            group.Add(hero);
            partySize++;
            gameObject.GetComponent<Image>().color = Color.green;
            Debug.Log("Select: " + gameObject.name);
        }

        if (inGroup == true)
        {
            group.Remove(hero);
            partySize--;
            gameObject.GetComponent<Image>().color = Color.white;
            Debug.Log("Deselect: " + gameObject.name);
        }

        groupSize.text = partySize + " / " + partyMax;
    }
    
    public void startGame()
    {
        if (partySize == 5) SceneManager.LoadScene ("Explore Test");
    }

    public void AssignCharSlots()
    {
        if (characterSlot == 99) groupSize = GameObject.Find("Group Size").GetComponent<Text>();
    }

}

