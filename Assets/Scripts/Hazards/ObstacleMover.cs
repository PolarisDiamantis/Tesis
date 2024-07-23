using UnityEngine;
using System.Collections.Generic;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] List<Transform> points;
    [SerializeField] float speed = 2.0f;

    private int currentIndex;
    private int nextIndex;

    void Start()
    {
        if (points.Count < 2)
        {
            return;
        }

        currentIndex = Random.Range(0, points.Count);
        transform.position = points[currentIndex].position;
        ChooseNextIndex();
    }

    void Update()
    {
        if (points.Count < 2) return;

        MoveTowardsNextPoint();
    }

    void MoveTowardsNextPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[nextIndex].position, speed * Time.deltaTime);

        if (transform.position == points[nextIndex].position)
        {
            currentIndex = nextIndex;
            ChooseNextIndex();
        }
    }

    void ChooseNextIndex()
    {
        do
        {
            nextIndex = Random.Range(0, points.Count);
        } while (nextIndex == currentIndex);
    }
}
