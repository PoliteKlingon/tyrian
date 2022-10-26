using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorRotation : MonoBehaviour
{
    [SerializeField] private float range = 100;
    private Vector3 _rotation;
    
    private void Start()
    {
        _rotation = new Vector3(
            Random.Range(-range, range),
            Random.Range(-range, range),
            Random.Range(-range, range)
            );
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
}
