using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void ApplyExplosionForce(Vector3 explosionPosition, List<Rigidbody> childrensRb)
    {
        foreach (Rigidbody rb in childrensRb)
        {
            rb.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
        }
    }
}
