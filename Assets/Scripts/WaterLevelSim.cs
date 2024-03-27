using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelSim : MonoBehaviour
{
    public Transform boat;
    public float WaterY{get; set;}
    
    private void Update() {
        Vector3 pos = new Vector3(boat.position.x, transform.position.y, boat.position.z);
        transform.position = pos;    
    }

    private void LateUpdate() {
        WaterY = transform.position.y;
    }
}
