using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManeger : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    private void Awake() {
        winPanel.SetActive(false);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (FindFirstObjectByType<Counter>().win)
            {
                winPanel.SetActive(true);
            }
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Solitaire()
    {
        Scene scene = Scene.Solitaire;
        SceneManager.LoadScene((int)scene);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

public enum Scene
{
    Menu = 0,
    Solitaire = 1,
    Cheat = 2,
    Durak = 3
}