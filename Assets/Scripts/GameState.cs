using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState instance;

    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameState").AddComponent<GameState>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Use Unity's event system to notify observers
    public event Action OnGameStateChanged;

    public void AddObserver(IObserver observer)
    {
        OnGameStateChanged += observer.UpdateObserver;
    }

    public void RemoveObserver(IObserver observer)
    {
        OnGameStateChanged -= observer.UpdateObserver;
    }

    public void NotifyObservers()
    {
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged.Invoke();
        }
    }
}