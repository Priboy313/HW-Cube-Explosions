using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explosionable : MonoBehaviour
{
    [SerializeField, Min(1)] private int _fragmentsCountMin = 2;
    [SerializeField, Min(1)] private int _fragmentsCountMax = 6;
    [SerializeField, Range(0, 100)] private int _chanceToDivide = 100;

    public Rigidbody Rb { get; private set; }

    public int FragmentsCountMin => _fragmentsCountMin;
    public int FragmentsCountMax => _fragmentsCountMax;
    public int ChanceToDivide => _chanceToDivide;

    private void OnValidate()
    {
        if (_fragmentsCountMax < _fragmentsCountMin)
        {
            _fragmentsCountMax = _fragmentsCountMin + 1;
        }
    }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 parentScale, int parentChace, int scaleDivide = 2, int chanceDivide = 2)
    {
        transform.localScale = parentScale / scaleDivide;
        _chanceToDivide = parentChace / chanceDivide;
    }

}
