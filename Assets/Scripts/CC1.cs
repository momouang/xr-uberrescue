using UnityEngine;

public class CC1 : MonoBehaviour
{
    public CharacterController controller;

    void Update()
    {
        controller.Move(Vector3.zero);
    }
}
