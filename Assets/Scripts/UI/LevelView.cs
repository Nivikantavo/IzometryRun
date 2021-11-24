using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    private TMP_Text _indexLabel;
    private Level _level;
    private Image[] _stars;
    private AudioSource _audioSource;

    [SerializeField] private GameObject _starsPanel;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Sprite _goldStar;
    [SerializeField] private AudioClip _clickSound;

    public int Index => _level.Index;

    public event UnityAction<int, bool> LevelSelected;

    public LevelView(Level level)
    {
        _level = level;

        _indexLabel.text = _level.Index.ToString();

        if (_level.Unlocked == true)
        {
            _level.Score = level.Score;
            FillingStars();
        }
        else
        {
            _starsPanel.SetActive(false);
            _lock.SetActive(true);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            _stars = _starsPanel.GetComponentsInChildren<Image>(true);
        }

        _audioSource = FindObjectOfType<AudioSource>();
        _indexLabel = GetComponentInChildren<TMP_Text>();
    }

    public void Render(Level level)
    {
        _level = level; 

        _indexLabel.text = _level.Index.ToString();
        
        if(_level.Unlocked == true)
        {
            _level.Score = level.Score;
            FillingStars();
        }
        else
        {
            _starsPanel.SetActive(false);
            _lock.SetActive(true);
        }
    }

    public void Unlock()
    {
        _level.Unlocked = true;
        _starsPanel.SetActive(true);
        _lock.gameObject.SetActive(false);
    }

    public void OnLevelClick()
    {
        if(_level.Unlocked == true)
        {
            _audioSource.PlayOneShot(_clickSound);
            LevelSelected?.Invoke(_level.Index, false);
        }
    }

    private void FillingStars()
    {
        for (int i = 0; i < _level.Score; i++)
        {
            _stars[i].sprite = _goldStar;
        }
    }
}

