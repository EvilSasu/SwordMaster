using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f; // Prêdkoœæ poruszania siê postaci
    public float changeDirectionInterval = 10f; // Interwa³ zmiany kierunku w sekundach

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeMovementDirection();
    }

    void Update()
    {
        // Poruszanie siê w obecnym kierunku
        rb.velocity = movementDirection * speed;

        // Aktualizacja timera
        timer += Time.deltaTime;

        // SprawdŸ, czy czas na zmianê kierunku
        if (timer >= changeDirectionInterval)
        {
            ChangeMovementDirection();
            timer = 0f;
        }
    }

    // Funkcja zmieniaj¹ca kierunek ruchu postaci
    void ChangeMovementDirection()
    {
        // Losowy kierunek ruchu
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
    }
}
