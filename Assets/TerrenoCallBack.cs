using System;
using UnityEngine;

public class TerrenoCallBack : MonoBehaviour
{
    private Action _callback;

    public void SetUp(Action callback)
    {
        _callback = callback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _callback?.Invoke();
    }
}