using UnityEngine;

public class RandomRotationAndScale : MonoBehaviour
{
    [SerializeField] float _maxRotationSpeed = 10f;
    [SerializeField] float _minRotationSpeed = 1f;
    [SerializeField] float _minScaleFactor = 0.5f;
    [SerializeField] float _maxScaleFactor = 1.5f;

    private float _rotationSpeed;

    void Start()
    {
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);

        float randomScale = Random.Range(_minScaleFactor, _maxScaleFactor);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
    }

    void Update()
    {
        // Rotar el objeto continuamente alrededor del eje Y
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
