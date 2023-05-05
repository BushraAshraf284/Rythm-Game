using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Color blockColor;
    public GameObject brokenBlockLeft;
    public GameObject brokenBlockRight;
    public float brokenBlockForce;
    public float brokenBlockTorque;
    public float brokenBlockDestroyDelay;

    Rigidbody brokenLeftRb;
    Rigidbody brokenRightRb;

    private void Start()
    {
        brokenLeftRb = brokenBlockLeft.GetComponent<Rigidbody>();
        brokenRightRb = brokenBlockRight.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Sword"))
        {
            Hit();
            if (other.CompareTag("ColorASword") && blockColor == Color.COLORA) //Correct Case 1
            {
                
            }
            else if (other.CompareTag("ColorBSword") && blockColor == Color.COLORB) // Correct Case 2
            {

            }
            else //wrong case
            {

            }
        }
       
      
    }

  

    private void Hit()
    {
        //enabling broken pieces
        brokenBlockRight.SetActive(true);
        brokenBlockLeft.SetActive(true);

        //Removing parent 
        brokenBlockRight.transform.parent = null;
        brokenBlockLeft.transform.parent = null;

        //Applying forces on opposite direction
        brokenLeftRb.AddForce(-transform.right * brokenBlockForce, ForceMode.Impulse);
        brokenRightRb.AddForce(transform.right * brokenBlockForce, ForceMode.Impulse);

        //Applying Torque to give rotation effect

        brokenLeftRb.AddTorque(-transform.forward * brokenBlockTorque, ForceMode.Impulse);
        brokenRightRb.AddTorque(transform.forward * brokenBlockTorque, ForceMode.Impulse);

        Destroy(brokenBlockRight, brokenBlockDestroyDelay);
        Destroy(brokenBlockLeft, brokenBlockDestroyDelay);

        Destroy(gameObject);


    }
}

public enum Color
{
    COLORA,
    COLORB
}