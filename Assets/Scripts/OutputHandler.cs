using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class OutputHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_OutputText;

    private StackValidator Validator => StackValidator.Instance;

    void Start()
    {
        Validator.OnOutput += StackValidator_OnOutput;
    }

    private void StackValidator_OnOutput(string output)
    {
        m_OutputText.text += output + "\n";
    }

    public void OnClick_Clear()
    {
        m_OutputText.text = string.Empty;
    }
}
