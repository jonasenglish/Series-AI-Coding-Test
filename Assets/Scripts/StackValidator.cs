using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class StackValidator : MonoBehaviour
{
    private static StackValidator m_instance;
    public static StackValidator Instance => m_instance;

    public Action<string> OnOutput;

    void Awake()
    {
        if(m_instance != null)
        {
            Destroy(m_instance);
        }

        m_instance = this;
    }

    public bool ValidateStack(string sequence)
    {
        var stack = new Stack<int>();
        int nextPush = 0;
        var delimSequence = sequence.Split(' ');
        int N = delimSequence.Length;

        for (int i = 0; i < N; i++)
        {
            if (!int.TryParse(delimSequence[i], out var current))
            {
                Debug.LogError($"Error! Input was not an integer! Input was {delimSequence[i]}");
                return false;
            }

            while (nextPush <= current)
            {
                stack.Push(nextPush);
                nextPush++;
            }

            if (stack.Count == 0 || stack.Peek() != current)
            {
                return false;
            }
            else
            {
                stack.Pop();
            }
        }

        return true;
    }

    public void ValidateStack_NoExpectedOutput(string sequence)
    {
        var result = ValidateStack(sequence);
        OnOutput?.Invoke(sequence + " " + result.ToString());
    }

    public void ValidateStack_ExpectedOutput(string sequence, bool expectedOutput)
    {
        var result = ValidateStack(sequence.Trim());
        StringBuilder sb = new();
        sb.Append(sequence);

        if(expectedOutput == result)
        {
            sb.Append($" Result {result} matched expected output {expectedOutput}");
        }
        else
        {
            sb.Append($" Result {result} did not match expected output {expectedOutput}");
            Debug.LogError($"Result {result} did not match expected output {expectedOutput}");
        }

        OnOutput?.Invoke(sb.ToSafeString());
    }
}
