using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void ApplyExplosionForce(Vector3 explosionPosition, List<Rigidbody> childrensRigidbody)
    {
        foreach (Rigidbody rigidbody in childrensRigidbody)
        {
            rigidbody.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
        }
    }
}
