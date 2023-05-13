using System;
using System.Collections;
using UnityEngine;
public class SnakeBodySpawner
{
    private GameObject SnakeBodyPrefab;
    private GameWorldController gameWorldController;
    private PlayerController playerController;
    private GameObject tail;
    private int length = 0;
    public int Length { get { return length; } }

    public SnakeBodySpawner(GameObject _snakeBodyPrefab, PlayerController _playerController, GameWorldController _gameWorldController)
    {
        SnakeBodyPrefab = _snakeBodyPrefab;
        playerController = _playerController;
        gameWorldController = _gameWorldController;
    }

    public void Spawn(GameObject parent)
    {
        if (tail != null)
            throw new Exception("Tail is not null!");

        tail = GameObject.Instantiate(SnakeBodyPrefab);
        tail.GetComponent<SnakeBodyController>().GameWorldController = gameWorldController;
        tail.GetComponent<SnakeBodyController>().Parent = parent;
        tail.name = "SnakeBody " + length;
        length += 1;
    }

    public void Spawn()
    {
        if (tail == null)
            throw new Exception("Tail is null!");

        GameObject _tail = GameObject.Instantiate(SnakeBodyPrefab);
        _tail.GetComponent<SnakeBodyController>().GameWorldController = gameWorldController;
        _tail.name = "Body " + length;

        tail.GetComponent<SnakeBodyController>().Child = _tail;
        _tail.GetComponent<SnakeBodyController>().Parent = tail;

        tail = _tail;
        length += 1;
    }

    public void DeSpawn()
    {
        if (tail == null || tail.GetComponent<SnakeBodyController>() == null)
            return;

        GameObject parent = tail.GetComponent<SnakeBodyController>().Parent;
        if (parent.GetComponent<SnakeBodyController>() == null)
            return;

        tail = parent;
        GameObject.Destroy(tail.GetComponent<SnakeBodyController>().Child);
        length -= 1;
    }

    public void UpdatePosition()
    {
        if (tail != null && tail.GetComponent<SnakeBodyController>() != null)
            tail.GetComponent<SnakeBodyController>().UpdatePosition();
    }

}