using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Colorable : MonoBehaviour, IColorable
{
    private void Awake()
    {
        SetRandomColor();
    }

    public void SetRandomColor()
    {
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }
}
