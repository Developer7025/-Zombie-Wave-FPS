using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       
    }
    public void Quit()
    { 
        Application.Quit();
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
