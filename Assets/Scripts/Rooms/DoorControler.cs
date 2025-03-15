using UnityEngine;

public class DoorControler : MonoBehaviour
{
    [SerializeField] private Animator animControler;

    public void OpenDoor()
    {
        animControler.SetBool("DoorIsOpen", true);
    }

    public void CloseDoor()
    {
        animControler.SetBool("DoorIsOpen", false);
    }
}
