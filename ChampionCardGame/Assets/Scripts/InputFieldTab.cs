using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldTab : MonoBehaviour, IUpdateSelectedHandler
{
    public TMP_InputField nextField;

    public void OnUpdateSelected(BaseEventData data)
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            nextField.ActivateInputField();
        }
    }
}
