using System.Collections.Generic;
using UnityEngine;

public static class FragmentSpawner
{
    public static List<Rigidbody> Spawn(
        GameObject fragmentPrefab,
        int minCount, 
        int maxCount,
        Vector3 spawnPosition,
        Vector3 parentScale,
        int parentChanceToDivide
        )
    {
        List<Rigidbody> spawnedFragmentsRb = new();

        if (fragmentPrefab == null)
        {
            Debug.LogError("Префаб осколка не установлен!");
            return spawnedFragmentsRb;
        }

        int count = DevUtils.GetRandomNumber(minCount, maxCount + 1);

        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Object.Instantiate(fragmentPrefab, spawnPosition, Random.rotation);
            
            if (newObject.TryGetComponent<Explosionable>(out Explosionable explosionableObject))
            {
                explosionableObject.Initialize(parentScale, parentChanceToDivide);
            }

            if (newObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                spawnedFragmentsRb.Add(rb);
            }
        }

        return spawnedFragmentsRb;
    }
}
