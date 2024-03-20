using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_0._2
{
    class Program
    {
        static public int gametracker = 0;
        static public int[,] gametable =
        {
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
        static public Tuple<int, int> answer;
        static public int row1, row2, row3, collumn1, collumn2, collumn3, cross1, cross2;



        static public void Main(string[] args)
        {
            Console.WriteLine("This is a Player vs AI(kinda) game \n" +
                "AI will play X and will win or draw the game with absolute win strategy that is basicly makes X unbeatable\n\n" +
                "How to play:\n" +
                "Player plays for O " +
                "\nif 3 of X or O lines up in a collumn or a row or crosswise its a win for the one who \"triples\" \n" +
                "the way you draw O on the game table is by typing \"collumn\" + enter \"row\" + enter  when its your turn\n\n" +
                "press any key to start the game");
            Console.ReadLine();

            Console.WriteLine("Calculating the move...");
            

            for(gametracker = 0; gametracker > -1; gametracker++) 
            {
                answer = GenerateAnswer(gametracker);
                gametable[answer.Item1, answer.Item2] = 1;
                Display();
                
                if (gametracker != -2 && gametracker != -5)
                {
                    AskPlayer();
                    gametable[answer.Item1, answer.Item2] = -1;
                    Display();
                } 
                
            }
            if (gametracker == -1)
            {
                Console.WriteLine($"\n\n X wins!");
            }
            else
            {
                Console.WriteLine("\n\n Its a draw! Good job!");
            } 
            
        }

        static void Display () 
        {
            for (int i = 0; i < gametable.GetLength(0); i++) 
            {
                for (int j = 0; j < gametable.GetLength(1); j++)
                {
                    if (gametable[i, j] == 1)
                    {
                        Console.Write("X"); // X is 1 on table
                    }
                    if (gametable[i, j] == -1)
                    {
                        Console.Write("O"); // O is -1 on table
                    }
                    if (gametable[i, j] == 0)
                    {
                        Console.Write(" "); // 0 is empty
                    }
                    if (j < 2)
                    {
                        Console.Write("|"); // this separates second collumn from first and third
                    }
                }            
                Console.WriteLine(); // separates rows
            }
            
        
        
        
        }

        static public Tuple<int, int> GenerateAnswer (int move)
        {
            Random rnd = new Random();
            LineCounter();
            int i = 0, j = 0;
            switch (move)
            {
                case 0:
                    i = rnd.Next(0, 2)*2;
                    j = rnd.Next(0, 2)*2;
                    return Tuple.Create(i, j);
                case 1:
                    switch (1)
                    {
                    
                        case int n when n == row1 + collumn2: //collumn2 == 0
                            Console.WriteLine("row1:,collumn2 == 0");
                            if (Check(0, 0)) { i = 0; j = 0; }
                            else { i = 0; j = 2; }
                            break; //break breaks all the switch                        
                        case int n when n == row3 + collumn2: //collumn2 == 0
                            Console.WriteLine("row3:,collumn2 == 0");
                            if (Check(2, 0)) { i = 2; j = 0; }
                            else { i = 2; j = 2; }
                            break;                        
                        case int n when n == collumn1 + row2: //row2 == 0
                            Console.WriteLine("collumn1:,row2 == 0");
                            if (Check(0, 0)) { i = 0; j = 0; }
                            else { i = 2; j = 0; }
                            break;                        
                        case int n when n == collumn3 + row2: //row2 == 0
                            Console.WriteLine("collumn3:,row2 == 0");
                            if (Check(0, 2)) { i = 0; j = 2; }
                            else { i = 2; j = 2; }
                            break;
                        default:
                            switch (1)
                            {
                                case int n when n == row1:
                                    if (Check(0, 0)) { i = 0; j = 0; } else { i = 0; j = 2; }
                                    break;
                                case int n when n == row3:
                                    if (Check(2, 0)) { i = 2; j = 0; } else { i = 2; j = 2; }
                                    break;                                
                            }
                            break;
                    }                    
                    return Tuple.Create(i, j);
                case 2:
                    if (CheckLines() != null)
                    {
                        switch (2) // wins the game if there are 2 x in a line
                        {
                            case int n when n == row1:
                                i = 0; j = 1;
                                gametracker = -2; //x wins
                                break;
                            case int n when n == row3:
                                i = 2; j = 1;
                                gametracker = -2; //x wins
                                break;
                            case int n when n == collumn1:
                                i = 1; j = 0;
                                gametracker = -2; //x wins
                                break;
                            case int n when n == collumn3:
                                i = 1; j = 2;
                                gametracker = -2; //x wins
                                break;
                       }
                    }
                    else if (Check(1,1)) // means midle (1, 1) is empty and xox happened -->
                    {
                        switch (1) // find the spot that will make 2 lines (one is crosswise) value 2 so it cant be blocked at the same time
                        {
                            case int n when n == row1 + collumn2:
                                if (Check(0,0)) { i = 0; j = 0; } else { i = 0; j = 2; }
                                break;
                            case int n when n == row3 + collumn2:
                                if (Check(2,0)) { i = 2; j = 0; } else { i = 2; j = 2; }
                                break;
                            case int n when n == collumn1 + row2:
                                if (Check(0,0)) { i = 0; j = 0; } else { i = 2; j = 0; }
                                break;
                            case int n when n == collumn3 + row2:
                                if (Check(0,2)) { i = 0; j = 2; } else { i = 2; j = 2; }
                                break;
                        }
                    }
                    else //means midle (1, 1) is O and xox happened
                    {
                        if (collumn2 == -2)
                        {
                            if (Check(0, 1)) { i = 0; j = 1; } else { i = 2; j = 1; }
                        }
                        if (row2 == -2)
                        {
                            if (Check(1,0)) { i = 1; j = 0; } else { i = 1; j = 2; }
                        }
                    }
                    return Tuple.Create(i, j);
                case 3:
                    if (CheckLines() != null)
                    {
                        switch (2) // wins the game if there are 2 x in a line
                        {
                            case int n when n == row1:
                                i = 0; j = 1;
                                gametracker = -2; //x wins
                                break;
                            case int n when n == row3:
                                i = 2; j = 1;
                                gametracker = -2; //x wins
                                break;
                            case int n when n == collumn1:
                                i = 1; j = 0;
                                gametracker = -2; //x wins
                                break;
                            case int n when n == collumn3:
                                i = 1; j = 2;
                                gametracker = -2; //x wins
                                break;
                            case int n when n == cross1:
                                i = 1; j = 1;
                                gametracker = -2; //x wins
                                break;
                            case int n when n == cross2:
                                i = 1; j = 1;
                                gametracker = -2; //x wins
                                break;                            
                        }
                    }
                    else if (row2 == -2) //prevent OO with XOO
                    {
                        if (Check(1,0)) { i = 1; j = 0; } else { i = 1; j = 2; }
                    }
                    else if (collumn2 == -2) //prevent OO with XOO
                    { 
                        if (Check(0,1)) { i = 0; j = 1; } else { i = 2; j = 1; } 
                    }
                    else if (row2 == -1 && Check(1,0)) { i = 1; j = 0; } //make sure to put X on midle lines so its impossible for O to win
                    else if (collumn2 == -1 && Check(0,1)) { i = 0; j = 1; } //make sure to put X on midle lines so its impossible for O to win

                    return Tuple.Create(i, j);
                case 4:
                    if (CheckLines() != null)
                    {
                        switch (2) // X wins the game if there are 2 x in a line
                        {
                            case int n when n == row1:
                                if (Check(0, 0)) { i = 0; j = 0; }
                                else { i = 0; j = 2; }
                                gametracker = -2; //x wins
                                break;
                            case int n when n == row3:
                                if (Check(2, 0)) { i = 2; j = 0; }
                                else { i = 2; j = 2; }
                                gametracker = -2; //x wins
                                break;
                            case int n when n == collumn1:
                                if (Check(0, 0)) { i = 0; j = 0; }
                                else { i = 2; j = 0; }
                                gametracker = -2; //x wins
                                break;
                            case int n when n == collumn3:
                                if (Check(0, 2)) { i = 0; j = 2; }
                                else { i = 2; j = 2; }
                                gametracker = -2; //x wins
                                break;
                        }
                    }
                    else
                    {
                        switch (0)
                        {
                            case int n when n == row1:
                                if (Check(0, 0)) { i = 0; j = 0; }
                                else if (Check(0, 1)) { i = 0; j = 1; }
                                else if (Check(0, 1)) { i = 0; j = 2; }
                                break;
                            case int n when n == row2:
                                if (Check(1, 0)) { i = 1; j = 0; }
                                else if (Check(1, 1)) { i = 1; j = 1; }
                                else if (Check(1, 2)) { i = 1; j = 2; }
                                break;
                            case int n when n == row3:
                                if (Check(2, 0)) { i = 2; j = 0; }
                                else if (Check(2, 1)) { i = 2; j = 1; }
                                else if (Check(2, 2)) { i = 2; j = 2; }
                                break;
                        }
                        gametracker = -5;
                        Console.WriteLine(gametracker);
                    }
                    
                return Tuple.Create(i, j);
            }
            Console.WriteLine("its broken");
            return Tuple.Create(0, 0);
        }

        static bool Check(int i, int j)
        {
            if (gametable[i, j] == 1 || gametable[i, j] == -1) { return false; }
            else { return true; }
        }

        static public void AskPlayer()
        {
            Console.WriteLine("Your turn!\n row:");
            int row = int.Parse(Console.ReadLine());
            Console.WriteLine("Collumn:");
            int collumn = int.Parse(Console.ReadLine());
            if (Check(row -1, collumn -1)) 
            {
                answer = Tuple.Create(row - 1, collumn - 1);
            }
            else 
            {
                Console.WriteLine("Thats Illegal!");
                AskPlayer();
            }
        }
        
        static public void LineCounter()
        {
            /*
            for (int i = 0; i < gametable.GetLength(1); i++) // this looks way better but it would be stupid to use for this 8 of them
            {
                row1 += gametable[0, i];
            }
            */
            row1 = gametable[0, 0] + gametable[0, 1] + gametable[0, 2];
            row2 = gametable[1, 0] + gametable[1, 1] + gametable[1, 2];
            row3 = gametable[2, 0] + gametable[2, 1] + gametable[2, 2];
            collumn1 = gametable[0, 0] + gametable[1, 0] + gametable[2, 0];
            collumn2 = gametable[0, 1] + gametable[1, 1] + gametable[2, 1];
            collumn3 = gametable[0, 2] + gametable[1, 2] + gametable[2, 2];
            cross1 = gametable[0, 0] + gametable[1, 1] + gametable[2, 2];
            cross2 = gametable[0, 2] + gametable[1, 1] + gametable[2, 0];

        }

        static public string CheckLines()
        {
            switch (2)
            {
                case int n when n == row1:
                    return "row1";
                case int n when n == row2:
                    return "row2";
                case int n when n == row3:
                    return "row3";
                case int n when n == collumn1:
                    return "collumn1";
                case int n when n == collumn2:
                    return "collumn2";
                case int n when n == collumn3:
                    return "collumn3";
                case int n when n == cross1:
                    return "cross1";
                case int n when n == cross2:
                    return "cross2";                
                default: return null;
            }            
        }
    }
}
