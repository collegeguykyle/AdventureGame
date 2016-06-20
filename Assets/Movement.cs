using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;

public static class Movement {

    public static void pathToPoint(GameObject who, Vector2 point)
    {
        PolyNavAgent agent = who.GetComponentInChildren<PolyNavAgent>();
        agent.SetDestination(point);
    }
    public static void pathToPoint(Transform who, Vector2 point)
    {
        PolyNavAgent agent = who.GetComponentInChildren<PolyNavAgent>();
        agent.SetDestination(point);
    }
    

    public static void drawPath(GameObject who)
    {
        PolyNavAgent agent = who.GetComponentInChildren<PolyNavAgent>();
        List<Vector2> path = agent.activePath;
        Unit unit = who.GetComponentInChildren<Unit>();
        //unit.lineList = path;
        //unit.movepathline.active = true;
    }
    public static void drawPath(Transform who)
    {

    }


    public static void setMoveCollider(GameObject who, bool onOff)
    {
        PolyNavObstacle col = who.GetComponentInChildren<PolyNavObstacle>(true);
        col.enabled = onOff;
    }
    public static void setMoveCollider(Transform who, bool onOff)
    {
        PolyNavObstacle col = who.GetComponentInChildren<PolyNavObstacle>(true);
        col.enabled = onOff;
    }


    public static void moveUnit(GameObject who, Vector2 where, float speed)
    {
        who.GetComponent<Character>().facing = Character.setFace(who.transform.position, where);
        who.transform.position = Vector2.MoveTowards(who.transform.position, where, Time.deltaTime * speed);
        setDepth(who);
    }

    public static void setDepth(GameObject who)
    {
        Vector3 newDepth = who.transform.position;
        newDepth.z = who.transform.position.y;
        who.transform.position = newDepth;
    }

    public static bool enterRoom(float speed)
    {
        bool done = true;
        float dist = 0.01f;
        if (Vector2.Distance(GameObject.Find("Hero1").transform.position, GameObject.Find("1").transform.position) > dist)
        { 
            moveUnit(GameObject.Find("Hero1"), GameObject.Find("1").transform.position, speed);
            done = false;
        }
        if (Vector2.Distance(GameObject.Find("Hero2").transform.position, GameObject.Find("2").transform.position) > dist)
        {
            moveUnit(GameObject.Find("Hero2"), GameObject.Find("2").transform.position, speed);
            done = false;
        }
        if (Vector2.Distance(GameObject.Find("Hero3").transform.position, GameObject.Find("3").transform.position) > dist)
        {
            moveUnit(GameObject.Find("Hero3"), GameObject.Find("3").transform.position, speed);
            done = false;
        }
        if (Vector2.Distance(GameObject.Find("Hero4").transform.position, GameObject.Find("4").transform.position) > dist)
        {
            moveUnit(GameObject.Find("Hero4"), GameObject.Find("4").transform.position, speed);
            done = false;
        }
        if (Vector2.Distance(GameObject.Find("Hero5").transform.position, GameObject.Find("5").transform.position) > dist)
        {
            moveUnit(GameObject.Find("Hero5"), GameObject.Find("5").transform.position, speed);
            done = false;
        }

        return done;
    }

}
