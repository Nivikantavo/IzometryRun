using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private LevelView[] _levelViews;

    private void Start()
    {
        _levelViews = FindObjectsOfType<LevelView>();

        foreach (var view in _levelViews)
        {
            view.LevelSelected += OnLevelSelected;
        }
    }

    private void OnDisable()
    {
        foreach (var view in _levelViews)
        {
            view.LevelSelected -= OnLevelSelected;
        }
    }

    public void OnLevelSelected(int SceneNumber, bool unloadingRequired = false)
    {
        if (unloadingRequired == true)
            SceneManager.LoadSceneAsync(SceneNumber);
        else
            SceneManager.LoadScene(SceneNumber);
    }
}
