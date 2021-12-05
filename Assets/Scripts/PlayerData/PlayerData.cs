using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    private int[] _levelsScores;
    private int _maxStarsCount = 3;

    public PlayerData(int levelIndex, int score)
    {
        _levelsScores = new int[SceneManager.sceneCountInBuildSettings - 1];
        AddLevelScore(levelIndex, score);
    }

    public void AddLevelScore(int levelIndex, int score)
    {
        if (levelIndex <= _levelsScores.Length)
        {
            if (score <= _maxStarsCount && score >= 0 && score > _levelsScores[levelIndex - 1])
            {
                _levelsScores[levelIndex - 1] = score;
            }
        }
    }

    public int[] GetProgress()
    {
        int[] tempArray = new int[_levelsScores.Length];

        for (int i = 0; i < _levelsScores.Length; i++)
        {
            tempArray[i] = _levelsScores[i];
        }

        return tempArray;
    }
}
