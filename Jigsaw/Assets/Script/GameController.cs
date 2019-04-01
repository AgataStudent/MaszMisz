using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int level;
    public int row, col, countStep; 
    public int rowBlank, colBlank;//pozycja pustego kafelka
    public int sizeRow,sizeCol;
    int countPoint = 0;
    int countImageKey = 0;
    int countComplete = 0;

    public bool startControl = false;
    public bool checkComplete;
    public bool gameIsComplete;

    GameObject temp;

    public List<GameObject> imageKeyList;
    public List<GameObject> imageOfPictueList;
    public List<GameObject> checkPointList;

    GameObject[,] imageKeyMatrix;
    GameObject[,] imageOfPictureMatrix;
    GameObject[,] checkPointMatrix;


    // Start is called before the first frame update
    void Start()
    {
        imageKeyMatrix = new GameObject[sizeRow, sizeCol];
        imageOfPictureMatrix = new GameObject[sizeRow, sizeCol];
        checkPointMatrix = new GameObject[sizeRow, sizeCol];
        if (level == 1)
        {
            ImageOfEasyLevel();
        }

        CheckPointManager();
        ImageKeyManager();

        for (int r = 0; r < sizeRow; r++) //rząd
        {
            for (int c = 0; c < sizeCol; c++) //kolumna
            {
                if (imageOfPictureMatrix[r, c].name.CompareTo("blank") == 0)
                {
                    rowBlank = r;
                    colBlank = c;
                    break;
                }
            }
        }
    }
   
    void CheckPointManager()
    {
        for (int r = 0; r < sizeRow; r++) //rząd
        {
            for (int c = 0; c < sizeCol; c++) //kolumna
            {
                checkPointMatrix[r, c] = checkPointList[countPoint];
                countPoint++;
            }
        }
    }
    void ImageKeyManager()
    {
        for (int r = 0; r < sizeRow; r++) //rząd
        {
            for (int c = 0; c < sizeCol; c++) //kolumna
            {
                imageKeyMatrix[r, c] = imageKeyList[countImageKey];
                countPoint++;
            }
        }

    }

    // Update is called once per frame
    void Update() 
    {
        if (startControl) //ruch kafelka
        {
            startControl = false;
            if (countStep == 1)
            {
                if(imageOfPictureMatrix[row,col]!=null && imageOfPictureMatrix[row,col].name.CompareTo("blank")!=0) // sprawdza czy obrazek nie jest "blank"
                {
                    if (rowBlank!=row && colBlank == col)
                    {
                        if (Mathf.Abs(row - rowBlank) == 1)
                        {
                            SortImage();
                            countStep = 0;
                        }
                        else
                        {
                            countStep = 0;
                        }
                    }
                    else if (rowBlank==row && colBlank != col)
                    {
                        if (Mathf.Abs(col - colBlank) == 1)
                        {
                            SortImage();
                            countStep = 0;
                        }
                        else
                        {
                            countStep = 0;
                        }
                    }
                    else if ((rowBlank == row && colBlank == col) || (rowBlank != row && colBlank != col))
                    {
                        countStep = 0;
                    }
                }
                else
                {
                    countStep = 0;
                }
            }
            
        }
    }
    private void FixedUpdate()
    {
        if (checkComplete)
        {
            checkComplete = false;
            for(int r = 0; r < sizeRow; r++)
            {
                for(int c = 0; c < sizeCol; c++)
                {
                    if (imageKeyMatrix[r, c].gameObject.name.CompareTo(imageOfPictureMatrix[r, c].gameObject.name) == 0)
                    {
                        countComplete++;
                    }
                    else
                    {

                        break;
                    }
                }
            }
            if (countComplete == checkPointList.Count)
            {
                gameIsComplete = true;
                Debug.Log("You win");
            }
            else
            {
                countComplete = 0;
            }
        }
    }

    void SortImage()
    {
        temp = imageOfPictureMatrix[rowBlank, colBlank];
        imageOfPictureMatrix[rowBlank, colBlank] = null;

        imageOfPictureMatrix[rowBlank, colBlank] = imageOfPictureMatrix[row, col];
        imageOfPictureMatrix[row, col] = null;

        imageOfPictureMatrix[row, col] = temp;

        imageOfPictureMatrix[rowBlank, colBlank].GetComponent<ImageController>().target = checkPointMatrix[rowBlank, colBlank];
        imageOfPictureMatrix[row, col].GetComponent<ImageController>().target = checkPointMatrix[row, col];

        imageOfPictureMatrix[rowBlank, colBlank].GetComponent<ImageController>().startMove = true;
        imageOfPictureMatrix[row, col].GetComponent<ImageController>().startMove = true;

        rowBlank = row;
        colBlank = col;
    }
    void ImageOfEasyLevel()
    {
        imageOfPictureMatrix[0, 0] = imageOfPictueList[0];  //rząd1
        imageOfPictureMatrix[0, 1] = imageOfPictueList[2];
        imageOfPictureMatrix[0, 2] = imageOfPictueList[5];
        imageOfPictureMatrix[1, 0] = imageOfPictueList[4];  //rząd2
        imageOfPictureMatrix[1, 1] = imageOfPictueList[1];
        imageOfPictureMatrix[1, 2] = imageOfPictueList[7];
        imageOfPictureMatrix[2, 0] = imageOfPictueList[3];  //rząd3
        imageOfPictureMatrix[2, 1] = imageOfPictueList[6];
        imageOfPictureMatrix[2, 2] = imageOfPictueList[8];
    }

}
