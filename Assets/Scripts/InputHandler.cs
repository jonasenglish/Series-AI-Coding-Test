using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField m_Input;

    StackValidator Validator => StackValidator.Instance;

    public void OnClick_ValidateStack()
    {
        Validator.ValidateStack_NoExpectedOutput(m_Input.text);
    }

}
