using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    private int[] _levelsScores;

    public PlayerData(int levelIndex, int score)
    {
        _levelsScores = new int[SceneManager.sceneCountInBuildSettings - 1];
        AddLevelScore(levelIndex, score);
    }

    public void AddLevelScore(int levelIndex, int score)
    {
        if (levelIndex <= _levelsScores.Length)
        {
            if (score <= 3 && score >= 0 && score > _levelsScores[levelIndex - 1])
            {
                _levelsScores[levelIndex - 1] = score;
            }
        }
    }

    public int[] GetProgress()
    {
        return _levelsScores;
    }
}
