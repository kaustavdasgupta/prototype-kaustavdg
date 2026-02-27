using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboView : MonoBehaviour
{
    [SerializeField] private BoardPresenter presenter;
    [SerializeField] private TMP_Text comboText;

    private void Awake()
    {
        comboText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        presenter.OnComboChanged += UpdateCombo;
    }

    private void OnDisable()
    {
        presenter.OnComboChanged -= UpdateCombo;
    }

    private void UpdateCombo(int combo)
    {
        if (combo <= 1)
        {
            comboText.gameObject.SetActive(false);
            return;
        }

        comboText.gameObject.SetActive(true);
        comboText.text = $"COMBO x{combo}";
    }

    private void HideCombo()
    {
        comboText.gameObject.SetActive(false);
    }
}


