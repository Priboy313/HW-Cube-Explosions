using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action OnClickClickLeftMouseButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnClickClickLeftMouseButton?.Invoke();
        }
    }
}
