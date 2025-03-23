using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VisionOS;
using UnityEngine;
using TMPro;

public class PlayerControllerRed : MonoBehaviour
{
    [SerializeField] PointsHUD movements;
    [SerializeField] TMP_Text TurnText;
    public static PlayerControllerRed Instance;
    public GameObject player;
    public Material highlightMaterial;
    public Material regularMaterial;
    public bool walking = false;
    public bool RedisPlaying = false;

    [Space]

    public Transform currentCube;
    public Transform clickedCube;
    WalkPath finalCurrentCube;
    // public Transform indicator;

    [Space]

    public List<Transform> finalPath = new List<Transform>();

    private float blend;

    void Awake()
    {
        Instance = this;
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
                    if (Possiblepath.target == clickedCube)
                    {

                        Debug.Log("Findpath in ");

                        FindPath();

                        movements.Movements -= 1;

                        GameManager.Instance.UpdateGameState(GameState.BlueTurn);
                        RedisPlaying = false;
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
            }
        }

        foreach (WalkPath Possiblepath in currentCube.GetComponent<Walkable>().possiblePaths)
        {
            //Possibles caselles
            Debug.Log(Possiblepath.target);
            Possiblepath.cube.GetComponent<MeshRenderer>().material = highlightMaterial;

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
                path.cube.GetComponent<MeshRenderer>().material = regularMaterial;

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
            player.transform.position = finalPath[i].GetComponent<Walkable>().transform.position  + transform.up *1f;
            Debug.Log(finalPath[i].GetComponent<Walkable>().transform.position);

            currentCube = finalPath[i];
         
            //currentCube.GetComponent<Walkable>().possiblePaths.Clear();
            Debug.Log(player.transform.position);
        }

        finalPath.Clear();

       
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
