using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RotateCenter : MonoBehaviour
{
    public Button boostButton;
    [SerializeField] private float rotationSpeed = -30f;
    public string direction = "right";
    private void FixedUpdate()
    {
        if(direction == "right")
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        else
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        if(direction == "right")
            direction = "left";
        else
            direction = "right";
    }

    public void BoostSpeed()
    {
        StartCoroutine(Boost());
    }

    private IEnumerator Boost()
    {
        rotationSpeed *= 3;
        boostButton.interactable = false;
        yield return new WaitForSeconds(4f);
        rotationSpeed /= 3;
        yield return new WaitForSeconds(10f);
        boostButton.interactable = true;
    }
}

