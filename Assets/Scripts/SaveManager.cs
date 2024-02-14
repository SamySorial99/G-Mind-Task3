using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;

    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SaveManager").AddComponent<SaveManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void SaveGameState()
    {
        // Save the game state
        GameMemento memento = GameOriginator.Instance.Save();
        string json = JsonUtility.ToJson(memento);
        PlayerPrefs.SetString("GameState", json);
        PlayerPrefs.Save();
    }

    public void LoadGameState()
    {
        // Load the game state
        string json = PlayerPrefs.GetString("GameState");
        GameMemento memento = JsonUtility.FromJson<GameMemento>(json);
        GameOriginator.Instance.Restore(memento);
    }
}