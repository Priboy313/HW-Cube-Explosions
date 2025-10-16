using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Exploder), typeof(FragmentSpawner))]
public class ExplosionManager : MonoBehaviour
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
        _raycaster.OnObjectClicked += ClickedObjectObserver;
    }

    private void OnDisable()
    {
        _raycaster.OnObjectClicked -= ClickedObjectObserver;
    }

    private void ClickedObjectObserver(GameObject clickedObject)
    {
        if (clickedObject.TryGetComponent<Explosionable>(out Explosionable explosionable))
        {
            List<Rigidbody> childrensRb = new();

            if (DevUtils.IsChanceSuccess(explosionable.ChanceToDivide))
            {
                 childrensRb = _fragmentSpawner.Spawn(
                    explosionable.FragmentsCountMin,
                    explosionable.FragmentsCountMax,
                    clickedObject.transform.position,
                    clickedObject.transform.localScale,
                    explosionable.ChanceToDivide
                );
            }

            _exploder.ApplyExplosionForce(clickedObject.transform.position, childrensRb);
            Destroy(clickedObject);
        }
    }
}
