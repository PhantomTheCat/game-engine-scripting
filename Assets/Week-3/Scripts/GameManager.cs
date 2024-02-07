using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Battleship
{
    public class GameManager : MonoBehaviour
    {
        //A 2D Array for our Battleship Map (0 = Water and 1 = Ship)
        [SerializeField]
        private int[,] grid = new int[,]
        {
            //Top left is (0,0)
            { 1, 1, 0, 0, 1 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 0 },
            { 1, 0, 1, 0, 1 }
            //Bottom Right is (4, 4)
        };
        
        //A 2D array that shows where the player has hit
        private bool[,] hits;

        //The number of rows and columns for the grid total
        private int numberOfRows;
        private int numberOfCols;

        //The selected row and column by player to fire at
        private int currentSelectedRow;
        private int currentSelectedCol;

        //The current score and time for the UI and Player
        private int score;
        private int time;

        //Parent of all cells
        [SerializeField] Transform gridRoot;
        //Unity Components Referenced
        [SerializeField] GameObject cellPrefab;
        [SerializeField] GameObject winLabel;
        [SerializeField] TextMeshProUGUI timeLabel;
        [SerializeField] TextMeshProUGUI scoreLabel;


        //Methods
        private void Awake()
        {
            //Initializing the rows and columns
            numberOfRows = grid.GetLength(0);
            numberOfCols = grid.GetLength(1);

            //Creating an identical 2D Array of our grid of the type bool rather than int
            hits = new bool[numberOfRows, numberOfCols];

            //Populating the grid (Figuring out how much to execute by multiplying rows by columns)
            for (int i = 0; i < numberOfRows * numberOfCols; i++)
            {
                Instantiate(cellPrefab, gridRoot);
            }

            SelectCurrentCell();
        }

        Transform GetCurrentCell()
        {
            //Finding out the index of the child of the cell on the grid
            int index = (currentSelectedRow * numberOfCols) + currentSelectedCol;

            //Returning the child we just found.
            return gridRoot.GetChild(index);
        }

        void SelectCurrentCell()
        {
            //Telling the grid to find the current cell
            Transform cell = GetCurrentCell();

            //Setting the cursor object to active
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(true);
        }

        void UnselectCurrentCell()
        {
            //Telling the grid to find the current cell
            Transform cell = GetCurrentCell();

            //Setting the cursor object to not be active
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(false);
        }

        public void MoveHorizontal(int amt)
        {
            //Deselecting the spot we are on
            UnselectCurrentCell();

            //Update our current column
            currentSelectedCol += amt;
            //Making sure our column is in the grid
            currentSelectedCol = Mathf.Clamp(currentSelectedCol, 0, numberOfCols - 1);

            //Selecting the new spot we are on
            SelectCurrentCell();
        }

        public void MoveVertical(int amt)
        {
            //Deselecting the spot we are on
            UnselectCurrentCell();

            //Update our current row
            currentSelectedRow += amt;
            //Making sure our row is in the grid
            currentSelectedRow = Mathf.Clamp(currentSelectedRow, 0, numberOfRows - 1);

            //Selecting the new spot we are on
            SelectCurrentCell();
        }

        void ShowHit()
        {
            //Gets current cell
            Transform cell = GetCurrentCell();
            //Setting the hit image to active
            Transform hit = cell.Find("Hit");
            hit.gameObject.SetActive(true);
        }

        void ShowMiss()
        {
            //Gets current cell
            Transform cell = GetCurrentCell();
            //Setting the hit image to active
            Transform miss = cell.Find("Miss");
            miss.gameObject.SetActive(true);
        }

        void IncrementScore()
        {
            //Incrementing the score variable
            score++;
            //Updating the score label
            scoreLabel.text = string.Format("Score: {0}", score);
        }

        public void Fire()
        {
            //Checks if this location has already been hit and stops method if it has already
            if (hits[currentSelectedRow, currentSelectedCol])
            {
                return;
            }

            //Marks the current position as being hit
            hits[currentSelectedRow, currentSelectedCol] = true;

            //Logic for if it hit or not
            if (grid[currentSelectedRow, currentSelectedCol] == 1)
            {
                ShowHit();
                IncrementScore();
            }
            else
            {
                ShowMiss();
            }
        }
    }

}
