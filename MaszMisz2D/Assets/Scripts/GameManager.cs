﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Puzzle puzzlePrefab;

    public int spawn = 14;
    public int lvl = 1;
    public int material = 3;
    

    private List<Puzzle> puzzleList = new List<Puzzle>();

    public Vector2 startPosition = new Vector2(-3.55f, 1.77f);
    public Vector2 offset = new Vector2(2.03f, 1.52f);


    public LayerMask collisionMask;

    Ray ray_up, ray_down, ray_left, ray_right;
    RaycastHit hit;
    private BoxCollider collider;
    private Vector3 collider_size;
    private Vector3 collider_centre;

    public static string FolderName;

    public GameObject FullPicture;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPuzzle(spawn);
        SetStartPosition();
        ApplyMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        MovePuzzle();
    }

    private void SpawnPuzzle(int number)
    {
        for (int i=0; i <= number; i++)
        {
            puzzleList.Add(Instantiate(puzzlePrefab, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 180.0f, 0.0f)) as Puzzle);
        }
    }

    void SetStartPosition()
    {
        if (lvl == 1)
        { 

        puzzleList[0].transform.position = new Vector3(startPosition.x, startPosition.y, 0.0f);
        puzzleList[1].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y, 0.0f);
        puzzleList[2].transform.position = new Vector3(startPosition.x + (2*offset.x), startPosition.y, 0.0f);

        puzzleList[3].transform.position = new Vector3(startPosition.x, startPosition.y - offset.y, 0.0f);
        puzzleList[4].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y - offset.y, 0.0f);
        puzzleList[5].transform.position = new Vector3(startPosition.x + (2*offset.x), startPosition.y - offset.y, 0.0f);
        puzzleList[6].transform.position = new Vector3(startPosition.x + (3*offset.x), startPosition.y - offset.y, 0.0f);

        puzzleList[7].transform.position = new Vector3(startPosition.x, startPosition.y - (2*offset.y), 0.0f);
        puzzleList[8].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y - (2 * offset.y), 0.0f);
        puzzleList[9].transform.position = new Vector3(startPosition.x + (2*offset.x), startPosition.y - (2 * offset.y), 0.0f);
        puzzleList[10].transform.position = new Vector3(startPosition.x + (3*offset.x), startPosition.y - (2 * offset.y), 0.0f);

        puzzleList[11].transform.position = new Vector3(startPosition.x, startPosition.y - (3*offset.y), 0.0f);
        puzzleList[12].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y - (3 * offset.y), 0.0f);
        puzzleList[13].transform.position = new Vector3(startPosition.x + (2*offset.x), startPosition.y - (3 * offset.y), 0.0f);
        puzzleList[14].transform.position = new Vector3(startPosition.x + (3*offset.x), startPosition.y - (3 * offset.y), 0.0f);

        }
        else 
        {
            puzzleList[0].transform.position = new Vector3(startPosition.x, startPosition.y, 0.0f);
            puzzleList[1].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y, 0.0f);

            puzzleList[2].transform.position = new Vector3(startPosition.x, startPosition.y - offset.y, 0.0f);
            puzzleList[3].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y - offset.y, 0.0f);
            puzzleList[4].transform.position = new Vector3(startPosition.x + (2 * offset.x), startPosition.y - offset.y, 0.0f);

            puzzleList[5].transform.position = new Vector3(startPosition.x, startPosition.y - (2 * offset.y), 0.0f);
            puzzleList[6].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y - (2 * offset.y), 0.0f);
            puzzleList[7].transform.position = new Vector3(startPosition.x + (2 * offset.x), startPosition.y - (2 * offset.y), 0.0f);



        }
    }

    void MovePuzzle()
    {
        foreach (Puzzle puzzle in puzzleList)
        {
            puzzle.move_amount = offset;

            if(puzzle.clicked)
            {
                collider = puzzle.GetComponent<BoxCollider>();
                collider_size = collider.size;
                collider_centre = collider.center;

                float move_amount = offset.x;
                float direction = Mathf.Sign(move_amount);

                float x = (puzzle.transform.position.x + collider_centre.x - collider_size.x / 2) + collider_size.x / 2;
                float y_up = puzzle.transform.position.y + collider_centre.y + collider_size.y / 2 * direction;
                float y_down = puzzle.transform.position.y + collider_centre.y + collider_size.y / 2 * -direction;

                ray_up = new Ray(new Vector2(x, y_up), new Vector2(0, direction));
                ray_down = new Ray(new Vector2(x, y_down), new Vector2(0, -direction));

                Debug.DrawRay(ray_up.origin, ray_up.direction);
                Debug.DrawRay(ray_down.origin, ray_down.direction);


                float y = (puzzle.transform.position.y + collider_centre.y - collider_size.y / 2) + collider_size.y / 2;
                float x_right = puzzle.transform.position.x + collider_centre.x + collider_size.x / 2 * direction;
                float x_left = puzzle.transform.position.x + collider_centre.x + collider_size.x / 2 * -direction;

                ray_left = new Ray(new Vector2(x_left, y), new Vector2(-direction, 0f));
                ray_right = new Ray(new Vector2(x_right, y), new Vector2(direction, 0f));

                Debug.DrawRay(ray_left.origin, ray_left.direction);
                Debug.DrawRay(ray_right.origin, ray_right.direction);

                if((Physics.Raycast(ray_up, out hit, 1.0f, collisionMask) == false) && (puzzle.moved == false) && (puzzle.transform.position.y < startPosition.y))
                    {
                    Debug.Log("Move Up Allowed");
                    puzzle.go_up = true;
                    }

                if((Physics.Raycast(ray_down, out hit, 1.0f, collisionMask) == false) && (puzzle.moved == false) && (puzzle.transform.position.y > (startPosition.y - material * offset.y)))
                    {
                    Debug.Log("Move Down Allowed");
                    puzzle.go_down = true;
                    }

                if ((Physics.Raycast(ray_left, out hit, 1.0f, collisionMask) == false) && (puzzle.moved == false) && (puzzle.transform.position.x > startPosition.x))
                    {
                    Debug.Log("Move Left Allowed");
                    puzzle.go_left = true;
                    }

                if ((Physics.Raycast(ray_right, out hit, 1.0f, collisionMask) == false) && (puzzle.moved == false) && (puzzle.transform.position.x < (startPosition.x + material * offset.x)))
                    {
                    Debug.Log("Move Right Allowed");
                    puzzle.go_right = true;
                    }


            }
        }

    }

    void ApplyMaterial()
    {
        string filePath;
        for (int i=1; i <= puzzleList.Count; i++)
        {
            if (i > material)
                filePath = "Puzzles/" + FolderName + "/Cube" + (i + 1);
            else
                filePath = "Puzzles/" + FolderName + "/Cube" + i;

            Texture2D mat = Resources.Load(filePath, typeof(Texture2D)) as Texture2D;
            puzzleList[i - 1].GetComponent<Renderer>().material.mainTexture = mat;
        }

        filePath = "Puzzles/" + FolderName + "/pic";
        Texture2D mat1 = Resources.Load(filePath, typeof(Texture2D)) as Texture2D;
        FullPicture.GetComponent<Renderer>().material.mainTexture = mat1;

    }



}













































