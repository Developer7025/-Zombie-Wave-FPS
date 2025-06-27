using Unity.VisualScripting;
using UnityEngine;

public class WalkPointsData 
{
    public static WalkPoints[] walkpointsData;

    public bool checkIfReached(Vector3 nextPosition, LayerMask zombie)
    {
        return Physics.CheckSphere(nextPosition, 3f, zombie);
    }
    public Transform SelectNextPoint(Transform preWalkPoint)
    {
        Transform nextpoint = null;
        foreach (WalkPoints walkPoints in walkpointsData)
        {
            if (walkPoints.walkpoint == preWalkPoint)
            {
                while (true)
                {

                    nextpoint = walkPoints.nextwalkpoints[(int)Random.Range(0f, (float)walkPoints.nextwalkpoints.Length)];

                    if (nextpoint != preWalkPoint) break;
                }
                
            }
        }

        return nextpoint;
    }
}
