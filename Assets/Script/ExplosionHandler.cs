using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Exploder), typeof(FragmentSpawner))]
public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    private FragmentSpawner _fragmentSpawner;
    private Exploder _exploder;

    private void Awake()
    {
        _fragmentSpawner = GetComponent<FragmentSpawner>();
        _exploder = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _raycaster.ActionObjectClicked += OnObjectClicked;
    }

    private void OnDisable()
    {
        _raycaster.ActionObjectClicked -= OnObjectClicked;
    }

    private void OnObjectClicked(GameObject clickedObject)
    {
        if (clickedObject.TryGetComponent<Explosionable>(out Explosionable explosionable))
        {
            List<Rigidbody> childrensRigidbody = new();

            if (DevUtils.IsChanceSuccess(explosionable.ChanceToDivide))
            {
                 childrensRigidbody = _fragmentSpawner.Spawn(
                    explosionable.FragmentsCountMin,
                    explosionable.FragmentsCountMax,
                    clickedObject.transform.position,
                    clickedObject.transform.localScale,
                    explosionable.ChanceToDivide
                );
                _exploder.ApplyExplosionForceToChilds(clickedObject.transform.position, childrensRigidbody);
            }
            else
            {
                _exploder.ApplyExplosionForceAround(explosionable);
            }
            
            Destroy(clickedObject);
        }
    }
}
