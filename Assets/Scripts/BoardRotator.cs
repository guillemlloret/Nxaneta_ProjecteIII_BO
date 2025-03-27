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
                animator.SetBool("RotateLeft", true);
                board.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
                // Instantiate(leftArrow, leftArrowPosition.transform.position, Quaternion.identity);
                leftArrow.SetActive(true);

            }
            else if (board.transform.eulerAngles.y == 270)
            {
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
                Debug.Log("Hola");
                board.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                Debug.Log(board.transform.eulerAngles.y);
            }
            else
            {
                board.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation_, transform.eulerAngles.z);
                Debug.Log(board.transform.eulerAngles.y);
            }

        }
    }

    public void FinishAnimation()
    {
        leftArrow.SetActive(false);
    }
}

    

