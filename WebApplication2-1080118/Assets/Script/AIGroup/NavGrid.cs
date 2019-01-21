using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
public class NavGrid
{
    public List<Vector3> gridPos = new List<Vector3>();
    public List<Vector3> vaildPos = new List<Vector3>();
    public List<NavPoint> navPoints = new List<NavPoint>();

    public int box_size = 5;

    //參考網址
    //https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
    public void GetGrid()
    {
        NavMeshHit hit;
        foreach(var surface in NavMeshSurface.activeSurfaces)
        {
            var min = surface.navMeshData.sourceBounds.min + surface.navMeshData.position;
            var max = surface.navMeshData.sourceBounds.max + surface.navMeshData.position;
            var surfaceCenterY = surface.navMeshData.sourceBounds.center.y;
            for(var x = min.x; x <= max.x; x += box_size)
            {
                for(var z = min.z; z <= max.z; z+= box_size)
                {
                    var pos = new Vector3(x,surfaceCenterY,z);
                    if(NavMesh.SamplePosition(pos,out hit,1f,-1))
                    {
                        vaildPos.Add(hit.position);
                        //vaildPos極有可能改成Vector4
                        //用vaildPos.FindAll(x=>Vector3.Distance(x.pos,target.pos) <= box_size)
                        //來找到想要更改，鎖定的點
                    }
                    gridPos.Add(pos);
                }
            }
        }
    }
    public void DrawGizmos()
    {      
        Gizmos.color = Color.blue;
 
        if (vaildPos != null)
        {
            foreach (var pos in vaildPos)
            {
                Gizmos.DrawSphere(pos, 0.3f);
            }
        }
 
        if (gridPos != null)
        {
            Gizmos.color = Color.gray;
            foreach (var pos in gridPos)
            {
                var halfBox = box_size;
                Gizmos.DrawSphere(pos, 0.1f);
                var a = pos - new Vector3(-halfBox, 0, -halfBox);
                var b = pos - new Vector3(-halfBox, 0, halfBox);
                var c = pos - new Vector3(halfBox, 0, halfBox);
                var d = pos - new Vector3(halfBox, 0, -halfBox);
                Gizmos.DrawLine(a, b);
                Gizmos.DrawLine(b, c);
                Gizmos.DrawLine(c, d);
                Gizmos.DrawLine(d, a);
            }
        }
    }
}