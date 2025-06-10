using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.VisionOS;
using UnityEngine;
using TMPro;
using System;


public class PlayerControllerBlue : MonoBehaviour
{
    [SerializeField] PointsHUD movements;
    [SerializeField] TMP_Text TurnText;
    public static PlayerControllerBlue Instance;
    public PlayerControllerRed red;
    public Transform player;
    public Material highlightMaterial;
    public Material regularMaterial;
    public Material blueMaterial;
    public Material redMaterial;
    public Material highlightMaterialRed;
    public Material highlightMaterialBlue;
    public bool walking = false;
    public bool BlueisPlaying = false;
    public GameObject blurEffect;

    public Walkable CubeWalkable;

    public GameObject paret;
    public GameObject BlueWall;
    public GameObject tutorialRotacio;
    public Animator animatorBlueWall;
    public bool FrontWallBlue = false;
    public Animator animatorParet;

    [Space]

    public Transform currentCubeBlue;
    public Transform clickedCubeBlue;
    // public Transform indicator;
    public WalkPath finalCube;

    [Space]

    public List<Transform> finalPath = new List<Transform>();

    private float blend;
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

    // Start is called before the first frame update
    void Start()
    {
        animatorParet = paret.GetComponent<Animator>();
        animatorBlueWall = BlueWall.GetComponent<Animator>();
        animatorParet.SetBool("pujarParet", false);
    }
    private void FixedUpdate()
    {
        if (MoveTarget)
        {
            //playerAnimator.SetBool("Jump", true);

            player.transform.position = Vector3.SmoothDamp(startPoint.position, endPoint.position + transform.up * 0.80f, ref velocity, Time.deltaTime * lerpSpeed);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && BlueisPlaying)
        {
            
            Debug.Log("input blue");
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                clickedCubeBlue = mouseHit.transform;

                foreach (WalkPath Possiblepath in currentCubeBlue.GetComponent<Walkable>().possiblePaths)
                {
        
                    
                        if (Possiblepath.target == clickedCubeBlue && !Possiblepath.cube.GetComponent<Walkable>().isOccupied)
                        {

                            currentCubeBlue.GetComponent<Walkable>().isOccupied = false;
                            FindPath();
                            movements.Movements -= 1;

                            if (GameManager.Instance.RedWon == true && GameManager.Instance.BlueWon == false)
                            {
                                Debug.Log("RedWon");
                                GameManager.Instance.UpdateGameState(GameState.BlueTurn);

                            }
                            else if (GameManager.Instance.RedWon == true && GameManager.Instance.BlueWon == true)
                                {
                                Debug.Log("Victoryy");
                                GameManager.Instance.UpdateGameState(GameState.Victory);

                                 }
                            else 
                                {
                                    GameManager.Instance.UpdateGameState(GameState.RedTurn);
                                    BlueisPlaying = false;
                                }
                        }
                    }
                
            }


            
        }
    }

    public void MoveBlue()
    {
        
    }

    public void FinishGame()
    {

    }
    public void ChooseTileBlue()
    {
        TurnText.text = "Blue".ToString();
        Debug.Log("Blue is going to play");
        //clickedCubeBlue = null;
        BlueisPlaying = true;
        
        RayCastDown();


    }


    public void RayCastDown()
    {
        Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);
        Ray playerRayfwd = new Ray(transform.GetChild(0).position, transform.forward);

        RaycastHit playerHit;

        if (Physics.Raycast(playerRayfwd, out playerHit))
        {
            if(playerHit.transform.tag == "paret")
            {
                Debug.Log("Wall detected");
            }
        }


            if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null && GameManager.Instance.RedWon == false)
            {
                currentCubeBlue = playerHit.transform;
                CubeWalkable = playerHit.transform.GetComponent<Walkable>();
                Debug.Log("currentCube=" + currentCubeBlue);

                //CubeWalkable.MakeOccupied2(CubeWalkable);

            }
            else if (playerHit.transform.GetComponent<Walkable>() != null && GameManager.Instance.RedWon == true)
            {
                currentCubeBlue = currentCubeBlue.transform;
                CubeWalkable = playerHit.transform.GetComponent<Walkable>();
                Debug.Log("currentCube=" + currentCubeBlue);

                //CubeWalkable.MakeOccupied2(CubeWalkable);

            }
        }
        if (GameManager.Instance.RedOpen == false && currentCubeBlue.GetComponent<Walkable>().FrontWallBlue == true)
        {
            

            currentCubeBlue.GetComponent<Walkable>().possiblePaths[2].cube.GetComponent<Walkable>().isOccupied = true;

        }
        if (GameManager.Instance.RedOpen == true && currentCubeBlue.GetComponent<Walkable>().FrontWallBlue == true)
        {
            //currentCubeBlue.GetComponent<Walkable>().possiblePaths[2].active = false;

            currentCubeBlue.GetComponent<Walkable>().possiblePaths[2].cube.GetComponent<Walkable>().isOccupied = false;

        }

        foreach (WalkPath Possiblepath in currentCubeBlue.GetComponent<Walkable>().possiblePaths)
        {
            if (!Possiblepath.cube.GetComponent<Walkable>().isOccupied)
            {
                //Possibles caselles
                Debug.Log(Possiblepath.target);
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
        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in currentCubeBlue.GetComponent<Walkable>().possiblePaths)
        {
            if (path.active && !path.occupied)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = currentCubeBlue;

                if (path.cube.GetComponent<Walkable>().finalBlue)
                {
                   path.cube.GetComponent <MeshRenderer>().material = blueMaterial;
                }
                else if (path.cube.GetComponent<Walkable>().finalRed)
                {
                    path.cube.GetComponent<MeshRenderer>().material = redMaterial;
                }
                else
                {
                    path.cube.GetComponent<MeshRenderer>().material = regularMaterial;
                }
                

            }
        }

        pastCubes.Add(currentCubeBlue);

        ExploreCube(nextCubes, pastCubes);
        BuildPath();
       


    }

    void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
    {
        Transform current = nextCubes.First();
        nextCubes.Remove(current);

        if (current == clickedCubeBlue)
        {
            return;
        }

        foreach (WalkPath path in current.GetComponent<Walkable>().possiblePaths)
        {
            if (!visitedCubes.Contains(path.target) && path.active)
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
        Transform cube = clickedCubeBlue;

        while (cube != currentCubeBlue)
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
        Debug.Log("following path blue");

        walking = true;

        for (int i = finalPath.Count - 1; i >= 0; --i)
        {
            startPoint = player;
            endPoint = finalPath[i].GetComponent<Walkable>().transform;




            movePlayer();

            Debug.Log(finalPath[i].GetComponent<Walkable>().transform.position);

            currentCubeBlue = finalPath[i];

            if (currentCubeBlue.GetComponent<Walkable>().finalBlue == true)
            {
                GameManager.Instance.BlueWon = true;
            }

            currentCubeBlue.GetComponent<Walkable>().isOccupied = true;
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
        if (currentCubeBlue.GetComponent<Walkable>().isButton == true)
        {
            animatorParet.SetBool("pujarParet", true);
            tutorialRotacio.SetActive(true);
            blurEffect.SetActive(true);
        }

        if (currentCubeBlue.GetComponent<Walkable>().isButtonBlue == true)
        {
            animatorBlueWall.SetBool("BlueDown", true);
            GameManager.Instance.BlueOpen = true;

        }

       
        if (currentCubeBlue.GetComponent<Walkable>().isButtonBlue == false)
        {
            animatorBlueWall.SetBool("BlueDown", false);
            GameManager.Instance.BlueOpen = false;

        }
        currentCubeBlue.GetComponent<Walkable>().isOccupied = true;


        finalPath.Clear();
       
    
        //GameManager.Instance.UpdateGameState(GameState.RedTurn);
    }

    private void movePlayer()
    {
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
