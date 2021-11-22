using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelButton : Interactable
{
    [SerializeField] private bool disposable;

    private Vector3 _targetPosition;
    public event UnityAction ButtonClick;

    private WaitForSeconds _deltaTime;

    private void Start()
    {
        _targetPosition = new Vector3(transform.position.x, transform.position.y - GetComponent<MeshRenderer>().bounds.size.y + 0.02f, transform.position.z);
        _deltaTime = new WaitForSeconds(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enabled == false)
            return;

        if (other.TryGetComponent<Player>(out Player player))
        {
            ButtonClick?.Invoke();
            if (disposable)
            {
                StartCoroutine(LowerButton());
                enabled = false;
            }
        }
    }

    private IEnumerator LowerButton()
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime);
            yield return _deltaTime;
        }
    }
}
