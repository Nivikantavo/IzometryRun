using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdater : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;

    private Door[] _doors;
    
    private void Awake()
    {
        _doors = FindObjectsOfType<Door>();
        _surface.BuildNavMesh();
    }

    private void OnEnable()
    {
        foreach (var door in _doors)
        {
            door.DoorOpened += OnDoorOpen;
        }
    }

    private void OnDisable()
    {
        foreach (var door in _doors)
        {
            door.DoorOpened -= OnDoorOpen;
        }
    }

    private void OnDoorOpen()
    {
        _surface.BuildNavMesh();
    }
}
