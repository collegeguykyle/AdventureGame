using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class MysteryTower : MonoBehaviour {

    public GameObject[] Heroes;
    public GameObject[] MoveNodes;
    public GameObject[] TowerNodes;

    public float speed = 1.5f;
    public float duration = 1.5f;
    private float startTime;
    private float elapsed = 0.0f;
    private float size;
    private float camY;
    private float camX;

    public GameObject heroChoice;
    public GameObject eventChoice;

    private List<int> towerHeroes = new List<int>();
    private List<int> mushroomHeroes = new List<int>();
    private List<int> forestHeroes = new List<int>();
    private List<int> riverHeroes = new List<int>();

    public enum State { enter, tower, mushroom, forest, river, normal }
    public State state = State.enter;
    private State curState = State.enter;

    // Update is called once per frame
    void Update() {
        setState();
        
    }

    #region Room Choices

    void enterRoom()
    {
        for (int i = 0; i < 5; i++)
        {
            Heroes[i].transform.position = Vector2.MoveTowards(Heroes[i].transform.position, MoveNodes[i].transform.position, Time.deltaTime * speed);
            Heroes[i].GetComponent<Character>().facing = Character.setFace(Heroes[i].transform.position, MoveNodes[i].transform.position);
            //if (Vector2.Distance(Heroes[i].transform.position, MoveNodes[i].transform.position) > 0.01f) state = State.enter;
        }

    }

    void Tower()
    {
        cameraTo(3, new Vector2(-0.77f, 1.8f));
        //show text description and hero choices
        assignHero(towerHeroes, 5);
        for (int i=0; i<towerHeroes.Count; i++)
        {
            unitTo(Heroes[towerHeroes[i]], MoveNodes[7+i].transform.position, 3.0f);
        }
    }

    void Mushroom()
    {
        cameraTo(2, new Vector2(1.9f, -1.25f));
        //show text description and hero choices
        assignHero(mushroomHeroes, 2);
        for (int i = 0; i < mushroomHeroes.Count; i++)
        {
            unitTo(Heroes[mushroomHeroes[i]], MoveNodes[5 + i].transform.position, 3.0f);
        }
    }

    void Forest()
    {
        cameraTo(2.45f, new Vector2(-5.16f, 2.45f));
        //show text description and hero choices
        assignHero(forestHeroes, 2);
        for (int i = 0; i < forestHeroes.Count; i++)
        {
            unitTo(Heroes[forestHeroes[i]], MoveNodes[12 + i].transform.position, 3.0f);
        }
    }

    void River()
    {
        cameraTo(2.24f, new Vector2(5.5f, 1.23f));
        //show text description and hero choices
        assignHero(riverHeroes, 2);
        for (int i = 0; i < riverHeroes.Count; i++)
        {
            unitTo(Heroes[riverHeroes[i]], MoveNodes[14 + i].transform.position, 3.0f);
        }
    }

    void normal()
    {
        cameraTo(5, new Vector2(0f, 0f));
    } 
    #endregion

    void reset()
    {
        startTime = Time.time;
        size = Camera.main.orthographicSize;
        curState = state;
        camX = Camera.main.transform.position.x;
        camY = Camera.main.transform.position.y;
    }

    void cameraTo(float camSize, Vector2 camCenter)
    {
        elapsed = (Time.time - startTime) / duration;

        Camera.main.orthographicSize = Mathf.SmoothStep(size, camSize, elapsed);

        Vector3 v3 = new Vector3();
        v3.z = -10;
        v3.x = Mathf.SmoothStep(camX, camCenter.x, elapsed);
        v3.y = Mathf.SmoothStep(camY, camCenter.y, elapsed);
        Camera.main.transform.position = v3;
    }

    void unitTo(GameObject hero, Vector2 location, float speedy)
    {
        hero.transform.position = Vector2.MoveTowards(hero.transform.position, location, Time.deltaTime * speedy);
        hero.GetComponent<Character>().facing = Character.setFace(hero.transform.position, location);
    }

    void setState()
    {
        int activeEvent = eventChoice.GetComponent<RoomEvents>().activeEvent;
        switch (activeEvent)
        {
            case 0: state = State.normal; break;
            case 1: state = State.tower; break;
            case 2: state = State.forest; break;
            case 3: state = State.river; break;
            case 4: state = State.mushroom; break;
            case 99: state = State.enter; break;
        }
        if (state != curState) reset();
        switch (state)
        {
            case State.enter: enterRoom(); break;
            case State.tower: Tower(); break;
            case State.mushroom: Mushroom(); break;
            case State.forest: Forest(); break;
            case State.river: River(); break;
            case State.normal: normal(); break;
        }
    }

    void assignHero(List<int> list, int maxAssign)
    {
        int hero = heroChoice.GetComponent<EventChoice>().heroNumber;
        if (hero != 0)
        {
            if (list.Contains(hero))
            {
                list.Remove(hero);
                return;
            }
            else if (list.Count < maxAssign)
            {
                freeHero(hero);
                list.Add(hero);
            }
            heroChoice.GetComponent<EventChoice>().heroNumber = 0;
        }
    }

    void freeHero(int hero)
    {
        if (towerHeroes.Contains(hero)) towerHeroes.Remove(hero);
        if (mushroomHeroes.Contains(hero)) mushroomHeroes.Remove(hero);
        if (forestHeroes.Contains(hero)) forestHeroes.Remove(hero);
        if (riverHeroes.Contains(hero)) riverHeroes.Remove(hero);
    }


}
