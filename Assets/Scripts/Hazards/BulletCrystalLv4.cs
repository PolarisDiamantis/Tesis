using UnityEngine;

public class BulletCrystalLv4 : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private float lifetime = 5f;

    private void Start()
    {
        direction.Normalize();
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
