using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.VisionOS;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.Rendering;
using Unity.VisualScripting;



public class PlayerControllerRed : MonoBehaviour
{
    [SerializeField] PointsHUD movements;
    [SerializeField] TMP_Text TurnText;
    public static PlayerControllerRed Instance;
    public Transform player;
    public Material highlightMaterial;
    public  Material regularMaterial;
    public  Material redMaterial;
    public  Material blueMaterial;
    public Material highlightMaterialRed;
    public Material highlightMaterialBlue;
    public bool walking = false;
    public bool RedisPlaying = false;
    public GameObject blurEffect;
    public Animator playerAnimator;

    
    public bool FrontWallRed = false;
    public GameObject tutorialRotacio;
  
    
    public CharacterController _characterController;

    [Space]

    public GameObject paret;
    public GameObject RedWall;
    public Animator animatorRedWall;
    public Transform currentCube;
    public Transform clickedCube;
    public Transform targetPosition;
    public float speed = 0.5f;
    public Transform nextCube;
    WalkPath finalCurrentCube;
    // public Transform indicator;

    [Space]

    public List<Transform> finalPath = new List<Transform>();

    private float blend;

    [Space]

    public Transform startPoint;

    public Transform endPoint;

    

    [Range(0, 10)] public float lerpSpeed;

    public float moveValue;
    public bool MoveTarget = false;

    Vector3 velocity = new Vector3(0, 0, 0);

    void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (MoveTarget)
        {
            playerAnimator.SetBool("Jump", true);

            player.transform.position = Vector3.SmoothDamp(startPoint.position, endPoint.position + transform.up*0.70f, ref velocity, Time.deltaTime * lerpSpeed);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

   void Update()
    {
        if (Input.GetMouseButtonDown(0) && RedisPlaying)
        {
            Debug.Log("input");
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                clickedCube = mouseHit.transform;

                foreach (WalkPath Possiblepath in currentCube.GetComponent<Walkable>().possiblePaths)
                {
                    Debug.Log("WalkPath");
                    if (Possiblepath.target == clickedCube && !Possiblepath.cube.GetComponent<Walkable>().isOccupied)
                    {

                        Debug.Log("Findpath in ");
                        currentCube.GetComponent<Walkable>().isOccupied = false;

                        FindPath();

                        movements.Movements -= 1;

                        if (GameManager.Instance.BlueWon == true && GameManager.Instance.RedWon == false)
                        {
                            Debug.Log("BlueWon");

                            GameManager.Instance.UpdateGameState(GameState.RedTurn);

                        }
                        else if (GameManager.Instance.RedWon == true && GameManager.Instance.BlueWon == true)
                        {
                            Debug.Log("Victory");
                            GameManager.Instance.UpdateGameState(GameState.Victory);
                        }
                        else
                        {
                            GameManager.Instance.UpdateGameState(GameState.BlueTurn);
                            RedisPlaying = false;
                        }


                    }
                }
            }
        }

    }
   
    public void MoveRed()
    {
        
    }

