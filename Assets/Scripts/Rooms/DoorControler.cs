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

    private void Update() {
        if(Input.GetKeyDown(KeyCode.O))
        {
            OpenDoor();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            CloseDoor();
        }
    }
}
