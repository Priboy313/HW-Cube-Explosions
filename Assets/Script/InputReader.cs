using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action ActionClick;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ActionClick?.Invoke();
        }
    }
}
