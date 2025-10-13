using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explosionable : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] [Min(0)] private int _fragmentsMin = 2;
    [SerializeField] [Min(1)] private int _fragmentsMax = 6;
    [SerializeField] private int _chanceToSpawnFraments = 100;
    [SerializeField] private GameObject _fragmentPrefab;

    private IInteractable _interactable;

    private void Awake()
    {
        _interactable = GetComponent<IInteractable>();
    }

    private void OnEnable()
    {
        _interactable.InteractionEvent += Explosion;
    }

    private void OnDisable()
    {
        _interactable.InteractionEvent -= Explosion;
    }

    private void OnValidate()
    {
        if (_fragmentsMax < _fragmentsMin)
        {
            _fragmentsMax = _fragmentsMin + 1;
        }
    }

    public void Initialize(Vector3 parentScale, int parentChace, int scaleDivide = 2, int chanceDivide = 2)
    {
        transform.localScale = parentScale / scaleDivide;
        _chanceToSpawnFraments = parentChace / chanceDivide;
    }

    private void Explosion()
    {
        Debug.Log("Explosion!");
        if (TryGetSpawnedFragments(out List<Rigidbody> childrenObjects))
        {
            ApplyExplosionForce(childrenObjects);
        }

        Destroy(gameObject);
    }

    private void ApplyExplosionForce(List<Rigidbody> childrenObjects)
    {
        foreach (Rigidbody rb in childrenObjects)
        {
            rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private bool TryGetSpawnedFragments(out List<Rigidbody> childrenObjects)
    {
        childrenObjects = null;

        if (_fragmentPrefab == null)
        {
            Debug.LogError("Fragment Prefab not assigned!");
            return false;
        }

        if (DevUtils.IsChanceSuccess(_chanceToSpawnFraments))
        {
            int count = DevUtils.GetRandomNumber(_fragmentsMin, _fragmentsMax + 1);

            childrenObjects = new();
            
            for (int i = 0; i < count; i++)
            {
                GameObject newObject = Instantiate(_fragmentPrefab, transform.position, Random.rotation);
                
                if (newObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    childrenObjects.Add(rb);
                }

                if (newObject.TryGetComponent<Explosionable>(out Explosionable explosionableObject))
                {
                    explosionableObject.Initialize(transform.localScale, _chanceToSpawnFraments);
                }
            }

            return true;
        }

        return false;
    }
}
