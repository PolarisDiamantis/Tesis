using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] float _MaxRotationSpeed = 10f; 
    [SerializeField] float _MinRotationSpeed = 10f; 

    void Start()
    {
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);

        transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Random.Range(_MinRotationSpeed, _MaxRotationSpeed) * Time.deltaTime);
    }
}
