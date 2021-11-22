using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private EndLevelPanel _endLevelPanel;
    [SerializeField] private int _currentLevelIndex;
    [SerializeField] private Button _pauseButton;

    private Exit _exit;
    private Humanoid[] _humans;
    private Player _player;

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRepeatButtonClick()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void OnNextButtonClick()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings && nextSceneIndex > 0)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void Awake()
    {
        _exit = FindObjectOfType<Exit>();
        _humans = FindObjectsOfType<Humanoid>();
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        _endLevelPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _player.HumanDie += LevelEnd;
        _exit.OnLevelCompleted += LevelEnd;
    }

    private void OnDisable()
    {
        _player.HumanDie -= LevelEnd;
        _exit.OnLevelCompleted -= LevelEnd;
    }

    private void LevelEnd(bool won)
    {
        _pauseButton.interactable = false;
        _endLevelPanel.OpenPanel(won);
        StopHumans();

        if(won)
            SaveProgress();
    }

    private void StopHumans()
    {
        foreach (var human in _humans)
        {
            human.DisableMovment();
        }
    }

    private void SaveProgress()
    {
        SaveSystem.SaveProgress(_currentLevelIndex, _endLevelPanel.StarsCollected);
    }
}
