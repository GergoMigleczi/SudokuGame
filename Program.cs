using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuGame
{
    struct Try
    {
        public int guess;
        public int row;
        public int coloumn;

        public Try(int guess, int row, int coloumn)
        {
            this.guess = guess;
            this.row = row;
            this.coloumn = coloumn;
        }
    }
    class SudokuGame
    {
        /* 
         txt file looks like:

            0 0 0 0 0 0 0 0 6 
            0 0 7 0 0 0 2 0 0 
            3 8 9 5 0 0 0 0 0 
            6 0 0 0 0 0 7 0 0 
            1 2 8 4 9 0 0 3 0 
            0 0 0 0 0 0 0 0 9 
            0 5 1 0 0 0 0 4 0 
            0 0 0 0 0 3 0 9 1 
            0 0 0 0 8 0 0 0 0 
            9 2 4               //tries
            1 2 1 
            5 9 9 
            7 2 2 
         */

        static int[,] Sudoku = new int[9, 9];
        static List<Try> tries = new List<Try>();

        static string fileName = "";
        static int Row = 0;
        static int Coloumn = 0;

        //Task 1: Ask teh user for a file name, a row number and a coloumn number
        //        (there are three files and easy, medium and a hard one)
        static void Task1()
        {
            Console.WriteLine("1. feladat");

            Console.Write("easy.txt or medium.txt or hard.txt?  ");
            fileName = Console.ReadLine();

            Console.Write("Row ");
            Row = int.Parse(Console.ReadLine());

            Console.Write("Coloumn");
            Coloumn = int.Parse(Console.ReadLine());
        }

        //Task 2: Read and store the data of teh given file
        static void Task2()
        {
            StreamReader sr = new StreamReader(fileName);

            for (int i = 0; i < 9; i++)
            {
                string[] row = sr.ReadLine().Split();
                for (int j = 0; j < 9; j++)
                {
                    Sudoku[i, j] = int.Parse(row[j]);
                }
            }

            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Split();
                int guess = int.Parse(adatok[0]);
                int row = int.Parse(adatok[1]);
                int coloumn = int.Parse(adatok[2]);

                Try item = new Try(guess, row, coloumn);
                tries.Add(item);
            }
        }

        //Task 3: Print the value of the place given by the user
        //        determine which small rectangle it belongs to
        static void Task3()
        {
            Console.WriteLine("Task 3");

            if (Sudoku[Row, Coloumn] == 0)
            {
                Console.WriteLine("This place is not filled in yet.");
            }
            else
            {
                Console.WriteLine("Value of this place: " + Sudoku[Row - 1, Coloumn - 1]);
            }

            if (Row < 4)
            {
                if (Coloumn < 4)
                    Console.WriteLine("It belongs to the rectangle number 1.");
                else if (Coloumn < 7)
                    Console.WriteLine("It belongs to the rectangle number 2.");
                else if (Coloumn < 10)
                    Console.WriteLine("It belongs to the rectangle number 3.");
            }
            else if (Row < 7)
            {
                if (Coloumn < 4)
                    Console.WriteLine("It belongs to the rectangle number 4.");
                else if (Coloumn < 7)
                    Console.WriteLine("It belongs to the rectangle number 5.");
                else if (Coloumn < 10)
                    Console.WriteLine("It belongs to the rectangle number 6.");
            }
            else if (Row < 10)
            {
                if (Coloumn < 4)
                    Console.WriteLine("It belongs to the rectangle number 7.");
                else if (Coloumn < 7)
                    Console.WriteLine("It belongs to the rectangle number 8.");
                else if (Coloumn < 10)
                    Console.WriteLine("It belongs to the rectangle number 9.");
            }
        }

        //Task 4: Determine the percentage of the unsolved palces
        static void Task4()
        {
            Console.WriteLine("Task 4");

            int counter = 0;

            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    if (Sudoku[i, j] == 0)
                    {
                        counter++;
                    }

                }
            }
            Console.WriteLine($"percentage of the unsolved places: {Math.Round((double)counter / (9 * 9) * 100, 1)}%");
        }

        //Task 5: Check if the tries arecorrect or not
        static void Task5()
        {
            Console.WriteLine("Task 5");
            bool Row = false;
            bool coloumn = false;
            bool rectangle = false;
            foreach (Try row in tries)
            {

                Console.WriteLine($" row: {row.row} coloumn: {row.coloumn} guess: {row.guess}");
                if (Sudoku[row.row - 1, row.coloumn - 1] != 0)
                {
                    Console.WriteLine("The place is already filled out");
                }
                else
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (Sudoku[row.row - 1, i] == row.guess)
                        {
                            Row = true;
                        }
                        if (Sudoku[i, row.coloumn - 1] == row.guess)
                        {
                            coloumn = true;
                        }
                    }

                    if (row.row < 4)
                    {
                        if (row.coloumn < 4)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                        else if (row.coloumn < 7)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 3; j < 6; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                        else if (row.coloumn < 10)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 6; j < 9; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                    }
                    else if (row.row < 7)
                    {
                        if (row.coloumn < 4)
                        {
                            for (int i = 3; i < 6; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                        else if (row.coloumn < 7)
                        {
                            for (int i = 3; i < 6; i++)
                            {
                                for (int j = 3; j < 6; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                        else if (row.coloumn < 10)
                        {
                            for (int i = 3; i < 6; i++)
                            {
                                for (int j = 6; j < 9; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                    }
                    else if (row.row < 10)
                    {
                        if (row.coloumn < 4)
                        {
                            for (int i = 6; i < 9; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                        else if (row.coloumn < 7)
                        {
                            for (int i = 6; i < 9; i++)
                            {
                                for (int j = 3; j < 6; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                        else if (row.coloumn < 10)
                        {
                            for (int i = 6; i < 9; i++)
                            {
                                for (int j = 6; j < 9; j++)
                                {
                                    if (Sudoku[i, j] == row.guess)
                                        rectangle = true;
                                }
                            }
                        }
                    }

                    if (Row == true)
                    {
                        Console.WriteLine("Can not fit into that row");
                    }
                    else if (coloumn == true)
                    {
                        Console.WriteLine("Can not fit into that coloumn");
                    }
                    else if (rectangle == true)
                    {
                        Console.WriteLine("Can not fit into that rectangle");
                    }
                    else
                    {
                        Console.WriteLine("it fits");
                    }


                }

                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Console.WriteLine();
            Task3();
            Console.WriteLine();
            Task4();
            Console.WriteLine();
            Task5();
            Console.ReadKey();
        }
    }
}
