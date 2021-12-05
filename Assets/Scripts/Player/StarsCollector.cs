using UnityEngine;

public class StarsCollector : MonoBehaviour
{
    private Star[] _stars;

    public int Collected { get; private set; }

    private void Awake()
    {
        _stars = FindObjectsOfType<Star>();
    }

    private void OnEnable()
    {
        foreach (var star in _stars)
        {
            star.Collected += StarCollected;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _stars)
        {
            item.Collected -= StarCollected;
        }
    }

    private void StarCollected()
    {
        Collected++;
    }
}
