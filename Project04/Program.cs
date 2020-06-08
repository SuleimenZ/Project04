using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project04
{
    class Program
    {
        static double RPNCalculate(string input)
        {
            int length, numcounter = 0, opcounter = 0;
            double value1, value2, output;
            string[] inputArray;
            var inputStack = new Stack<double>();

            if(string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("No input given");
            }



            inputArray = input.Split(' ');
            length = inputArray.Length;

            foreach(string stringInput in inputArray)
            {
                if(string.IsNullOrEmpty(stringInput))
                {
                    continue;
                }
                if (double.TryParse(stringInput, out double num))
                {
                    inputStack.Push(Convert.ToDouble(stringInput));
                    numcounter++;
                }
                else
                {
                    value2 = inputStack.Last.Data;
                    inputStack.Pop();
                    value1 = inputStack.Last.Data;
                    inputStack.Pop();

                    switch (stringInput)
                    {
                        case "+":output = value1 + value2; inputStack.Push(output); break;
                        case "-": output = value1 - value2; inputStack.Push(output); break;
                        case "*": output = value1 * value2; inputStack.Push(output); break;
                        case "/": output = value1 / value2; inputStack.Push(output); break;
                        case "^": output = Math.Pow(value1, value2); inputStack.Push(output); break;
                        default: Console.WriteLine("Unexpected operator (" + stringInput +")"); Console.ReadLine(); break;
                    }
                    opcounter++;
                }
            }

            if (numcounter - 1 > opcounter)
            {
                throw new Exception("Wrong equation: not enough operators");
            }

            return inputStack.Last.Data;
        }

        static int Precedence(char Operator)
        {
            switch(Operator)
            {
                case '+': return 2;
                case '-': return 2;
                case '*': return 3;
                case '/': return 3;
                case '^': return 4;
                case '(': return 1;
                default: throw new Exception($"Invalid operator exception ({Operator})");
            }
        }

        static string InfixToPostfix(string input)
        {
            int length;
            string result = "";
            string[] inputArray;
            var inputStack = new Stack<char>();

            inputArray = input.Split(' ');
            length = inputArray.Length;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("No input given");
            }

            foreach (string stringInput in inputArray)
            {
                if (double.TryParse(stringInput, out double num))
                {
                    result += $"{stringInput} ";
                }
                else if (stringInput == "+" || stringInput == "-" || stringInput == "*" || stringInput == "/" || stringInput == "^")
                {
                    while (inputStack.Count != 0 && (Precedence(inputStack.Peek()) > Precedence(Convert.ToChar(stringInput)) || (Precedence(inputStack.Peek()) == Precedence(Convert.ToChar(stringInput)) && stringInput != "^")))
                    {
                        result += ($"{inputStack.Last.Data} ");
                        inputStack.Pop();
                    }
                    inputStack.Push(Convert.ToChar(stringInput));
                }
                else if (stringInput == "(")
                {
                    inputStack.Push(Convert.ToChar(stringInput));
                }
                else if (stringInput == ")")
                {
                    while (inputStack.Peek() != '(')
                    {
                        result += ($"{inputStack.Last.Data} ");
                        inputStack.Pop();
                    }
                    inputStack.Pop();
                }
                else
                {
                    throw new Exception($"Invalid operator '{stringInput}'");
                }
            }

            while(inputStack.Count > 0)
            {
                result += ($"{inputStack.Last.Data} ");
                inputStack.Pop();
            }

            result.Remove(result.Length - 1);
            return result;
        }

        static void Main(string[] args)
        {
            string userInput;

            Console.WriteLine("Write an equation.\nStack array is 10, so no more than 10 numbers or operators)\nSeparator between numbers, operators AND BRACKETS is space (' '); i.e: ( 5 + 2 ) * 3 ");
            userInput = Console.ReadLine();

            Console.Clear();
            Console.Write($"Your equation: {userInput}");
            Console.WriteLine();
            Console.Write($"Postfix equation: {InfixToPostfix(userInput)}");
            Console.WriteLine();
            Console.Write($"Answer: {RPNCalculate(InfixToPostfix(userInput))}");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
