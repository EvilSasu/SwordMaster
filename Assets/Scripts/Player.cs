using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public WeaponPickUp startWeapon;
    public Transform weaponsParent;
    public List<Weapon> weapons = new List<Weapon>();
    public int health;
    public int playerPower;
    public ParticleSystem blood;
    public bool isAlive = true;
    [SerializeField] private Animator animator;

    private Vector2[] rotationPositions = new Vector2[]
    {
        new Vector2(-17.5f, 0f),  // Dla 0 stopni
        new Vector2(0f, 17.5f),   // Dla 90 stopni
        new Vector2(17.5f, 0f),   // Dla 180 stopni
        new Vector2(0f, -17.5f)   // Dla 270 stopni
    };

    private void Start()
    {
        AddWeapon(startWeapon);
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(health <= 0 && isAlive) 
        {
            isAlive = false;
            if (this.GetComponent<PlayerMovement>())
            {
                this.GetComponent<PlayerMovement>().enabled = false;
                if(isAlive == false)
                {
                    RemoveWeapons();
                    animator.SetBool("Idle", false);
                    animator.SetBool("isRunning", false);
                    blood.Play();
                    animator.SetTrigger("Dead");
                    StartCoroutine(AfterDeath());
                }          
            }           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PickUp")
        {
            AddWeapon(collision.gameObject.GetComponent<WeaponPickUp>());
            Destroy(collision.gameObject);
        }
    }

    public void AddWeapon(WeaponPickUp weapon)
    {   
        GameObject newWeapon = Instantiate(weapon.weaponPrefab, weapon.weaponPrefab.transform.localPosition, weapon.weaponPrefab.transform.localRotation, weaponsParent);
        newWeapon.transform.parent = weaponsParent;
        newWeapon.transform.localPosition = weapon.weaponPrefab.transform.localPosition;
        newWeapon.transform.localRotation = weapon.weaponPrefab.transform.localRotation;
        newWeapon.GetComponent<Weapon>().health = weapon.health;
        newWeapon.GetComponent<Weapon>().damage = weapon.damage;
        newWeapon.GetComponent<Weapon>().player = this;
        newWeapon.GetComponent<SpriteRenderer>().sprite = weapon.sprite;
        weapons.Add(newWeapon.GetComponent<Weapon>());

        SetupWeapons();
        CalculateOverPower();
    }

    private void SetupWeapons()
    {
        int amount = weapons.Count;
        float rotationChange = 360 / amount;
        int i = 0;
        foreach (Weapon we in weapons)
        {
            we.transform.localPosition = GetInterpolatedPosition(i * rotationChange);
            we.transform.localRotation = Quaternion.Euler(0, 0, i * rotationChange + 45f);
            i++;
        }
    }

    private void RemoveWeapons()
    {
        for (int i = weapons.Count - 1; i >= 0; i--)
        {
            Weapon we = weapons[i];
            weapons.RemoveAt(i);
            Destroy(we.gameObject);
        }
    }

    private Vector2 GetInterpolatedPosition(float rotation)
    {
        // ZnajdŸ indeksy punktów dla rotacji
        int index1 = Mathf.FloorToInt(rotation / 90f) % rotationPositions.Length;
        int index2 = (index1 + 1) % rotationPositions.Length;

        // Oblicz wagê dla interpolacji
        float t = (rotation % 90f) / 90f;

        // Interpoluj pozycjê miêdzy punktami
        return Vector2.Lerp(rotationPositions[index1], rotationPositions[index2], t);
    }

    public void CalculateOverPower()
    {
        playerPower = 0;
        foreach (Weapon we in weapons)
        {
            playerPower += we.damage + we.health;
        }
    }

    private IEnumerator AfterDeath()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetTrigger("Disapear");
        yield return new WaitForSeconds(0.3f);

        float minX = -15f;   // Minimalna wartoœæ X
        float maxX = 15f;    // Maksymalna wartoœæ X
        float minY = -10f;   // Minimalna wartoœæ Y
        float maxY = 75f;    // Maksymalna wartoœæ Y

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        transform.position = new Vector2(randomX, randomY);

        animator.SetTrigger("Apear");

        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Idle", true);
        health = 1;

        AddWeapon(startWeapon);
        this.GetComponent<PlayerMovement>().enabled = true;
        isAlive = true;
    }
}
