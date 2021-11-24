using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _stars;
    [SerializeField] private Sprite _starsImage;

    private WaitForSeconds _starsDrawingDelay = new WaitForSeconds(0.5f);
    public void ActiveStars(int starsCount)
    {
        var starsDrowing = StartCoroutine(DrowStars(starsCount));
    }

    private IEnumerator DrowStars(int starsCount)
    {
        yield return _starsDrawingDelay;
        for (int i = 0; i < starsCount; i++)
        {
            _stars[i].GetComponent<Image>().sprite = _starsImage;
            yield return _starsDrawingDelay;
        }
        yield break;
    }
}
