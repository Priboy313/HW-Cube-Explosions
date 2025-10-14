using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explosionable : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField, Min(1)] private int _fragmentsCountMin = 2;
    [SerializeField, Min(1)] private int _fragmentsCountMax = 6;
    [SerializeField, Range(0, 100)] private int _chanceToDivide = 100;
    [SerializeField] private GameObject _fragmentPrefab;

    private IInteractable _interactable;

    private void OnValidate()
    {
        if (_fragmentsCountMax < _fragmentsCountMin)
        {
            _fragmentsCountMax = _fragmentsCountMin + 1;
        }
    }

    private void Awake()
    {
        _interactable = GetComponent<IInteractable>();
    }

    private void OnEnable()
    {
        _interactable.InteractionEvent += Explode;
    }

    private void OnDisable()
    {
        _interactable.InteractionEvent -= Explode;
    }

    public void Initialize(Vector3 parentScale, int parentChace, int scaleDivide = 2, int chanceDivide = 2)
    {
        transform.localScale = parentScale / scaleDivide;
        _chanceToDivide = parentChace / chanceDivide;
    }

    private void Explode()
    {
        if (DevUtils.IsChanceSuccess(_chanceToDivide))
        {
            List<Rigidbody> childrensRb = FragmentSpawner.Spawn(
                _fragmentPrefab,
                _fragmentsCountMin,
                _fragmentsCountMax,
                transform.position,
                transform.localScale,
                _chanceToDivide
            );

            ApplyExplosionForce(childrensRb);
        }

        Destroy(gameObject);
    }

    private void ApplyExplosionForce(List<Rigidbody> childrensRb)
    {
        foreach (Rigidbody rb in childrensRb)
        {
            rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

}
