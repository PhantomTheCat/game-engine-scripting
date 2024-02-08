using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;


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

        //Will keep track of if the board is set up
        //Doesn't allow game to create another board while one is up
        bool isBoardSetUp = false;


        //Methods
        private void Awake()
        {
            //Initializing the rows and columns
            numberOfRows = grid.GetLength(0);
            numberOfCols = grid.GetLength(1);


            //Setting up the grid with new values that are random (Part of the assignment)
            //Selecting every row
            for (int row = 0; row < numberOfRows; row++)
            {
                //Selecting every column
                for (int col = 0; col < numberOfCols; col++)
                {
                    //Produces either 0 or 1
                    int randomNumber = Random.Range(0, 2);

                    //Setting the instance here in this spot to be 0 or 1
                    grid[row, col] = randomNumber;

                }
            }

            //Creating an identical 2D Array of our grid of the type bool rather than int
            hits = new bool[numberOfRows, numberOfCols];

            //Checking if we already have board up
            if (isBoardSetUp == false)
            {
                //Populating the grid (Figuring out how much to execute by multiplying rows by columns)
                for (int i = 0; i < numberOfRows * numberOfCols; i++)
                {
                    Instantiate(cellPrefab, gridRoot);
                }

                //Setting this variable to let game know we have the board
                isBoardSetUp = true;
            }

            SelectCurrentCell();

            //Repeating the incrementing of time every second
            InvokeRepeating("IncrementTime", 1f, 1f);
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
            //Setting the miss image to active
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

                //Trying to end the game if we hit something
                TryEndGame();
            }
            else
            {
                ShowMiss();
            }
        }

        void TryEndGame()
        {
            //Checking every row
            for (int row = 0; row < numberOfRows; row++)
            {
                //Checking every column
                for (int col = 0; col < numberOfCols; col++)
                {
                    //Ignoring spots on our grid that aren't ships
                    if (grid[row, col] == 0)
                    {
                        continue;
                    }
                    //Stopping the search and method if we find at least one ship that hasn't been hit
                    if (hits[row, col] == false) 
                    {
                        return;
                    }
                }

            }

            //Putting the winLabel on as if we get to this, it means that all ships have been hit
            winLabel.SetActive(true);

            //Stoping the timer and the InvokeRepeating method we called above
            CancelInvoke("IncrementTime");
        }

        void IncrementTime()
        {
            //Incrementing the time each second
            time++;

            //Updating the time label
            //The {0} stands for the minutes of the game, which is shown by time / 60
            //The {1} stands for the seconds of the game up to 60 (Always 2 digits)
            timeLabel.text = string.Format("{0}:{1}", time/60, (time % 60).ToString("00"));
        }

        public void RestartGame()
        {
            //Restarting the game by setting everything to defaults
            UnselectCurrentCell();
            currentSelectedCol = 0;
            currentSelectedRow = 0;
            SelectCurrentCell();

            //Resetting the 2D hits array to be blank
            //And Removing the hit and miss icons from the board (Set them to inactive)
            //Selecting every row
            for (int row = 0; row < numberOfRows; row++)
            {
                //Selecting every column
                for (int col = 0; col < numberOfCols; col++)
                {
                    //Setting the logic up for GetCurrentCell
                    currentSelectedCol = col;
                    currentSelectedRow = row;

                    //Sets the instance of that hit to be false
                    hits[row, col] = false;

                    //Gets current cell
                    Transform cell = GetCurrentCell();

                    //Setting the hit image to not active
                    Transform hit = cell.Find("Hit");
                    hit.gameObject.SetActive(false);

                    //Setting the miss image to not active
                    Transform miss = cell.Find("Miss");
                    miss.gameObject.SetActive(false);
                }
            }

            //Reseting these values are searching through both grids
            UnselectCurrentCell();
            currentSelectedCol = 0;
            currentSelectedRow = 0;
            SelectCurrentCell();

            //Resetting timer and score, and their labels
            time = 0;
            timeLabel.text = string.Format("{0}:{1}", time / 60, (time % 60).ToString("00"));
            score = 0;
            scoreLabel.text = string.Format("Score: {0}", score);



            //Setting the winLabel to be inactive
            winLabel.SetActive(false);



            //For the assignment, the randomization of the locations of the ships is included in my awake function
            //This will allow it to be called upon to randomize at the start and every time the game is reset.
            //Reseting up the game
            Awake();
        }
    }

}
