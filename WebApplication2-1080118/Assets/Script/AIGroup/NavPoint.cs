using UnityEngine;
using UnityEngine.AI;

public struct NavPoint
{
    public Vector3 position;
    public int heat;

    public NavPoint(Vector3 position,int heat)
    {
        this.position = position;
        this.heat = heat;
    }
}