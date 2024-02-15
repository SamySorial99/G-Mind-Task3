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



public class Caretaker
{
    private GameMemento memento;

    public void SetMemento(GameMemento memento)
    {
        this.memento = memento;
        SaveMemento();
    }

    public GameMemento GetMemento()
    {
        LoadMemento();
        return memento;
    }

    private void SaveMemento()
    {
        string json = JsonUtility.ToJson(memento);
        PlayerPrefs.SetString("GameMemento", json);
        PlayerPrefs.Save();
    }

    private void LoadMemento()
    {
        string json = PlayerPrefs.GetString("GameMemento");
        if (!string.IsNullOrEmpty(json))
        {
            memento = JsonUtility.FromJson<GameMemento>(json);
        }
        else
        {
            memento = new GameMemento(Vector3.zero); // Initial position
        }
    }
}