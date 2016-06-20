using UnityEngine;
using System.Collections;

public class animate : MonoBehaviour {

    public bool start_active = false;
    public GameObject animationn;
    private AnimationClip clip;

	// Use this for initialization
	void Start () {
        if (start_active) animationn.SetActive(true);
        else animationn.SetActive(false);
        clip = animationn.GetComponent<AnimationClip>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void start_animation ()
    {
        animationn.SetActive(true);
    }

    public void start_combat ()
    {

    }

}
