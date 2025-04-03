using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float rewindDuration = 5f; // Макс. длительность перемотки (сек)
    [SerializeField] private KeyCode rewindKey = KeyCode.R; // Активация

    [Header("Particles")]
    [SerializeField] private ParticleSystem rewindParticles; // Система частиц

    // [Header("Shader")]
    // [SerializeField] private Material TimeRewindMaterial; // Шейдер
    
    private bool isRewinding = false; // Значение активности перемотки
    private float rewindTimer = 0f; // Таймер перемотки
    private List<Vector2> positionHistory = new List<Vector2>(); // История сохраненных позиций
    private Rigidbody2D rb;
    private PlayerController playerController; // Ссылка на скрипт управления

    // Инициализация при старте
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Кэшируем Rigibody2D
        playerController = GetComponent<PlayerController>(); // Кэшируем контроллер игрока
    }

    // Обработка ввода каждый кадр
    void Update()
    {
        // Активация перемотки по кнопке
        if (Input.GetKeyDown(rewindKey))
            StartRewind();
        // Остановка при отпускании кнопки
        if (Input.GetKeyUp(rewindKey))
            StopRewind();
    }

    // Физические вычисления (вызывается с фиксированным интервалом)
    void FixedUpdate()
    {
        if (isRewinding)
            Rewind(); // Режим перемотки
        else
            Record(); // Режим записи всего
    }

    // Запись позиции игрока
    private void Record()
    {
        // Если история превышает длительность перемотки – удаляем самый старый элемент
        if (positionHistory.Count > Mathf.Round(rewindDuration / Time.fixedDeltaTime))
        {
            positionHistory.RemoveAt(positionHistory.Count - 1); // Удаляем старые данные
        }
        
        positionHistory.Insert(0, transform.position); // Добавляем текущую позицию
    }

    // Перемотка времени
    private void Rewind()
    {
        if (positionHistory.Count > 0)
        {
            // Возвращаем игрока на прошлую позицию
            transform.position = positionHistory[0];
            positionHistory.RemoveAt(0); // Удаляем использованную позицию
            
            // Отключаем управление во время перемотки и физику
            playerController.enabled = false;
            rb.linearVelocity = Vector2.zero; // Минус скорость
        }
        else
        {
            StopRewind(); // Остановка если история закончилась
        }
    }

    // Запуск перемотки (Алгоритм сверху)
    private void StartRewind()
    {
        isRewinding = true; // Активируем флаг перемотки
        rb.gravityScale = 0; // Отключаем гравитацию
        rewindParticles.Play(); // Включаем частицы

        // Шейдер
        // TimeRewindMaterial.SetFloat("_DistortAmount", 0.1f);
    }

    // Остановка перемотки
    private void StopRewind()
    {
        isRewinding = false; // Деактивируем флаг перемотки
        rb.gravityScale = 3; // Включаем гравитацию
        playerController.enabled = true; // Возвращаем управление
        rewindParticles.Stop(); // Остановка частиц

        // Шейдер
        // TimeRewindMaterial.SetFloat("_DistortAmount", 0);
    }
}