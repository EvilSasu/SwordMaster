using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject weaponPickUp;
    public List<Sprite> sprites = new List<Sprite>();
    public List<GameObject> pickUps = new List<GameObject>();

    public float spawnInterval = 5f;   // Interwa³ czasowy miêdzy spawnowaniem obiektów

    public float minX = -15f;   // Minimalna wartoœæ X
    public float maxX = 15f;    // Maksymalna wartoœæ X
    public float minY = -10f;   // Minimalna wartoœæ Y
    public float maxY = 75f;    // Maksymalna wartoœæ Y

    private float timer = 0f;   // Licznik czasu

    private float normalThreshold = 0.5f;       // 50%
    private float rareThreshold = 0.8f;         // 30% (50% + 30%)
    private float veryRareThreshold = 0.95f;    // 15% (50% + 30% + 15%)

    private void FixedUpdate()
    {
        // Zaktualizuj timer
        timer += Time.deltaTime;

        // SprawdŸ, czy up³yn¹³ czas spawnowania kolejnego obiektu
        if (timer >= spawnInterval && pickUps.Count <= 30)
        {
            // Resetuj timer
            timer = 0f;

            // Wygeneruj losowe pozycje
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            float randomValueRarity = Random.value;

            // Twórz nowy obiekt z prefabu na losowej pozycji
            GameObject newWeaponPickUp = Instantiate(weaponPickUp, new Vector3(randomX, randomY, 0f), Quaternion.identity);

            if (randomValueRarity <= normalThreshold)
            {
                newWeaponPickUp.GetComponent<WeaponPickUp>().health = 1;
                newWeaponPickUp.GetComponent<WeaponPickUp>().damage = 1;
            }
            else if (randomValueRarity <= rareThreshold)
            {
                newWeaponPickUp.GetComponent<WeaponPickUp>().health = 2;
                newWeaponPickUp.GetComponent<WeaponPickUp>().damage = 2;
                newWeaponPickUp.GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            else if (randomValueRarity <= veryRareThreshold)
            {
                newWeaponPickUp.GetComponent<WeaponPickUp>().health = 4;
                newWeaponPickUp.GetComponent<WeaponPickUp>().damage = 4;
                newWeaponPickUp.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else
            {
                newWeaponPickUp.GetComponent<WeaponPickUp>().health = 7;
                newWeaponPickUp.GetComponent<WeaponPickUp>().damage = 7;
                newWeaponPickUp.GetComponent<SpriteRenderer>().sprite = sprites[2];
            }

            pickUps.Add(weaponPickUp);
        }
    }
}
