using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Player player;
    public int health;
    public int damage;

    private void Start()
    {
        weaponPrefab = this.gameObject;
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            player.weapons.Remove(this);
            player.CalculateOverPower();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(player.isPlayer == true)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                foreach (Weapon we in collision.gameObject.GetComponent<Player>().weapons)
                {
                    WeaponPickUp newWeapon = gameObject.AddComponent<WeaponPickUp>();
                    newWeapon.damage = we.damage;
                    newWeapon.health = we.health;
                    newWeapon.weaponPrefab = we.weaponPrefab;
                    newWeapon.sprite = we.GetComponent<SpriteRenderer>().sprite;
                    player.AddWeapon(newWeapon);
                }
                //Destroy(collision.gameObject);
                collision.gameObject.GetComponent<Player>().health = health - 1;
            }
            else if (collision.gameObject.tag == "EnemyWeapon")
            {
                health -= collision.gameObject.GetComponent<Weapon>().damage;
                player.weaponRotationObject.ChangeDirection();
            }
        }
        else
        {
            if (collision.gameObject.tag == "Ally")
            {
                foreach (Weapon we in collision.gameObject.GetComponent<Player>().weapons)
                {
                    WeaponPickUp newWeapon = gameObject.AddComponent<WeaponPickUp>();
                    newWeapon.damage = we.damage;
                    newWeapon.health = we.health;
                    newWeapon.weaponPrefab = we.weaponPrefab;
                    newWeapon.sprite = we.GetComponent<SpriteRenderer>().sprite;
                    player.AddWeapon(newWeapon);
                }
                collision.gameObject.GetComponent<Player>().health = health - 1;
            }
            else if (collision.gameObject.tag == "AllyWeapon")
            {
                health -= collision.gameObject.GetComponent<Weapon>().damage;
                player.weaponRotationObject.ChangeDirection();
            }
        }
    }
}
