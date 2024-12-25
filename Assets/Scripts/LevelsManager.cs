using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
struct LevelData
{
    public bool Unlocked;
    public string Name;
}

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance { get; private set; }
    

    [SerializeField] LevelData[] _levels;

    int _farthestLevel ;
    int _currLevel;
    
    public int CurrLevel 
    { 
        get { 
            return _currLevel; 
        } 
        private set { 
            _currLevel = value;
            if (_currLevel > _farthestLevel) _farthestLevel = _currLevel;
        } 
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {

        string lvlName = SceneManager.GetActiveScene().name;
        int i = 0;
        bool found = false;
        for (; i < _levels.Length; i++)
        {
            if (_levels[i].Name == lvlName)
            {
                found = true;
                break;
            }
        }
        if(found)
            CurrLevel = i;    
    }

    public void MarkNextLevelUnlocked()
    {
        if (CurrLevel + 1 >= _levels.Length) return;
        _levels[CurrLevel+1].Unlocked = true;
    }

    public void NextLevel()
    {
        if (CurrLevel + 1 >= _levels.Length) return;

        GoToLevel(_levels[++CurrLevel].Name);
    }

    public void GoToLevel(string lvlName)
    {
        int i = 0;
        for (; i < _levels.Length; i++)
        {
            if (_levels[i].Name == lvlName)
                break;
        }

        CurrLevel = i;
        SceneManager.LoadScene(lvlName);
    }

    public void TryGoToLevel(int level)
    {
        if (level < 0 || level >= _levels.Length || !_levels[level].Unlocked) return;

        GoToLevel(level);
    }

    public void GoToLevel(int level)
    {
        CurrLevel = level;
        GoToLevel(_levels[CurrLevel].Name);
    }

    public bool LevelIsCompleted(int level)
    {
        return _levels[level].Unlocked;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToLevelsScene()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoNextLevel()
    {
        GoToLevel(CurrLevel+1);
    }

    public void GoToFarthestLevel()
    {
        GoToLevel(_farthestLevel);
    }
}
