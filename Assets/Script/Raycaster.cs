using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private Camera _camera;

    public event Action<GameObject> OnObjectClicked;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _inputReader.ClickLeftMouseButton += CastPerMouse;
    }

    private void OnDisable()
    {
        _inputReader.ClickLeftMouseButton -= CastPerMouse;
    }

    private void CastPerMouse()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            OnObjectClicked?.Invoke(hit.transform.gameObject);
        }
    }
}
