using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteObserver : MonoBehaviour
{
    public static CompleteObserver Instance;

    private int _hitedTargets = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void HitTarget()
    {
        _hitedTargets++;

        if(_hitedTargets == LevelManager.Instance.GetTargetsCount())
        {
            GameManager.Instance.CompleteLevel();
        }
    }
}
