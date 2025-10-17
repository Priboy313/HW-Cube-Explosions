using System.Collections.Generic;
using UnityEngine;

public class FragmentSpawner : MonoBehaviour
{
    [SerializeField] private Explosionable _explosionablePrefab;

    public List<Rigidbody> Spawn(
        int minCount, 
        int maxCount,
        Vector3 spawnPosition,
        Vector3 parentScale,
        int parentChanceToDivide
        )
    {
        List<Rigidbody> spawnedFragmentsRb = new();

        if (_explosionablePrefab == null)
        {
            Debug.LogError("Префаб осколка не установлен!");
            return spawnedFragmentsRb;
        }

        int count = DevUtils.GetRandomNumber(minCount, maxCount + 1);

        for (int i = 0; i < count; i++)
        {
            Explosionable newObject = Object.Instantiate(_explosionablePrefab, spawnPosition, Random.rotation);

            newObject.Initialize(parentScale, parentChanceToDivide);

            spawnedFragmentsRb.Add(newObject.Rb);
        }

        return spawnedFragmentsRb;
    }
}
