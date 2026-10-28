using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnPlayerDied;
    
    public static event Action<int> OnScoreAdded;

    public static void TriggerPlayerDied()
    {
        OnPlayerDied?.Invoke();
    }

    public static void TriggerScoreAdded(int score)
    {
        OnScoreAdded?.Invoke(score);
    }
}