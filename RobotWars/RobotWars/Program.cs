using System;
/*
RobotWars

Mountain warehouse programming assessment
A fleet of hand-built robots are due to engage in battle for the annual “Robot Wars” competition. 
Each robot will be placed within a rectangular battle arena and will navigate their way around the arena using a built-in computer system.
A robot’s location and heading are represented by a combination of x and y co-ordinates and a letter representing one of the four cardinal
compass points. The arena is divided up into a grid to simplify navigation. An example position might be 0, 0, N which means the robot
is in the bottom left corner and facing North.
In order to control a robot, the competition organisers have provided a console for sending a simple string of letters to the on-board
navigation system. The possible letters are ‘L’, ‘R’ and ‘M’. ‘L’ and ‘R’ make the rover spin 90 degrees to the left or right respectively
without moving from its current spot while ‘M’ means move forward one grid point and maintain the same heading.
Assume that the square directly North from (x, y) is (x, y+1).

INPUT:
The first line of input is the upper-right coordinates of the arena, the lower-left coordinates are assumed to be (0, 0).
The rest of the input is information pertaining to the robots that have been deployed. 
Each robot has two lines of input - the first gives the robot’s position and the second is a series of instructions telling the robot how to move within the arena.
The position is made up of two integers and a letter separated by spaces, corresponding to the x and y coordinates and the robot’s orientation. Each robot will finish moving sequentially, which means that the second robot won’t start to move until the first one has finished moving.

Input.txt
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM

TESTS:
 - Robots cannot leave the arena
 */

namespace RobotWars
{
    class Program
    {   

        public static void PrintArena(int[,] arena, int col, int row, string direction = "x"){
            String matrix;
            for (int i = row - 1; i >= 0 ; i--)
            {
                matrix = "";
                for (int j = 0; j < col ; j++)
                {
                    if (arena[i, j] == 1)
                    {
                        matrix += direction;
                    }
                    else {
                        matrix += ".";
                    }

                }
                Console.WriteLine(matrix);
            }
            Console.WriteLine();
        }


        public static void Wars(int[,] arena, int row, int col, Robot bot)
        {

            string[] facing = { "N", "E", "S", "W" };
            string input = "";
            bool validation;
            bool playing = true;
            int pointer = Array.IndexOf(facing, bot.direction);
            Console.WriteLine("Robot wars has begun!");
            Console.WriteLine("Each turn you will have a choice of four commands, Left(L), Right(R) or Move(M), Cancel(C) selecting left or right will rotate your robots direction by 90 degrees accordingly. Cancel wil end the game");
            while (playing == true)
            {
                validation = false;
                while (validation != true)
                {
                    Console.WriteLine("Please enter one of the follow commands: L, R, M, C");
                    input = Console.ReadLine();
                    if (input == "C") {
                        playing = false;
                        break;
                    }
                    if (input != "L" && input != "R" && input != "M")
                    {
                        Console.WriteLine("Invalid input, please enter one of the commands: L, R, M");
                    }
                    else
                    {
                        validation = true;
                    }
                }

                if (input == "L")
                {
                    if (pointer == 0)
                    {
                        pointer = 3;
                        continue;
                    }
                    else
                    {
                        pointer--;
                        continue;
                    }
                }

                if (input == "R")
                {
                    if (pointer == 3)
                    {
                        pointer = 0;
                        continue;
                    }
                    else
                    {
                        pointer++;
                        continue;
                    }
                }

                if (input == "M")
                {
                    if (facing[pointer] == "N")
                    {
                        if (bot.x + 1 > 5)
                        {
                            Console.WriteLine("This will take your robot out of the arena, please select a different command");
                            continue;
                        }
                        else
                        {
                            arena[bot.x, bot.y] = 0;
                            arena[bot.x + 1, bot.y] = 1;
                            bot.x++;
                            PrintArena(arena, 5 + 1, 5 + 1);
                        }
                    }

                    if (facing[pointer] == "S")
                    {
                        if (bot.x - 1 < 0)
                        {
                            Console.WriteLine("This will take your robot out of the arena, please select a different command");
                            continue;
                        }
                        else
                        {
                            arena[bot.x, bot.y] = 0;
                            arena[bot.x - 1, bot.y] = 1;
                            bot.x--;
                            PrintArena(arena, row , col);
                            continue;
                        }
                    }

                    if (facing[pointer] == "E")
                    {
                        if (bot.y + 1 > 5)
                        {
                            Console.WriteLine("This will take your robot out of the arena, please select a different command");
                            continue;
                        }
                        else
                        {
                            arena[bot.x, bot.y] = 0;
                            arena[bot.x, bot.y + 1] = 1;
                            bot.y++;
                            PrintArena(arena, row, col);
                            continue;
                        }
                    }


                    if (facing[pointer] == "W")
                    {
                        if (bot.y - 1 < 0)
                        {
                            Console.WriteLine("This will take your robot out of the arena, please select a different command");
                            continue;
                        }
                        else
                        {
                            arena[bot.x, bot.y] = 0;
                            arena[bot.x, bot.y - 1] = 1;
                            bot.y--;
                            PrintArena(arena, row , col);
                            continue;
                        }
                    }
                }
            }
            Console.WriteLine(bot.y + "," + bot.x + " " + bot.direction);
            Console.WriteLine("The game has ended");
        }

        static void Main(string[] args)
        {   
            int value;
            int col = 0;
            int row = 0;
            Robot bot = new Robot();
            bool success = false;
            while (success == false)
            {
                Console.WriteLine("Enter the size of your battle arena (Square shaped). This should be an integer");
                String size = Console.ReadLine();
                success = int.TryParse(size, out value);
                if (success)
                {
                    col = value;
                    row = value;
                }
            }

            success = false;
            while (success == false) {
                Console.WriteLine("Enter the starting co-ordinates of your robot, this should be two integers between 0 and the max size of your arena, given in the format 'X,Y'.");
                String co_ordinates = Console.ReadLine();
                int value1 = 0;
                int value2 = 0;
                    if (co_ordinates.Length == 3 && int.TryParse(co_ordinates[0].ToString(), out value1) && int.TryParse(co_ordinates[2].ToString(), out value2) && value1 >= 0 && value1 <= col && value2 >= 0 && value2 <= col)
                    {
                        bot.x = value2;
                        bot.y = value1;
                        success = true;
                    }
            }

            success = false;
            while (success == false)
            {
                Console.WriteLine("Please Enter the direction your bot will be facing, N, E, S, W");
                String input = Console.ReadLine();
                if (input != "N" && input!= "E" && input != "S" && input!= "W")
                {
                    Console.WriteLine("Please enter a valid direction!");
                }
                else
                {
                    bot.direction = input;
                    success = true;
                }
            }

            int[,] arena = new int[col + 1, row  + 1];
            arena[bot.x, bot.y] = 1;
            PrintArena(arena, col + 1, row + 1, bot.direction);
            Wars(arena, col + 1, row + 1, bot);
        }
    }
}