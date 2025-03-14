using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour {
	public Button yourButton;
    public GameObject menu;
    public GameObject settings;

	void Start () {
		yourButton.onClick.AddListener(TaskOnClick);
	}

    void OnDestroy(){
        yourButton.onClick.RemoveListener(TaskOnClick);
    }

	void TaskOnClick(){
		menu.SetActive(false);
        settings.SetActive(true);
	}
}