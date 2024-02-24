using System;
using System.Collections.Generic;

public static class EventHolder<T> where T : class
{
    private static readonly List<Action<T>> Listeners = new List<Action<T>>();
    private static T currentInfoState;

    public static void AddListener(Action<T> listener, bool instantNotify = true)
    {
        Listeners.Add(listener);
        if (instantNotify && currentInfoState != null)
        {
            listener?.Invoke(currentInfoState);
        }
    }

    public static void RaiseRegistrationInfo(T state)
    {
        currentInfoState = state;
        foreach (var listener in Listeners)
        {
            listener?.Invoke(state);
        }
    }

    public static void RemoveListener(Action<T> listener)
    {
        if (Listeners.Contains(listener))
        {
            Listeners.Remove(listener);
        }
    }
}