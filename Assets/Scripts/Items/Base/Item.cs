using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    private bool _wasPickedUp = false;

    private Player _user;

    protected Player User => _user;
    public string Name => _name;
    public Sprite Icon => _icon;

    public virtual void Use()
    {
        Debug.Log("Use " + _name);
    }

    public void SetUser(Player player)
    {
        _user = player;
    }
}
