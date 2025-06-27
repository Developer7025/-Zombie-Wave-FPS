using Unity.Burst;
using UnityEngine;

public class ZombieEar
{
    public bool hearshoot;
    public static bool shotfired ;
    float hearingRange = 150f;

    public static Vector3 Target ;

    public void CanHearShoot(Transform transform, bool hearshot)
    {
        if (Target != Vector3.zero && shotfired)
        {
            float distance = Vector3.Distance(transform.position, Target);
            if (distance <= hearingRange)
            {
                hearshoot = true;
            }
            else
                hearshoot = false;
        }
        else
            if (hearshot)
            {
                if (Vector3.Distance(Target, transform.position) < 5f)
                    hearshoot = false;
                else
                    hearshoot = true;
            }
            else
            hearshoot = hearshot ;
    }
    public void ResetHearShoot()
    {
        hearshoot = false;
    }
    public void SetTraget(Transform hitPosition)
    {
        Target = new Vector3(hitPosition.position.x, hitPosition.position.y, hitPosition.position.z)  ;
    }
    public void SetTraget(Vector3 position)
    {
        Target = position;
    }
}
