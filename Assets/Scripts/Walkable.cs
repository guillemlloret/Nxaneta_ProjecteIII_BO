using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{

    public List<WalkPath> possiblePaths = new List<WalkPath>();

    [Space]

    public Transform previousBlock;

    [Space]

    [Header("Booleans")]
    public bool isStair = false;
    public bool isButton = false;
    public bool isOccupied = false;

    [Space]
    public bool finalRed = false;
    public bool finalBlue = false;

    [Space]
    [Header("level2")]
    public bool isButtonRed = false;
    public bool isButtonBlue = false;



    [Space]

    [Header("Offsets")]
    public float walkPointOffset = .5f;
    public float stairOffset = .4f;

    public Vector3 GetWalkPoint()
    {

        float stair = isStair ? stairOffset : 0;
        return transform.position + transform.up * walkPointOffset - transform.up *stair;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        float stair = isStair ? .4f : 0;
        Gizmos.DrawSphere(GetWalkPoint(), .1f);   

        if (possiblePaths == null)
        {
            return;
        }

        foreach (WalkPath p in possiblePaths)
        {
            if (p.target == null)
                return;
            Gizmos.color = p.active ? Color.black : Color.clear;
            Gizmos.DrawLine(GetWalkPoint(), p.target.GetComponent<Walkable>().GetWalkPoint());
        }
    }

    public void MakeOccupied(Walkable cube)
    {
        cube.isOccupied = true;
    }
    
}


[System.Serializable]

public class WalkPath
{
    public Transform target;
    public bool active = true;
    public GameObject cube;
    public bool occupied = false;
}


