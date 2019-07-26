using System.Collections;
using UnityEngine;

public interface IObserverSubject
{
    void Notify();
    void AddObserver(Observer ob);
    void RemoveObserver(Observer ob);
}
public interface Observer
{
    void OnNotify();
}

