using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Camera _camera;

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
            if (hit.transform.gameObject.TryGetComponent<IInteractable>(out IInteractable interactableObject))
            {
                interactableObject.Interaction();
            }
        }
    }
}
