using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInputGrid : MonoBehaviour
{
    private TMP_InputField currentInputField;
    [SerializeField] private bool isRow;

    private int minSize = 1;
    private int maxSizeRow = 4;
    private int maxSizeCol = 5;

    private void OnEnable()
    {
        currentInputField = this.GetComponent<TMP_InputField>();
    }

    public void OnValueChange()
    {
        if (currentInputField == null || string.IsNullOrEmpty(currentInputField.text))
            return;

        if (isRow)
        {
            if (int.Parse(currentInputField.text) <= 0)
                currentInputField.text = minSize.ToString();
            else if (int.Parse(currentInputField.text) > maxSizeRow)
                currentInputField.text = maxSizeRow.ToString();
        }
        else
        {
            if (int.Parse(currentInputField.text) <= 0)
                currentInputField.text = minSize.ToString();
            else if (int.Parse(currentInputField.text) > maxSizeCol)
                currentInputField.text = maxSizeCol.ToString();
        }
    }
}




