using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContentFilling : MonoBehaviour
{
    [SerializeField] private LevelView _prefabView;
    [SerializeField] private GameObject _itemsContainer;
    [SerializeField] private List<Level> _levels;

    private void Awake()
    {
        LoadProgress();
    }
    private void Start()
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            AddLevel(_levels[i]); 
        }
    }

    private void OnDisable()
    {
        DestroyAllViews();
    }

    private void DestroyAllViews()
    {
        LevelView[] levelViews = _itemsContainer.GetComponentsInChildren<LevelView>();

        foreach (var view in levelViews)
        {
            Destroy(view.gameObject);
        }
    }

    private void AddLevel(Level level)
    {
        var view = Instantiate(_prefabView, _itemsContainer.transform);
        
        view.Render(level);
    }


    private void LoadProgress()
    {
        PlayerData data = SaveSystem.LoadProgress();

        int[] levelsScors = data.GetProgress();

        if (levelsScors == null)
            return;

        for (int i = 0; i < levelsScors.Length; i++)
        {
            _levels[i].Score = levelsScors[i];
            if(i < _levels.Count - 1)
            {
                Debug.Log(i + " " + _levels.Count);
                if (_levels[i].Score > 0 && _levels[i + 1].Unlocked == false)
                {
                    _levels[i + 1].Unlocked = true;
                }
            }
        }
    }
}

[System.Serializable]
public class Level
{
    public int Index;
    public int Score;
    public bool Unlocked;

    public Level(int index1, int score, bool unlocked)
    {
        Index = index1;
        Score = score;
        Unlocked = unlocked;
    }
}
