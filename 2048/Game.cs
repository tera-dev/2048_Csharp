using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Game
    {
        public Game()
        { 
            this.arr2D = new Cell [4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arr2D[i, j] = new Cell(0, false);
                }
            }
            direction = new ConsoleKey();
            rand = new Random();
        }
        private Cell[,] arr2D; //game array-field
        private ConsoleKey direction; // move direction
        private int a; // [i] index of new element in field 
        private int b;//[j] index of new element in field 
        private int score = 0; 
        Random rand;
        int buff;
        bool isNewValueAdded;

        
        private void PrintField() 
        {
            Console.Clear(); 

            Console.WriteLine("\t\t\t\t\tScore: " + score);
            AddElement();

            for (int i = 0; i < 4; i++)//changing color according to cell value
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr2D[i, j].value == 2) { Console.ForegroundColor = ConsoleColor.Cyan; }
                    
                    else if (arr2D[i, j].value == 4) { Console.ForegroundColor = ConsoleColor.Magenta; }
                    else if (arr2D[i, j].value == 8) { Console.ForegroundColor = ConsoleColor.Red; }
                    else if (arr2D[i, j].value == 16) { Console.ForegroundColor = ConsoleColor.Yellow; }
                    else if (arr2D[i, j].value == 32) { Console.ForegroundColor = ConsoleColor.Blue; }
                    else if (arr2D[i, j].value == 64) { Console.ForegroundColor = ConsoleColor.White; }
                    else if (arr2D[i, j].value == 128) { Console.ForegroundColor = ConsoleColor.Green; }
                    else if (arr2D[i, j].value == 256) { Console.ForegroundColor = ConsoleColor.Gray; }
                    else if (arr2D[i, j].value == 512) { Console.ForegroundColor = ConsoleColor.DarkYellow; }
                    else if (arr2D[i, j].value == 1024) { Console.ForegroundColor = ConsoleColor.DarkRed; }
                    else if (arr2D[i, j].value == 2048) { Console.ForegroundColor = ConsoleColor.DarkGreen; }
                    else { Console.ForegroundColor = ConsoleColor.Gray; }
                    Console.Write(arr2D[i,j].value + "\t"); 
                }

                
                Console.WriteLine("\n\n\n\n");
                
            }
            Console.WriteLine(); 
            Console.ForegroundColor = ConsoleColor.Gray; //backing to standard color
        }

        private void GetDirection()
        {
            direction = Console.ReadKey().Key; 
            switch (direction) 
            {
                case ConsoleKey.LeftArrow:
                    {
                        MoveLeft();
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        MoveRight();
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        MoveDown();
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        MoveUp();
                        break;
                    }
            }
        }

        private void MakeIsDoubledPropertyFalse()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arr2D[i, j].isDoubled = false;
                }
            }
        }

        /*MoveLeft() и MoveRight() move columns
        MoveDown() и MoveUp() move rows*/

        private void MoveLeft()
        {
            //moving columns from left to right beginning from 2nd

            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //checking whether current element (arr2D[j, i]) can be moved on the place of previous (arr2D[j, i - 1])
                    while (true)
                    {
                        if ( i > 0 && arr2D[j, i - 1].value == 0 && arr2D[j, i].value != 0)
                        {
                            buff = arr2D[j, i].value;
                            arr2D[j, i].value = arr2D[j, i - 1].value; 
                            arr2D[j, i - 1].value = buff;
                        }
                        else break; 
                        i--; 
                    }

                    if (i > 0 && arr2D[j, i - 1].value == arr2D[j, i].value && 
                        arr2D[j,i].value!=0 && 
                        !arr2D[j, i - 1].isDoubled) //cheking is neighbour element is the same and whether curr-nt element has been already doubled in current move
                    {
                        arr2D[j, i - 1].value *= 2; arr2D[j, i].value = 0; score += arr2D[j, i - 1].value;
                        arr2D[j, i - 1].isDoubled = true;
                    }
                }
            }
            MakeIsDoubledPropertyFalse();
            PrintField();
            GetDirection();
        }


        private void MoveRight()
        {
            for (int i = 2; i >= 0; i--)//moving columns from left to right beginning from 3rd
            {
                for (int j = 0; j < 4; j++)
                {
                    while (true)
                    {
                        if (i < 3 && arr2D[j, i + 1].value == 0 && arr2D[j, i].value != 0)
                        //checking whether current element (arr2D[j, i].value) can be moved on the place of next (arr2D[j, i + 1].value)
                        {
                            buff = arr2D[j, i + 1].value; 
                            arr2D[j, i + 1].value = arr2D[j,i].value;
                            arr2D[j, i].value = buff;
                        }
                        else break;
                        i++; 
                    }
                    if (i < 3 && arr2D[j, i + 1].value == arr2D[j, i].value && arr2D[j, i].value != 0 && !arr2D[j, i + 1].isDoubled)//cheking is neighbour element is the same
                    {
                        arr2D[j, i + 1].value *= 2; arr2D[j, i].value = 0; score += arr2D[j, i + 1].value;
                        arr2D[j, i + 1].isDoubled = true;
                    }
                }
            }
            MakeIsDoubledPropertyFalse();
            PrintField();
            GetDirection();
        }

        private void MoveDown()
        {

            for (int j = 2; j >= 0; j--)//moving rows from up to down beginning from 3d (from above)
            {
                for (int i = 0; i < 4; i++)
                {
                    while (true)
                    {
                        if (j < 3 && arr2D[j + 1, i].value == 0 && arr2D[j, i].value != 0)
                        {
                            buff = arr2D[j + 1, i].value;
                            arr2D[j + 1, i].value = arr2D[j, i].value;
                            arr2D[j, i].value = buff;
                        }
                        else { break; }
                        j++;
                        
                    }
                    if (j < 3 && arr2D[j + 1, i].value == arr2D[j, i].value && arr2D[j, i].value != 0 && !arr2D[j + 1, i].isDoubled)
                    {
                        arr2D[j + 1, i].value *= 2; arr2D[j, i].value = 0; score += arr2D[j + 1, i].value;
                        arr2D[j + 1, i].isDoubled = true;
                    }
                    //PrintField();
                }
            }
            MakeIsDoubledPropertyFalse();
            PrintField();
            GetDirection();
        }

        private void MoveUp()
        {

            for (int j = 1; j < 4; j++)//moving rows down to up beginning from 2nd (from above)
            {
                for (int i = 0; i < 4; i++)
                {
                    while (true)
                    {
                        if (j > 0 && arr2D[j - 1, i].value == 0 && arr2D[j, i].value != 0)
                        {
                            buff = arr2D[j - 1, i].value;
                            arr2D[j - 1, i].value = arr2D[j, i].value;
                            arr2D[j, i].value = buff;
                        }
                        else break; 
                        j--;
                    }
                    if (j > 0 && arr2D[j - 1, i].value == arr2D[j, i].value && arr2D[j, i].value != 0 && !arr2D[j - 1, i].isDoubled)
                    {
                        arr2D[j - 1, i].value *= 2; arr2D[j, i].value = 0; score += arr2D[j - 1, i].value;
                        arr2D[j - 1, i].isDoubled = true;
                    }
                }
            }
            MakeIsDoubledPropertyFalse();
            PrintField();
            GetDirection();
        }

        private void AddElement() 
        {
            isNewValueAdded = false;

            while (!isNewValueAdded)
            {
                a = rand.Next(0, 4);
                b = rand.Next(0, 4);

                if (arr2D[a, b].value != 0)
                {
                    if (!DoesFieldHasEmptyCell()) GameOver();
                }

                else
                {
                    if (rand.Next(1, 9) % 3 == 0) arr2D[a, b].value = 4; //made for appearing "4" less often, than "2"
                    else arr2D[a, b].value = 2; 
                    isNewValueAdded = true;
                }
            }            
        }

        private bool DoesFieldHasEmptyCell()
        {
            for (int i = 0; i < 4; i++)
			{
			    for (int j = 0; j < 4; j++)
			    {
                    if (arr2D[i,j].value == 0) return true;
			    }
			}
            return false;
        }

                
        public void Iniz()
        { 
            AddElement();
            PrintField();
            GetDirection();
        }

        private void ClearFieldForNewGame()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arr2D[i, j].value = 0;
                    arr2D[i, j].isDoubled = false;
                }
            }
            score = 0;

        }

        private void GameOver()
        {
            Console.WriteLine("\n\n\n\n\t\t Game Over! Press Enter to play again.");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                ClearFieldForNewGame();
                Iniz();
            } 

        }

    }
}
