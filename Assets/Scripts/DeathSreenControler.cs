using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSreenControler : MonoBehaviour
{
    public void GoToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
