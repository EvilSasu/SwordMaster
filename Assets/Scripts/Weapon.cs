using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public int health;
    public int damage;

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
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Jest colizja z " + collision.gameObject.name);
            Destroy(this.gameObject);
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Jest colizja z " + collision.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
