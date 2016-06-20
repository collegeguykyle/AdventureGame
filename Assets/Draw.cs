using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public static class Draw {

    public static void line (string lineName, Vector2 start, Vector2 end, bool overlay)
    {
        
        Vector2[] points = new Vector2[2];
        points[0] = start;
        points[1] = end;
        new VectorLine(lineName, points, null, 2.0f);
        Debug.Log("test line created");
    }
    public static void line(string lineName, Vector3 start, Vector3 end, bool overlay)
    {

    }

    public static bool lineExists(string lineName)
    {
        GameObject what = GameObject.Find(lineName);
        Debug.Log(what);
        if (what) return true;
        else return false;
    }

}
