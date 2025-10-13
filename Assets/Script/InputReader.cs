using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action ClickLeftMouseButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClickLeftMouseButton?.Invoke();
        }
    }
}
