using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    //Properties
    int[] peg1 = { 1, 2, 3, 4, 5, 6, 7, 8 };
    int[] peg2 = { 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] peg3 = { 0, 0, 0, 0, 0, 0, 0, 0 };

    int currentPeg = 1;


    //Methods
    int[] GetPeg(int peg)
    {
        if (peg == 1) 
        {
            return peg1;
        }
        else if (peg == 2)
        {
            return peg2;
        }
        else if (peg == 3)
        {
            return peg3;
        }
        else 
        {
            return null;
        }
    }

    [ContextMenu("Move Right")]
    void MoveRight()
    {
        //Get Lists we are working with
        int[] currentList = GetPeg(currentPeg);
        int[] targetList = GetPeg(currentPeg + 1);

        //Stops procedure if we didn't get a next target list, which stops it from going to a 4th array
        if (targetList == null) 
        {
            return;
        }

        //Get Top number index from the current list
        int fromIndex = GetTopNumberIndex(currentList);
        //Get the bottom most free index from the target list
        int toIndex = GetBottomNumberIndex(targetList);

        //Check that we are able to find a free spot in the target list
        if(toIndex == -1)
        {
            return;
        }

        //Check that the number we want to move does not break our rules
        if (CanMoveIntoPeg(currentList[fromIndex], currentList) == false)
        {
            return;

        }

        MoveIntoPeg(fromIndex, toIndex, currentList, targetList);

    }


    [ContextMenu("Move Left")]
    void MoveLeft() 
    {
        
    
    }

    int GetTopNumberIndex(int[] peg) 
    {
        for(int i = 0; i < peg.Length; i++)
        {
            //if value in peg for index is NOT 0
            if (peg[i] != 0) 
            {
                return i;
            }
        }


        //Will tell if they didn't find a number
        return -1;
    }


    int GetBottomNumberIndex(int[] peg) 
    {
        //Same as Getting the top number index, but going backwards
        for (int i = peg.Length - 1; i >= 0; i--)
        {
            //if value in peg for index is 0
            if (peg[i] == 0)
            {
                return i;
            }
        }


        //Will tell if they didn't find a number
        return -1;
    }

    bool CanMoveIntoPeg(int numberToMove, int[] peg) 
    {
        int bottomIndex = GetBottomNumberIndex(peg);

        //Checking if 
        if (bottomIndex == peg.Length - 1 && peg[peg.Length - 1] == 0)
        {
            return true;
        }

        int bottomPlus1 = bottomIndex + 1;
        return bottomPlus1 == 0;
    }

    void MoveIntoPeg(int fromIndex, int toIndex, int[] from, int[] to)
    {
        int numberToMove = from[fromIndex];
        from[fromIndex] = 0;
        to[fromIndex] = numberToMove;
    }

}
