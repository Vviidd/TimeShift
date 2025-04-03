using UnityEngine;

public class CircularMovingPlatform : MonoBehaviour
{
    [SerializeField] private float radius = 3f; // Радиус круга
    [SerializeField] private float speed = 1f; // Скорость движения
    [SerializeField] private Vector3 centerPoint; // Центр окружности

    private float angle = 0f;

    void Update()
    {
        // Увеличиваем угол на основе времени и скорости
        angle += speed * Time.deltaTime;
        
        // Вычисляем новую позицию по кругу
        float x = centerPoint.x + Mathf.Cos(angle) * radius;
        float z = centerPoint.z + Mathf.Sin(angle) * radius;
        
        // Применяем позицию
        transform.position = new Vector3(x, transform.position.y, z);
    }
}