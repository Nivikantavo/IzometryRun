using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelPanel : MonoBehaviour
{
    [SerializeField] private Button _nextButton;

    private TextMeshProUGUI _endText;
    private StarsPanel _starsPanel;

    public int StarsCollected;

    private void OnEnable()
    {
        _endText = GetComponentInChildren<TextMeshProUGUI>();
        _starsPanel = GetComponentInChildren<StarsPanel>();
        StarsCollected = FindObjectOfType<StarsCollector>().Collected;
    }

    public void OpenPanel(bool won)
    {
        gameObject.SetActive(true);
        Initiation(won);
    }

    private void Initiation(bool won)
    {
        if (won)
        {
            _endText.text = "You win!";
            _starsPanel.ActiveStars(StarsCollected);
        }
        else
        {
            _endText.text = "You lose!";
            _nextButton.interactable = false;
        }
    }
}
