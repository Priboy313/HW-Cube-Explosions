using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Colorable : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }
}
