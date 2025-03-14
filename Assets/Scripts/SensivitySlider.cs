using UnityEngine;
using UnityEngine.UI;

public class SensivitySlider : MonoBehaviour
{
    public Slider Slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetFloat("sensX", Slider.value);
        PlayerPrefs.SetFloat("sensY", Slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("sensX", Slider.value);
        PlayerPrefs.SetFloat("sensY", Slider.value);
    }
}
