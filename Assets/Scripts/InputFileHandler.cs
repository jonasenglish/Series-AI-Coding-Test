using UnityEngine;
using System.IO;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;

public class InputFileHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_FileName;

    StackValidator Validator => StackValidator.Instance;

    private string m_FilePath = "";

    public void OpenFileBrowser()
    {
        string path = EditorUtility.OpenFilePanel("Select a text file", "", "txt");

        if (!string.IsNullOrEmpty(path))
        {
            var file = File.Open(path, FileMode.Open);
            m_FileName.text = file.Name;
            file.Close();

            m_FilePath = path;
        }
    }

    public void OnClick_ValidateFile()
    {
        if (!File.Exists(m_FilePath))
        {
            Debug.LogError("Error! No file selected!");
        }

        var lines = File.ReadLines(m_FilePath);
        foreach (var line in lines) 
        { 
            var spaceSplit = line.Split(' ');
            int spaceSplitLength = spaceSplit.Length;
            bool hasExpectedResult = false;
            bool expectedResult = false;

            if (spaceSplit[spaceSplitLength - 1].ToLower().Equals("false"))
            {
                hasExpectedResult = true;
            }
            else if (spaceSplit[spaceSplitLength - 1].ToLower().Equals("true"))
            {
                hasExpectedResult = true;
                expectedResult = true;
            }

            if (hasExpectedResult)
            {
                var newString = "";
                for (int j = 0; j < spaceSplit.Length - 1; j++)
                {
                    newString += spaceSplit[j] + " ";
                }

                Validator.ValidateStack_ExpectedOutput(newString, expectedResult);
            }
            else
            {
                Validator.ValidateStack_NoExpectedOutput(line);
            }
        }
    }
}
