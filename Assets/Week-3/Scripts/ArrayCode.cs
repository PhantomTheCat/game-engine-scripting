using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayCode : MonoBehaviour
{
    //Properties and Arrays

        //SerializeField will force any private variable to be visible in the Unity Inspector (Usually hidden)
        //In this case, the private scoresArray is put in the Unity Inspector
    [SerializeField]
    int[][] scoresArray = new int[4][];
        //Default way of writing an array: "int[] scoresArray = new int[4]"
        //New way that includes 2 [], is to represent a 2 dimensional array

    //Now creating an array with the comma in the []
    [SerializeField]
    int[,] scoresArray2 = new int[3, 3];





    [ContextMenu("Execute If Test")]
    void Execute() 
    {
        //Putting arrays into this array (2 Dimensional Array)
        scoresArray[0] = new int[4] { 1, 2, 3, 4 };
        scoresArray[1] = new int[4] { 5, 6, 7, 8 };
        scoresArray[2] = new int[4] { 9, 10, 11, 12 };
        scoresArray[3] = new int[4] { 13, 14, 15, 16 };


        //Previous Example
        /*for(int i = 0; i < scoresArray.Length; i++)
        {
            //LogFormat is same as string.Format
            Debug.LogFormat("The number is {0}", scoresArray[i]);
        }*/


        int numberofRows = scoresArray.GetLength(0);
        int numberofColumns = scoresArray.GetLength(1);

        //For each nested array in our array
        //Looking at first layer
        for (int i = 0; i < numberofRows; i++)
        {
            //Looking at each nested array (2nd layer/dimension)
            for (int j = 0; j < numberofColumns; j++)
            {
                Debug.LogFormat("The number is {0}", scoresArray[i][j]);
            }
        }


    }

}
