using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private Transform sweepTransform;
    private float rotationSpeed;
    private float radarDistance;

    RaycastHit hitinfo;
    bool hitDetect;
    Collider collider;

    private void Awake()
    {
        sweepTransform = transform.Find("Sweep");
        rotationSpeed = 180f;
        radarDistance = 1f;

        collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        sweepTransform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);

        hitDetect = Physics.BoxCast(transform.position, new Vector3(100,5,5),transform.forward ,Quaternion.AngleAxis(10, GetVectorFromAngle(sweepTransform.eulerAngles.z)), radarDistance);
        if (hitDetect)
        {
            //hitsomething
            Debug.Log("Hit");
            
        }
        else
        {
            Debug.Log("nothing");
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(hitDetect)
        {
            Gizmos.DrawWireCube(transform.position + transform.forward * hitinfo.distance, transform.localScale);
        }
        else
        {
            Gizmos.DrawWireCube(transform.position + transform.forward * radarDistance, transform.localScale*5);
        }

    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
