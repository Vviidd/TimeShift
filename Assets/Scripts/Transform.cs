using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; // Скорость вращения (градусы в секунду)
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // Ось вращения (по умолчанию - вверх)

    void Update()
    {
        // Вращаем объект вокруг указанной оси
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}