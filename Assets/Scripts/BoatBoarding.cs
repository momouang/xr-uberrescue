using UnityEngine;

public class BoatBoarding : MonoBehaviour
{
    [SerializeField]
    Transform boatBoard;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent.SetParent(boatBoard);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent.SetParent(null);
        }
    }
}
