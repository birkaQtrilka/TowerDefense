using UnityEngine;
using UnityEngine.UI;

public class LevelsUI : MonoBehaviour
{
    [SerializeField] Button[] _levelButtons;


    void OnEnable()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            var copy = i;
            _levelButtons[i].onClick.AddListener(() => { 
                LevelsManager.Instance.TryGoToLevel(copy);
            });

            
        }
    }

    void Start()
    {
        SetLevelToLockedVisuals();
    }

    void OnDisable()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].onClick.RemoveAllListeners();
        }
    }

    void SetLevelToLockedVisuals()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            if (LevelsManager.Instance.LevelIsCompleted(i))
                _levelButtons[i].transform.Find("Image").GetComponent<Image>().gameObject.SetActive(false);
        }
    }
}
