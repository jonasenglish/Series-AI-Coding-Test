using UnityEngine;
using System.IO;
using TMPro;
using SimpleFileBrowser;
using System.Collections;

public class InputFileHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_FileName;

    StackValidator Validator => StackValidator.Instance;

    private string m_FilePath = "";

    public void OpenFileBrowser()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Text Files", ".txt"));
        FileBrowser.SetDefaultFilter(".txt");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            m_FilePath = FileBrowser.Result[0];
            m_FileName.text = m_FilePath;
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
