using System.Collections.Generic;
using System;
using UnityEngine;

public interface IObserver
{
    void UpdateObserver();
}

// AutoSaveObserver class
public class AutoSaveObserver : MonoBehaviour, IObserver
{
    private void Start()
    {
        GameState.Instance.AddObserver(this);
    }

    public void UpdateObserver()
    {
        // Save the game state
        SaveManager.Instance.SaveGameState();
    }
}




[Serializable]
public class GameMemento
{
    public Vector3 PlayerPosition { get; private set; }

    public GameMemento(Vector3 playerPosition)
    {
        PlayerPosition = playerPosition;
    }
}

// Originator class to create and restore mementos



// Caretaker class to manage the mementos
public class Caretaker
{
    private Stack<GameMemento> mementos = new Stack<GameMemento>();

    public void AddMemento(GameMemento memento)
    {
        mementos.Push(memento);
    }

    public GameMemento GetMemento()
    {
        if (mementos.Count == 0)
        {
            return null;
        }
        return mementos.Pop();
    }
}