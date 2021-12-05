using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class LevelButton : Interactable
{
    [SerializeField] private bool _disposable;

    private Vector3 _targetPosition;
    public event UnityAction ButtonClicked;


    private void Start()
    {
        _targetPosition = new Vector3(transform.position.x, transform.position.y - GetComponent<MeshRenderer>().bounds.size.y + 0.02f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enabled == false)
            return;

        if (other.TryGetComponent<Player>(out Player player))
        {
            ButtonClicked?.Invoke();
            if (_disposable)
            {
                StartCoroutine(LowerButton());
                enabled = false;
            }
        }
    }

    private IEnumerator LowerButton()
    {
        WaitForSeconds deltaTime = new WaitForSeconds(Time.deltaTime);

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime);
            yield return deltaTime;
        }
        yield break;
    }
}
