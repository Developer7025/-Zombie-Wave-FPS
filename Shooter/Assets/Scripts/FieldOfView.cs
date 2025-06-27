using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class FieldOfView 
{

    
    Transform traget;

    LayerMask player = 8;
    LayerMask obstacle = 128;

    public bool InFieldOfView(Transform transform , bool canSeePlayer , float radius , float angle)
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, player);
        if (rangeChecks.Length != 0)
        {
            traget = rangeChecks[0].transform;
            Vector3 directionToTraget = (traget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTraget) < angle / 2)
            {
                float distanceToTraget = Vector3.Distance(transform.position, traget.position);
                if (!Physics.Raycast(transform.position, directionToTraget, distanceToTraget, obstacle))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;

            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
        return canSeePlayer;
    }

    public bool ObstacleBetweenTarget(Transform transform , Vector3 directionToTraget, float distanceToTraget)
    {
        if (Physics.Raycast(transform.position, directionToTraget, distanceToTraget, obstacle))
        {
            return true;
        }
        return false;
    }
}