    public  void ChooseTileRed()
    {
        TurnText.text = "Red".ToString();
        Debug.Log("Red is going to play");
       
        RedisPlaying = true;
        RayCastDown();

    }

   
    public void RayCastDown()
    {
        Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentCube = playerHit.transform;
                Walkable CubeWalkable = playerHit.transform.GetComponent<Walkable>();
                

                CubeWalkable.MakeOccupied(CubeWalkable);

            }
        }

        if (GameManager.Instance.BlueOpen == false && currentCube.GetComponent<Walkable>().FrontWallRed == true)
        {


            currentCube.GetComponent<Walkable>().possiblePaths[2].cube.GetComponent<Walkable>().isOccupied = true;

        }
        if (GameManager.Instance.BlueOpen == true && currentCube.GetComponent<Walkable>().FrontWallRed == true)
        {
            //currentCubeBlue.GetComponent<Walkable>().possiblePaths[2].active = false;

            currentCube.GetComponent<Walkable>().possiblePaths[2].cube.GetComponent<Walkable>().isOccupied = false;

        }




        foreach (WalkPath Possiblepath in currentCube.GetComponent<Walkable>().possiblePaths)
        {
            if (!Possiblepath.cube.GetComponent<Walkable>().isOccupied)
            {
                //Possibles caselles
                Debug.Log(Possiblepath.target);

                //fa be el debug del material
                Debug.Log(Possiblepath.cube.GetComponent<MeshRenderer>().sharedMaterial);

                //Sempre entra nomes al primer bucle

                
                if (Possiblepath.cube.GetComponent<MeshRenderer>().sharedMaterial == regularMaterial)
                   {
                    Debug.Log("regular");
                    Possiblepath.cube.GetComponent<MeshRenderer>().sharedMaterial = highlightMaterial;
                 }
                else if (Possiblepath.cube.GetComponent<MeshRenderer>().sharedMaterial == redMaterial)
                {
                    //    Debug.Log("rojoH");

                    Possiblepath.cube.GetComponent<MeshRenderer>().sharedMaterial = highlightMaterialRed;
                }
                else if (Possiblepath.cube.GetComponent<MeshRenderer>().sharedMaterial == blueMaterial)
                {
                        Debug.Log("blueH");

                    Possiblepath.cube.GetComponent<MeshRenderer>().sharedMaterial = highlightMaterialBlue;
                }



            }


        }
    }

    void FindPath()
    {
        Debug.Log("Find");
        List<Transform> nextCubes = new List<Transform>();
        List <Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in currentCube.GetComponent<Walkable>().possiblePaths)
        {
          
            if (path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock =currentCube;
                if (path.cube.GetComponent<Walkable>().finalRed)
                {
                    path.cube.GetComponent<MeshRenderer>().material = redMaterial;
                }
                else if (path.cube.GetComponent<Walkable>().finalBlue)
                {
                    path.cube.GetComponent<MeshRenderer>().material = blueMaterial;
                }
                
                else
                {
                    path.cube.GetComponent<MeshRenderer>().material = regularMaterial;
                }
                
               

            }
        }

        pastCubes.Add(currentCube);

        ExploreCube(nextCubes, pastCubes);
        BuildPath();
        
        

    }

    void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
    {
        Debug.Log("Explore");
        Transform current = nextCubes.First();
        nextCubes.Remove(current);

        if (current == clickedCube)
        {
            return;
        }

        foreach (WalkPath path in current.GetComponent<Walkable>().possiblePaths)
        {
            if(!visitedCubes.Contains(path.target) && path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = current;

            }

        }
        visitedCubes.Add(current);

        if (nextCubes.Any())
        {
            ExploreCube(nextCubes, visitedCubes);
        }

    }

    void BuildPath()
    {
        Debug.Log("Build");
        Transform cube = clickedCube;

        while (cube != currentCube)
        {
            finalPath.Add(cube);
            if (cube.GetComponent<Walkable>().previousBlock != null)
            {
                cube = cube.GetComponent<Walkable>().previousBlock;
            }
            else
            {
                return;
            }
        }

        FollowPath();
    }

    void FollowPath()
    {

        walking = true;

        for (int i = finalPath.Count - 1; i >= 0; --i)
        {
            Debug.Log("Follow");

            startPoint = player ;
            endPoint= finalPath[i].GetComponent<Walkable>().transform;
            
            

   
            movePlayer();
 


            Debug.Log(finalPath[i].GetComponent<Walkable>().transform.position);

            currentCube = finalPath[i];

            if (currentCube.GetComponent<Walkable>().finalRed == true)
            {
                GameManager.Instance.RedWon = true;
            }

            if (GameManager.Instance.RedWon == true && GameManager.Instance.BlueWon == true)
            {
                Debug.Log("victory");
                GameManager.Instance.UpdateGameState(GameState.Victory);

            }
            if (movements.Movements <= 1)
            {
                GameManager.Instance.UpdateGameState(GameState.Lose);
            }
            //puja la paret
            if (currentCube.GetComponent<Walkable>().isButton == true)
            {
                
                tutorialRotacio.SetActive(true);
                blurEffect.SetActive(true);
            }
            if (currentCube.GetComponent<Walkable>().isButtonRed == true)
            {
                animatorRedWall.SetBool("Down", true);
                GameManager.Instance.RedOpen = true;
               
            }
           
            if (currentCube.GetComponent<Walkable>().isButtonRed == false)
            {
                animatorRedWall.SetBool("Down", false);
                GameManager.Instance.RedOpen = false;

            }
            currentCube.GetComponent<Walkable>().isOccupied = true;

            //currentCube.GetComponent<Walkable>().possiblePaths.Clear();
            Debug.Log(player.transform.position);


        }

        finalPath.Clear();

       
    }

    private void movePlayer()
    {
        //player.transform.LookAt(endPoint);
        MoveTarget = true;
    }



    //void Clear()
    //{
    //    foreach (Transform t in finalPath)
    //    {
    //        s.GetComponent<Walkable>.previousBlock = null;
    //    }
    //    finalPath.Clear();
    //    walking = false;
    //}


}
