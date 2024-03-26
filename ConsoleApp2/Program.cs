using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Введите арифметическое выражение: ");
            string input = Console.ReadLine();
            double operand1;
            double operand2;
            char operation;
            if (TryParseInput(input, out operand1, out operand2, out operation))
            {
                try
                {
                    double result = 0;

                    switch (operation)
                    {
                        case '+':
                            result = operand1 + operand2;
                            break;
                        case '-':
                            result = operand1 - operand2;
                            break;
                        case '*':
                            result = operand1 * operand2;
                            break;
                        case '/':
                            if (operand2 == 0)
                            {
                                throw new DivideByZeroException("На ноль делить нельзя");
                            }
                            result = operand1 / operand2;
                            break;
                        case '√':
                            if (operand1 < 0)
                            {
                                result = Math.Sqrt(Math.Abs(operand1));
                                Console.WriteLine("Результат: " + result + "i");
                                break;
                            }
                            result = Math.Sqrt(operand1);
                            break;
                        case '%':
                            result = operand1 * (operand2 / 100.0);
                            break;
                        default:
                            throw new FormatException("Некорректный оператор");
                    }

                    Console.WriteLine("Результат: " + result);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                }
                catch (DivideByZeroException e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод, попробуйте снова.");
            }
        }
    }

    static bool TryParseInput(string input, out double operand1, out double operand2, out char operation)
    {
        operand1 = 0;
        operand2 = 0;
        operation = '\0';

        // Регулярное выражение для разбора арифметического выражения
        Regex regex = new Regex(@"^\s*([-+]?\d+(\.\d+)?)\s*([+\-*/%√])\s*([-+]?\d+(\.\d+)?)?\s*$");

        Match match = regex.Match(input);

        if (!match.Success)
            return false;

        // Парсинг операндов и оператора
        operand1 = double.Parse(match.Groups[1].Value);
        operation = match.Groups[3].Value[0];

        // Если операция - корень, второй операнд игнорируется
        if (operation != '√')
        {
            operand2 = double.Parse(match.Groups[4].Value);
        }

        return true;
    }
}
