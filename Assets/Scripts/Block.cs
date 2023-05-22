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

    const string SWORD_A_TAG = "ColorASword";
    const string SWORD_B_TAG = "ColorBSword";

    private void Start()
    {
        brokenLeftRb = brokenBlockLeft.GetComponent<Rigidbody>();
        brokenRightRb = brokenBlockRight.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Sword"))
        {          
            /*if (IsCorrect(other)) 
            {
                Hit();
                GameManager.Instance.AddScore();
            }
            else //wrong case
            {
                GameManager.Instance.HitWrongBlock();
            }*/

            // Break only if the threshold is greated otherwise the Block will be considered missed

            if(other.CompareTag(SWORD_A_TAG))
            {
                if(GameManager.Instance.isGreaterThanThreshold(Color.COLORA))
                {
                    Hit();
                    if (blockColor == Color.COLORA)
                        GameManager.Instance.AddScore();
                    else
                        GameManager.Instance.HitWrongBlock();
                }
            }
            else if(other.CompareTag(SWORD_B_TAG))
            {
                if (GameManager.Instance.isGreaterThanThreshold(Color.COLORB))
                {
                    Hit();
                    if (blockColor == Color.COLORB)
                        GameManager.Instance.AddScore();
                    else
                        GameManager.Instance.HitWrongBlock();
                }
            }

            
        }       
      
    }
   
   /* private bool IsCorrect(Collider other)
    {
        return (other.CompareTag(SWORD_A_TAG) && blockColor == Color.COLORA) || //Correct Case 1
                (other.CompareTag(SWORD_B_TAG) && blockColor == Color.COLORB); // Correct Case 2
    }*/
  

    public void Hit()
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