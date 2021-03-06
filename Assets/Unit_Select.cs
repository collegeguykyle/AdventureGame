﻿using UnityEngine;
using System.Collections;
using Vectrosity;

public class Unit_Select : MonoBehaviour {

    private VectorLine selectCircle;
    private VectorLine destinationCircle;
    private VectorLine movePathLine;

    public GameObject selected;
    public float line_thickness = 2.0f;

    //Screen Drag Vars
    private Vector3 refPoint;
    private Vector3 CamPos;
    public float dragSpeed = -0.1f;

    private Vector3 pathGoal;

	private float selectedRadius = 3.0f;
	private Vector3 selectedPos;
	private Vector3[] lineList = new Vector3[20];
   

    void Start()
    {
        createLines();
    }

    void Update()
    {
        //Left click on units 
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D playerUnit = overPlayerUnit();
            if (playerUnit)
            {
                newSelectedUnit(playerUnit);
            }   
        }

        //Right mouse held / clicked while unit selected for movement
		if (selected && Input.GetMouseButton(0))
        {
			setDestinationCircle();
			connectLine();
        }

        //left mouse held to drag screen around
        if (Input.GetMouseButton(1)) 
        {
            screenDrag();
        }
    }

    void createLines()
    {
        Vector3[] v3;
        v3 = new Vector3[720];
        selectCircle = new VectorLine("Select Circle", v3, null, line_thickness);
        selectCircle.Draw3DAuto();
        destinationCircle = new VectorLine("Destination Circle", v3, null, line_thickness);
        destinationCircle.Draw3DAuto();
		movePathLine = new VectorLine("Move Path Line", lineList, null, line_thickness, LineType.Continuous);
		movePathLine.Draw3DAuto();
        VectorLine.canvas3D.sortingLayerName = "Select Circles";
    }

    RaycastHit2D overPlayerUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//1 << 8 raycasts only against layer 8 which has unit sprite hit boxes for unit selection
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << 8);
        return hit;
    }

    void newSelectedUnit(RaycastHit2D playerUnit)
    {
        selected = playerUnit.transform.gameObject;
        CircleCollider2D collider = selected.GetComponentInChildren<CircleCollider2D>();
		selectedRadius = collider.radius;
		selectedPos = collider.transform.position;
        selectCircle.MakeCircle(selectedPos, selectedRadius, 360);
        selectCircle.active = true;
		if (destinationCircle.active == true) destinationCircle.active = false;
		if (movePathLine.active == true) movePathLine.active = false;
    }

	void setDestinationCircle()
	{
		Vector3 point = MouseWorldPoint();

		//if mouse is over no walk collider, move destination location out of contact with colliders in scene
		Collider2D overlap = Physics2D.OverlapCircle(point, selectedRadius, 1 << 10);
		if (overlap)
		{
			Ray2D ray = new Ray2D(overlap.transform.position, point-overlap.transform.position);
			float dist = Vector2.Distance(overlap.transform.position, point);
			while (overlap)
			{
				dist += 0.1f;
				point = ray.GetPoint(dist);
				overlap = Physics2D.OverlapCircle(point, selectedRadius, 1 << 10);
			}
		}

		//Draw Destination circle
		destinationCircle.MakeCircle(point, selectedRadius, 360);
		destinationCircle.active = true;
		pathGoal = point;
	}
	
	void connectLine()
	{
		movePathLine.active = true;
		movePathLine.points3[0] = selectedPos;
		movePathLine.points3[1] = pathGoal;
		movePathLine.drawEnd = 1;
		//TODO add shift click for multiple way points in path
		//TODO add auto pathfinding to point capability
	}


    void screenDrag()
    {
        //TODO: change mouse icon to hand graby or something
        
        if (Input.GetMouseButtonDown(1))
        {
            refPoint = Input.mousePosition;
            CamPos = Camera.main.transform.position;  
        }
        Vector3 mouseNow = Input.mousePosition;  
        Vector3 temp3 = mouseNow - refPoint;
        temp3.z = 0;

        Camera.main.transform.position = CamPos + temp3 * dragSpeed;
    }

	Vector3 MouseWorldPoint()
	{
		Vector3 point;
		point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		point.z = 0;
		return point;
	
	}
}

