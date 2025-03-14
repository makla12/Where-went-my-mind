using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class QuitGameScript : MonoBehaviour {
	public Button yourButton;

	void Start () {
		yourButton.onClick.AddListener(TaskOnClick);
	}

    void OnDestroy(){
        yourButton.onClick.RemoveListener(TaskOnClick);
    }

	void TaskOnClick(){
		Application.Quit();
	}
}