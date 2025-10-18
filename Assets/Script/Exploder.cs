using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void ApplyExplosionForceToChilds(Vector3 explosionPosition, List<Rigidbody> childrensRigidbody)
    {
        foreach (Rigidbody rigidbody in childrensRigidbody)
        {
            rigidbody.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
        }
    }

    public void ApplyExplosionForceAround(Explosionable explosionable)
    {
        Vector3 explosionPosition = explosionable.transform.position;
        float radius = _explosionRadius / explosionable.transform.localScale.y;
        float force = _explosionForce / explosionable.transform.localScale.y;

        foreach (Rigidbody rigidbody in GetPhysicsObjectsAround(explosionPosition, radius))
        {
            rigidbody.AddExplosionForce(force, explosionPosition, radius);
        }
    }

    private List<Rigidbody> GetPhysicsObjectsAround(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        List<Rigidbody> physicsObjects = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                physicsObjects.Add(hit.attachedRigidbody);
            }
        }

        return physicsObjects;
    }
}
