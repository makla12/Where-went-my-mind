using TMPro;
using UnityEngine;

public class GamblingMachine : MonoBehaviour
{
    public GameObject[] pickUps;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         GameObject InteractText = GameObject.FindWithTag("InteractText");
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < 3f)
        {
            if (InteractText != null)
            {
                InteractText.GetComponent<TextMeshProUGUI>().text = "Press E to gamble";
                InteractText.SetActive(true);
            }
            // InteractText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(pickUps[Random.Range(0, pickUps.Length)], new Vector3(transform.position.x + 3, transform.position.y + 1, transform.position.z), transform.rotation);
            }
        } else {
            InteractText.SetActive(false);
        }
    }
}