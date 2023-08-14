using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        PlayerController.Instance.Disable();
        UIManager.Instance.EnablePanel(PanelType.Menu);
        LevelManager.Instance.InitLevel();
    }

    public void GameStart()
    {
        UIManager.Instance.EnablePanel(PanelType.Game);
        PlayerController.Instance.Enable();
        PlayerController.Instance.EnableInput();
    }

    public void CompleteLevel()
    {
        UIManager.Instance.EnablePanel(PanelType.Complete);
        LevelManager.Instance.Complete();
        PlayerController.Instance.Disable();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
