using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackValidator : MonoBehaviour
{
    private static StackValidator m_instance;
    public static StackValidator Instance => m_instance;

    private readonly Stack<int> m_CurrentStack = new();
    private readonly Stack<int> m_PoppedStack = new();
    private readonly Stack<int> m_ValidationStack = new();

    public Action<Stack<int>> OnStackModified;
    public Action<Stack<int>> ValidationListModified;
    public Action<Stack<int>> ValidationListSet;
    public Action ValidationListCleared;
    public Action StackInvalid;
    public Action StackValid;

    void Awake()
    {
        if(m_instance != null)
        {
            Destroy(m_instance);
        }

        m_instance = this;
    }

    public void AddInputToValidationStack(string inputString)
    {

        if (!int.TryParse(inputString, out var number))
        {
            Debug.LogError("Error! Input was not an Integer!");
            return;
        }

        m_ValidationStack.Push(number);
    }

    public void SetValidationList()
    {
        if(m_ValidationStack.Count == 1) 
        {
            Debug.Log("Error! Validation List must have at least one input.");
            return;
        }

        ValidationListSet?.Invoke(m_ValidationStack);
    }

    public void ClearValidationList()
    {
        m_ValidationStack?.Clear();
        ValidationListCleared?.Invoke();
    }

    public void AddInputToStack(string inputString)
    {
        if (!int.TryParse(inputString, out var number))
        {
            Debug.LogError("Error! Input was not an Integer!");
            return;
        }

        m_CurrentStack.Push(number);
        OnStackModified?.Invoke(m_CurrentStack);
        ValidateStack();
    }

    public void PopFromStack()
    {
        m_PoppedStack.Push(m_CurrentStack.Pop());
        OnStackModified?.Invoke(m_CurrentStack);
        ValidateStack();
    }

    public void ValidateStack()
    {
        List<int> poppedStackList = m_PoppedStack.ToList();
        List<int> validationStackList = m_ValidationStack.ToList();

        if(poppedStackList.Count >= validationStackList.Count)
        {
            Debug.LogError("Error! Popped Stack Count is greater than or equal to Validation Stack Count!");
        }

        for(int i = 0; i < poppedStackList.Count; i++)
        {
            if (poppedStackList[i] == validationStackList[i])
            {
                if(i == poppedStackList.Count - 1)
                {
                    if(m_CurrentStack.Peek() == validationStackList[i + 1])
                    {
                        if (i + 1 == validationStackList.Count)
                            StackValid?.Invoke();
                        else
                            return;
                    }
                    else
                    {
                        StackInvalid?.Invoke();
                    }
                    
                }

                continue;
            }

            StackInvalid?.Invoke();
            return;
        }
    }
}
