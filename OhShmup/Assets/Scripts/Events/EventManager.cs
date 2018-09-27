using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event manager.
/// </summary>
public static class EventManager
{
    static List<GameOverInvoker> gameOverInvokers = new List<GameOverInvoker>();
    static List<UnityAction> gameOverListeners = new List<UnityAction>();

    #region GameOver

    public static void AddGameOverInvoker(GameOverInvoker gameOverInvoker)
    {
        // add the new invoker to the list of invokers
        gameOverInvokers.Add(gameOverInvoker);

        // ensure that all existing listeners are added to this new invoker
        foreach (UnityAction listener in gameOverListeners)
        {
            gameOverInvoker.AddListener(EventName.GameOverEvent, listener);
        }
    }

    public static void AddGameOverListener(UnityAction gameOverListener)
    {
        // add the new listener to the list of listeners
        gameOverListeners.Add(gameOverListener);

        // ensure that this new listener is added to all existing new invokers
        foreach (GameOverInvoker gameOverInvoker in gameOverInvokers)
            gameOverInvoker.AddListener(EventName.GameOverEvent, gameOverListener);
    }

    public static void Reset()
    {
        foreach (GameOverInvoker gameOverInvoker in gameOverInvokers)
            gameOverInvoker.RemoveAllListeners(EventName.GameOverEvent);

        gameOverInvokers.Clear();
        gameOverListeners.Clear();
    }

    #endregion
}
