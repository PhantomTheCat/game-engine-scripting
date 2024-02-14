using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    //Properties
    [SerializeField] private Transform peg1Transform;
    [SerializeField] private Transform peg2Transform;
    [SerializeField] private Transform peg3Transform;

    public TextMeshProUGUI currentPegText;
    public GameObject winText;

    [SerializeField] private int[] peg1 = { 1, 2, 3, 4 };
    [SerializeField] private int[] peg2 = { 0, 0, 0, 0 };
    [SerializeField] private int[] peg3 = { 0, 0, 0, 0 };

    [SerializeField] private int currentPeg = 1;


    //Methods
    [ContextMenu("Move Right")]
    public void MoveRight()
    {
        //Make sure we aren't the right most peg
        if (CanMoveRight() == false) return;

        //Check to see what index and number we are moving from THIS peg
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        //If there wasn't anything to move then don't try to move
        if (fromIndex == -1) return;

        //Check to see where in the peg we are moving to that the number
        //should be placed into
        int[] toArray = GetPeg(currentPeg + 1);
        int toIndex = GetIndexOfFreeSlot(toArray);

        //If the adjacent peg is FULL then we cannot move anything into it
        //This probably will never happen since the max number of numbers
        //we have is the size of each peg
        if (toIndex == -1) return;

        //Lastly check to verify the number we are moving is not larger
        //than whatever number we may be placing this number on top of
        //on the adjacent peg
        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;

        //If all checks PASS then go aheand and move the number
        //out of THIS array into the adjacent array
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        //Getting our disc to move and peg to move it to
        Transform disc = PopDiscFromCurrentPeg();
        Transform toPeg = GetPegTransform(currentPeg + 1);

        //Setting the disc to it's new peg parent (Will move it in interface)
        disc.SetParent(toPeg);

        //Checking if player won and setting winText as visible if they did
        bool playerWon = CheckIfPlayerWon(peg3);
        if (playerWon) winText.SetActive(true);
    }

    [ContextMenu("Move Left")]
    public void MoveLeft()
    {
        //Make sure we aren't the left most peg
        if (CanMoveLeft() == false) return;

        //Check to see what index and number we are moving from THIS peg
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        //If there wasn't anything to move then don't try to move
        if (fromIndex == -1) return;

        //Check to see where in the peg we are moving to that the number
        //should be placed into
        int[] toArray = GetPeg(currentPeg - 1);
        int toIndex = GetIndexOfFreeSlot(toArray);

        //If the adjacent peg is FULL then we cannot move anything into it
        //This probably will never happen since the max number of numbers
        //we have is the size of each peg
        if (toIndex == -1) return;

        //Lastly check to verify the number we are moving is not larger
        //than whatever number we may be placing this number on top of
        //on the adjacent peg
        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;

        //If all checks PASS then go aheand and move the number
        //out of THIS array into the adjacent array
        MoveNumber(fromArray, fromIndex, toArray, toIndex);


        //Getting our disc to move and peg to move it to
        Transform disc = PopDiscFromCurrentPeg();
        Transform toPeg = GetPegTransform(currentPeg - 1);

        //Setting the disc to it's new peg parent (Will move it in interface)
        disc.SetParent(toPeg);

        //Checking if player won and setting winText as visible if they did
        bool playerWon = CheckIfPlayerWon(peg3);
        if (playerWon) winText.SetActive(true);
    }

    public void IncrementPegNumber()
    {
        if (currentPeg != 3)
        {
            currentPeg++;
            UpdatePegText();
        }
    }

    public void DecrementPegNumber()
    {
        if (currentPeg != 1)
        {
            currentPeg--;
            UpdatePegText();
        }
    }

    public void UpdatePegText()
    {
        //Updating the text in Unity that tells user which peg they are on
        currentPegText.text = $"Your Current Peg is: {currentPeg}";
    }

    Transform PopDiscFromCurrentPeg()
    {
        //Getting our selected peg from the Unity interface
        Transform currentPegTransform = GetPegTransform(currentPeg);
        int index = currentPegTransform.childCount - 1;

        //Getting the child of that peg (The disk that we will be moving)
        Transform disk = currentPegTransform.GetChild(index);
        return disk;
    }

    Transform GetPegTransform(int pegNumber)
    {
        //Getting which peg we are transforming our disks too in Unity
        //Based on our current peg
        if (pegNumber == 1) return peg1Transform;

        if (pegNumber == 2) return peg2Transform;

        return peg3Transform;
    }

    void MoveNumber(int[] fromArr, int fromIndex, int[] toArr, int toIndex)
    {
        //Moving the number on top of one array to the top of another array
        //Logic found out for the values of these passed in variables earlier
        int value = fromArr[fromIndex];
        fromArr[fromIndex] = 0;

        toArr[toIndex] = value;
    }

    bool CanAddToPeg(int value, int[] peg)
    {
        //Checking if we can add to a peg
        int topNumberIndex = GetTopNumberIndex(peg);
        if (topNumberIndex == -1) return true;

        int topNumber = peg[topNumberIndex];
        return topNumber > value;
    }

    bool CanMoveRight()
    {
        //If peg 1 or 2 then can move right
        return currentPeg < 3;
    }

    bool CanMoveLeft()
    {
        //If peg 2 or 3 then can move right
        return currentPeg > 1;
    }

    int[] GetPeg(int pegNumber)
    {
        //Returning the peg we are on
        if (pegNumber == 1) return peg1;

        if (pegNumber == 2) return peg2;

        return peg3;
    }

    int GetTopNumberIndex(int[] peg)
    {
        //Getting the top number (earliest number in list with value that isn't 0)
        for (int i = 0; i < peg.Length; i++)
        {
            if (peg[i] != 0) return i;
        }

        //Returning a bad value if there were no numbers that werent 0
        return -1;
    }

    int GetIndexOfFreeSlot(int[] peg)
    {
        //Checking for earliest free slot
        for (int i = peg.Length - 1; i >= 0; i--)
        {
            if (peg[i] == 0) return i;
        }

        //Returns a bad value if there were no free slots
        return -1;
    }

    bool CheckIfPlayerWon(int[] lastPeg)
    {
        //Passes in a peg (the last peg in the series)
        //Want to check the peg for if it has all the disks in it
        for (int i = 0; i < lastPeg.Length; i++)
        {
            //Checking if any are 0, as each slot should be filled up for a win
            if (lastPeg[i] == 0)
            {
                //If any are, we return false
                return false;
            }
        }

        //Will set it to be true if it made it through the check
        return true;
    }
}
