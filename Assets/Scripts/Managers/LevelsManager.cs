using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Responsible to moving through scenes
/// </summary>
public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance { get; private set; }
    

    [SerializeField] LevelData[] _levels;

    //it's for a continue feature, so you don't have to go to the levels scene all the time
    int _farthestLevel ;
    int _currLevel;
    //keeps track of the _levels index and not the scene build index
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
        //get's the current level index automatically, so I don't have to set it manually in the inspector
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
    /// <summary>
    /// changes to next level scene if available
    /// </summary>
    public void NextLevel()
    {
        if (CurrLevel + 1 >= _levels.Length) return;

        GoToLevel(_levels[++CurrLevel].Name);
    }
    /// <summary>
    /// Goes to level by name if it exists
    /// </summary>
    /// <param name="lvlName"></param>
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
    /// <summary>
    /// Goes to level by index if it exists
    /// </summary>
    /// <param name="lvlName"></param>
    public void TryGoToLevel(int level)
    {
        if (level < 0 || level >= _levels.Length || !_levels[level].Unlocked) return;

        GoToLevel(level);
    }
    /// <summary>
    /// Goes to level by index, doesn't do a contains check
    /// </summary>
    /// <param name="level"></param>
    public void GoToLevel(int level)
    {
        CurrLevel = level;
        GoToLevel(_levels[CurrLevel].Name);
    }
    ///
    public bool LevelIsCompleted(int level)
    {
        return _levels[level].Unlocked;
    }

    //used for scenes outside the levels
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
    
    /// <summary>
    /// Goes to the highest visited level
    /// </summary>
    public void GoToFarthestLevel()
    {
        GoToLevel(_farthestLevel);
    }
}
