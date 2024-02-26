using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Sprite sprite;
    public int health;
    public int damage;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
