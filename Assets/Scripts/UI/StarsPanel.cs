using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsPanel : MonoBehaviour
{
    [SerializeField] private List<Image> _stars;
    [SerializeField] private Sprite _starsImage;

    public void ActiveStars(int starsCount)
    {
        StartCoroutine(DrowStars(starsCount));
    }

    private IEnumerator DrowStars(int starsCount)
    {
         WaitForSeconds starsDrawingDelay = new WaitForSeconds(0.5f);

        yield return starsDrawingDelay;

        for (int i = 0; i < starsCount; i++)
        {
            _stars[i].sprite = _starsImage;
            yield return starsDrawingDelay;
        }

        yield break;
    }
}
