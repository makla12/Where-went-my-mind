using UnityEngine;

public class InteractText : MonoBehaviour
{
    public bool isInRange = false;
    public void Show() {
        isInRange = true;     
    }

    public void Hide() {
        isInRange = false;     
    }

    void Update()
    {
        if (isInRange) {
            // setActive(true);
        }
    }
}
