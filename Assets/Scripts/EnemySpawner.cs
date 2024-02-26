using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Sprite> sprites = new List<Sprite>();
    public List<GameObject> enemies = new List<GameObject>();

    public float spawnInterval = 5f;   // Interwa� czasowy mi�dzy spawnowaniem obiekt�w

    public float minX = -15f;   // Minimalna warto�� X
    public float maxX = 15f;    // Maksymalna warto�� X
    public float minY = -10f;   // Minimalna warto�� Y
    public float maxY = 75f;    // Maksymalna warto�� Y

    private float timer = 0f;   // Licznik czasu

   /* private float normalThreshold = 0.5f;       // 50%
    private float rareThreshold = 0.8f;         // 30% (50% + 30%)
    private float veryRareThreshold = 0.95f;    // 15% (50% + 30% + 15%)*/

    private void FixedUpdate()
    {
        // Zaktualizuj timer
        timer += Time.deltaTime;

        // Sprawd�, czy up�yn�� czas spawnowania kolejnego obiektu
        if (timer >= spawnInterval && enemies.Count <= 10)
        {
            // Resetuj timer
            timer = 0f;

            // Wygeneruj losowe pozycje
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            float randomAmountOfWeapons = Random.Range(1, 11);

            // Tw�rz nowy obiekt z prefabu na losowej pozycji
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);
            enemy.tag = enemyPrefab.tag;
            for(int i = 0; i < randomAmountOfWeapons; i++)
            {
                enemy.GetComponent<Player>().AddEnemyWeapon(enemy.GetComponent<Player>().startWeapon);
            }

            enemies.Add(enemy);
        }
    }
}
