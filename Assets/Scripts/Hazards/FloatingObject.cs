using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float areaRadius = 10f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
        SetNewRandomTarget();
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    void SetNewRandomTarget()
    {
        float randomX = Random.Range(-areaRadius, areaRadius);
        float randomY = Random.Range(-areaRadius, areaRadius);
        float randomZ = Random.Range(-areaRadius, areaRadius);
        targetPosition = initialPosition + new Vector3(randomX, randomY, randomZ);
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewRandomTarget();
        }
    }
}
