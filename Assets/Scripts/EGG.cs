using UnityEngine;

public class EGG : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float eggForce = 10f;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("bullet")){
            Debug.Log("EGG!");
            rb.AddForce(Vector3.up * eggForce, ForceMode.Impulse);
        }
    }
}
