using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;

public class UIControl : MonoBehaviour {

    public static VectorLine selectCircle;
    public static VectorLine destinationCircle;
    public static VectorLine movePathLine;
    public static List<Vector2> lineList;
    public float line_thickness = 2.0f;

    void Start()
    {
        createLines();
    }

    void createLines()
    {
        Vector2[] v2;
        v2 = new Vector2[720];
        selectCircle = new VectorLine("Select Circle", v2, null, line_thickness);
        selectCircle.Draw3DAuto();
        destinationCircle = new VectorLine("Destination Circle", v2, null, line_thickness);
        destinationCircle.Draw3DAuto();
        movePathLine = new VectorLine("Move Path Line", lineList, null, line_thickness, LineType.Continuous);
        movePathLine.Draw3DAuto();
        VectorLine.canvas3D.sortingLayerName = "Select Circles";
    }
}
