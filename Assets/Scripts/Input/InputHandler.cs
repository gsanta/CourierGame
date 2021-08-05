using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] List<string> keys = new List<string>();

    void Update()
    {
        HandleKeys();
    }

    private void HandleKeys()
    {
        foreach(string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log(key);
                TriggerKeyDown(new KeyDownEventArgs(key));
            }
        }
    }

    private void TriggerKeyDown(KeyDownEventArgs e)
    {
        EventHandler<KeyDownEventArgs> handler = OnKeyDown;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler<KeyDownEventArgs> OnKeyDown;
}

public class KeyDownEventArgs : EventArgs
{
    private string key;

    internal KeyDownEventArgs(string _key)
    {
        key = _key;
    }
    public string Key { get => key; }
}

