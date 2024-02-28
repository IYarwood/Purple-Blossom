using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Patroller : MonoBehaviour
{
    //Consts
    private const float rotationSlerpAmount = .68f;

    [Header("References")]
    public Transform trans;
    public Transform modelHolder;

    [Header("Stats")]
    public float movespeed = 30;

    //Private Variables
    private int currentPointIndex;
    private Transform currentPoint;

    private Transform[] patrolPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Get an unsorted list of patrol points
        List<Transform> points = GetUnsortedPatrolPoints();

        //Only continue if we have at least 1
        if (points.Count > 0)
        {
            //Prepare our array
            patrolPoints = new Transform[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                //Quick reference to current point
                Transform point = points[i];

                //Isolate just the patrol point number
                int closingParenthesisIndex = point.gameObject.name.IndexOf(')');
                string indexSubstring = point.gameObject.name.Substring(14, closingParenthesisIndex - 14);

                //Convert number from a string to integer
                int index = Convert.ToInt32(indexSubstring);

                //Set a reference in our script patrolPoints array
                patrolPoints[index] = point;

                //Unparent each patrol point so it doesn't move with us
                point.SetParent(null);

                //Hide patrol points in the Hierarchy
                point.gameObject.hideFlags = HideFlags.HideInHierarchy;

            }

            //Start patrolling at first point
            SetCurrentPatrolPoint(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Only operate if we have a currentPoint
        if (currentPoint != null)
        {
            //Move root GameObject towards current point:
            trans.position = Vector3.MoveTowards(trans.position, currentPoint.position, movespeed * Time.deltaTime);

            //If we're on top of point, change current point
            if (trans.position == currentPoint.position)
            {
                //If we're at last point
                if (currentPointIndex >= patrolPoints.Length - 1)
                {
                    //set to first point
                    SetCurrentPatrolPoint(0);
                }
                else //if we're not at the last point
                    SetCurrentPatrolPoint(currentPointIndex + 1);
                    //Go to index after current
            }
            else //if we're not on the point yet, rotate model towards it
            {
                Quaternion lookRotation = Quaternion.LookRotation((currentPoint.position - trans.position).normalized);

                modelHolder.rotation = Quaternion.Slerp(modelHolder.rotation, lookRotation, rotationSlerpAmount);
            }
        }
    }

    private void SetCurrentPatrolPoint(int index)
    {
        currentPointIndex = index;
        currentPoint = patrolPoints[index];
    }

    //Returns a List containing the Transform of each child with a name "Patrol Point ("
    private List<Transform> GetUnsortedPatrolPoints()
    {
        //Get the transform of each child in the patroller
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        //Declare a local list storing Transforms:
        List<Transform> points = new List<Transform>();

        //Loop through the child Transforms
        for (int i = 0; i < children.Length; i++)
        {
            //Check if the child's name starts with "Patrol Point("
            if (children[i].gameObject.name.StartsWith("Patrol Point ("))
            {
                //If so, add it to the list
                points.Add(children[i]);
            }
        }

        //Return the point List
        return points;
    }
}
