using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    private int _duration = 3;
    private int _damage = 1;
    private WaitForSeconds _bleedingStep = new WaitForSeconds(1f);
    private Humanoid _target;

    private void Awake()
    {
        _target = GetComponent<Humanoid>();
    }

    private void OnEnable()
    {
        StartCoroutine(BloodLoss());
    }

    private void OnDisable()
    {
        StopCoroutine(BloodLoss());
    }

    private IEnumerator BloodLoss()
    {
        for (int i = 0; i < _duration; i++)
        {
            _target.TakeDamage(_damage);
            yield return _bleedingStep;
        }
        this.enabled = false;
    }
}
