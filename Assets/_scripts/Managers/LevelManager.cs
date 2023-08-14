using UnityEngine;

[System.Serializable]
public class Level
{
    public GameObject Parent;
    public Slicable[] Obstacles;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private Level[] _levels;
    private int _level = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void InitLevel()
    {
        _level = PlayerPrefs.GetInt("Lvl");
        _levels[_level].Parent.SetActive(true);
    }

    public void Complete()
    {
        _level++;
        if (_level >= _levels.Length) _level = 0;
        PlayerPrefs.SetInt("Lvl", _level);
        PlayerPrefs.Save();
    }

    public int GetTargetsCount()
    {
        return _levels[_level].Obstacles.Length;
    }
}
