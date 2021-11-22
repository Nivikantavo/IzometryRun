using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsCollector : MonoBehaviour
{
    private int _starsCollected;
    private Star[] _stars;

    public int StarsCollected => _starsCollected;

    private void Awake()
    {
        _stars = FindObjectsOfType<Star>();
    }

    private void OnEnable()
    {
        foreach (var item in _stars)
        {
            item.StarCollected += StarCollected;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _stars)
        {
            item.StarCollected -= StarCollected;
        }
    }

    private void StarCollected()
    {
        _starsCollected++;
    }
}
