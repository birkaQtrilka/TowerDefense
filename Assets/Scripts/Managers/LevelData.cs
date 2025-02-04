using System;

[Serializable]
struct LevelData
{
    /// <summary>
    /// If level is unlocked
    /// </summary>
    public bool Unlocked;
    /// <summary>
    /// LevelName. Should coincide with scene name
    /// </summary>
    public string Name;
}
