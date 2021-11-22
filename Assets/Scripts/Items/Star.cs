using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Star : Interactable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pickUpSound;

    public event UnityAction StarCollected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _audioSource.PlayOneShot(_pickUpSound);
            StarCollected?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
