using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Raycaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _ignoredLayer;

    private Camera _camera;

    public event Action<GameObject> ActionObjectClicked;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _inputReader.ActionClick += OnClickCastPerMouse;
    }

    private void OnDisable()
    {
        _inputReader.ActionClick -= OnClickCastPerMouse;
    }

    private void OnClickCastPerMouse()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~_ignoredLayer))
        {
            ActionObjectClicked?.Invoke(hit.transform.gameObject);
        }
    }
}
