using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotator : MonoBehaviour
{
    public GameObject board;

    float yRotation = 90.0f;
    float yRotation_ = -90.0f;

    public GameObject leftArrow;
    public GameObject leftArrowPosition;
    public Animator animator;
    public Animator camaraAnimator;

   


    // Start is called before the first frame update
    void Start()
    {
        leftArrow.SetActive(false);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (board.transform.eulerAngles.y == 0)
            {
                camaraAnimator.SetBool("RotationCamara", true);
                animator.SetBool("ReturnRight", false);
                animator.SetBool("RotateRight", false);
                animator.SetBool("RotateLeft", true);
               
                board.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
                // Instantiate(leftArrow, leftArrowPosition.transform.position, Quaternion.identity);
                leftArrow.SetActive(true);
                //camaraAnimator.SetBool("RotationCamara", false);

            }
            else if (board.transform.eulerAngles.y == 270)
            {
                animator.SetBool("RotateRight", false);
                animator.SetBool("ReturnLeft", true);
                Debug.Log("-90");
                board.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                leftArrow.SetActive(true);
            }

            else
            {

                leftArrow.SetActive(true);
            }
        }

        if (Input.GetKeyDown("e"))
        {
            if (board.transform.eulerAngles.y == 90)
            {
                animator.SetBool("RotateLeft", false);
                animator.SetBool("ReturnLeft", false);
                animator.SetBool("ReturnRight", true);
                Debug.Log("Hola");
                board.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                Debug.Log(board.transform.eulerAngles.y);
            }
            else
            {
                animator.SetBool("RotateRight", true);
                animator.SetBool("ReturnLeft", false);
                animator.SetBool("ReturnRight", false);
                board.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation_, transform.eulerAngles.z);
                Debug.Log(board.transform.eulerAngles.y);
            }

        }
    }

   

    public void FinishAnimation()
    {
        camaraAnimator.SetBool("RotationCamara", false);
    }
}

    

