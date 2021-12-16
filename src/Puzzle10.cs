namespace AdventOfCode2021;

public class Puzzle10 : AbstractPuzzle
{
    public Puzzle10(string input) : base(input)
    {
    }

    public override int Day()
    {
        return 10;
    }

    public override string SolvePart1()
    {
        var score = 0;
        foreach (var line in Input.Trim().Split('\n'))
        {
            var stack = new Stack<char>();
            foreach (var c in line)
            {
                if (c is '(' or '[' or '{' or '<')
                {
                    stack.Push(c);
                }
                else if (!stack.Any()) 
                {
                    break;
                }
                else if (c == ')' && stack.Peek() == '(' 
                         || c == ']' && stack.Peek() == '['
                         || c == '}' && stack.Peek() == '{'
                         || c == '>' && stack.Peek() == '<')
                {
                    stack.Pop();
                }
                else
                {
                    switch (c)
                    {
                        case ')': score += 3; break;
                        case ']': score += 57; break;
                        case '}': score += 1197; break;
                        case '>': score += 25137; break;
                    }
                    break;
                }
            }
        }
        return score.ToString();
    }

    public override string SolvePart2()
    {
        var costs = new List<long>();
        foreach (var line in Input.Trim().Split('\n'))
        {
            var corrupted = false;
            var stack = new Stack<char>();
            foreach (var c in line)
            {
                if (c is '(' or '[' or '{' or '<')
                {
                    stack.Push(c);
                }
                else if (!stack.Any()) 
                {
                    break;
                }
                else if (c == ')' && stack.Peek() == '(' 
                         || c == ']' && stack.Peek() == '['
                         || c == '}' && stack.Peek() == '{'
                         || c == '>' && stack.Peek() == '<')
                {
                    stack.Pop();
                }
                else
                {
                    corrupted = true;
                    break;
                }
            }
            if (!corrupted)
            {
                var cost = 0L;
                while (stack.Any())
                {
                    cost *= 5;
                    switch (stack.Pop())
                    {
                        case '(': cost += 1; break;
                        case '[': cost += 2; break;
                        case '{': cost += 3; break;
                        case '<': cost += 4; break;
                    }
                }
                costs.Add(cost);
            }
        }
        costs.Sort();
        return costs[costs.Count / 2].ToString();
    }
}