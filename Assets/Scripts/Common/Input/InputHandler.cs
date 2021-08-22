using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] List<string> keys = new List<string>();

    void Update()
    {
        HandleKeys();
        HandleMouse();
    }

    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMouseButtonDown?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleKeys()
    {
        foreach(string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
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
    public event EventHandler OnLeftMouseButtonDown;
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

