using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VisionOS;
using UnityEngine;
using TMPro;


public class PlayerControllerBlue : MonoBehaviour
{
    [SerializeField] PointsHUD movements;
    [SerializeField] TMP_Text TurnText;
    public static PlayerControllerBlue Instance;
    public GameObject player;
    public Material highlightMaterial;
    public Material regularMaterial;
    public bool walking = false;
    public bool BlueisPlaying = false;

    [Space]

    public Transform currentCubeBlue;
    public Transform clickedCubeBlue;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && BlueisPlaying)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                clickedCubeBlue = mouseHit.transform;

                foreach (WalkPath Possiblepath in currentCubeBlue.GetComponent<Walkable>().possiblePaths)
                {
                    if (Possiblepath.target == clickedCubeBlue)
                    {

                        FindPath();
                        movements.Movements -= 1;

                        GameManager.Instance.UpdateGameState(GameState.RedTurn);
                        BlueisPlaying = false;
                    }
                }


            }
        }
    }

    public void MoveBlue()
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
        RaycastHit playerHit;

        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentCubeBlue = playerHit.transform;
            }
        }

        foreach (WalkPath Possiblepath in currentCubeBlue.GetComponent<Walkable>().possiblePaths)
        {
            //Possibles caselles
            Debug.Log(Possiblepath.target);
            Possiblepath.cube.GetComponent<MeshRenderer>().material = highlightMaterial;

        }
    }

    void FindPath()
    {
        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in currentCubeBlue.GetComponent<Walkable>().possiblePaths)
        {
            if (path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = currentCubeBlue;
                path.cube.GetComponent<MeshRenderer>().material = regularMaterial;

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

        walking = true;

        for (int i = finalPath.Count - 1; i >= 0; --i)
        {
            player.transform.position = finalPath[i].GetComponent<Walkable>().transform.position + transform.up * 1f;
            currentCubeBlue = finalPath[i];
        }

        finalPath.Clear();

    
        GameManager.Instance.UpdateGameState(GameState.RedTurn);
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
