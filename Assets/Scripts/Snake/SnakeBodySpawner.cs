using System;
using System.Collections;
using UnityEngine;
public class SnakeBodySpawner
{
    private GameObject SnakeBodyPrefab;
    private GameObject tail;

    public SnakeBodySpawner(GameObject _snakeBodyPrefab)
    {
        SnakeBodyPrefab = _snakeBodyPrefab;
    }

    public void Spawn(GameObject parent)
    {
        if (tail != null)
            throw new Exception("Tail is not null!");

        tail = GameObject.Instantiate(SnakeBodyPrefab);
        tail.GetComponent<SnakeBodyController>().SetParent(parent);

    }

    public void Spawn()
    {
        GameObject _tail = GameObject.Instantiate(SnakeBodyPrefab);

        _tail.GetComponent<SnakeBodyController>().SetParent(tail);
        tail.GetComponent<SnakeBodyController>().SetChild(_tail);

        tail = _tail;
    }

    public void UpdatePosition()
    {
        if (tail != null && tail.GetComponent<SnakeBodyController>() != null)
            tail.GetComponent<SnakeBodyController>().UpdatePosition();
    }

}