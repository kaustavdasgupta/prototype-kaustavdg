using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;
    [SerializeField] private Button button;
    [SerializeField] private float flipDuration = 0.2f;

    public CardModel Model { get; private set; }
    public event Action<CardView> OnClicked;
    private bool isFlipping;

    public void Init(CardModel model, Sprite sprite)
    {
        Model = model;
        front.GetComponent<Image>().sprite = sprite;
        ShowBackInstant();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (isFlipping || Model.IsMatched) return;

        OnClicked?.Invoke(this);
    }

    public IEnumerator FlipToFront()
    {
        yield return Flip(true);
    }

    public IEnumerator FlipToBack()
    {
        yield return Flip(false);
    }

    private IEnumerator Flip(bool showFront)
    {
        isFlipping = true;

        float t = 0f;

        while (t < flipDuration)
        {
            t += Time.deltaTime;
            float y = Mathf.Lerp(0f, 90f, t / flipDuration);
            transform.localRotation = Quaternion.Euler(0, y, 0);
            yield return null;
        }

        front.SetActive(showFront);
        back.SetActive(!showFront);

        t = 0f;

        while (t < flipDuration)
        {
            t += Time.deltaTime;
            float y = Mathf.Lerp(90f, 0f, t / flipDuration);
            transform.localRotation = Quaternion.Euler(0, y, 0);
            yield return null;
        }

        isFlipping = false;
    }

    private void ShowBackInstant()
    {
        front.SetActive(false);
        back.SetActive(true);
        transform.localRotation = Quaternion.identity;
    }

    public void SetMatchedVisual()
    {
        button.interactable = false;
        button.GetComponent<Image>().enabled = false;
        front.SetActive(false);
        back.SetActive(false);
    }
}



