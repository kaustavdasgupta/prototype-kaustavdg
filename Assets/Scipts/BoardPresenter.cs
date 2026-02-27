using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardPresenter : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TMP_InputField rowsCount;
    [SerializeField] private TMP_InputField colsCount;
    [SerializeField] private GameObject errorMessage;
    [SerializeField] private List<Sprite> frontImages;

    private int rows;
    private int cols;

    private List<CardView> spawnedCards = new();
    private CardView first;
    private CardView second;
    private bool isResolving;


    public void StartGame()
    {
        rows = int.Parse(rowsCount.text);
        cols = int.Parse(colsCount.text);
        int totalcards = rows * cols;

        if (totalcards % 2 != 0)
        {
            errorMessage.SetActive(true);
            return;
        }
        else
        {
            errorMessage.SetActive(false);
        }

        boardManager.GenerateBoard(rows, cols);
        SpawnCards(rows * cols);
        AssignModels();
    }

    private void SpawnCards(int count)
    {
        spawnedCards.Clear();

        foreach (Transform child in boardManager.BoardGrid.transform)
        {
            var view = child.GetComponent<CardView>();
            view.OnClicked += OnCardClicked;
            spawnedCards.Add(view);
        }
    }

    private void AssignModels()
    {
        int pairCount = spawnedCards.Count / 2;
        List<int> ids = new();

        for (int i = 0; i < pairCount; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        for (int i = 0; i < ids.Count; i++)
        {
            int rand = Random.Range(i, ids.Count);
            (ids[i], ids[rand]) = (ids[rand], ids[i]);
        }

        for (int i = 0; i < spawnedCards.Count; i++)
        {
            int id = ids[i];
            if (id >= frontImages.Count)
            {
                Debug.LogError("Not enough Front Images!");
                return;
            }
            spawnedCards[i].Init(new CardModel(id), frontImages[id]);
        }
    }

    private void OnCardClicked(CardView card)
    {
        if (isResolving) return;
        if (card == first) return;

        StartCoroutine(HandleFlip(card));
    }

    private IEnumerator HandleFlip(CardView card)
    {
        yield return card.FlipToFront();

        if (first == null)
        {
            first = card;
            yield break;
        }

        second = card;
        yield return ResolveMatch();
    }

    private IEnumerator ResolveMatch()
    {
        isResolving = true;
        yield return new WaitForSeconds(0.3f);

        if (first.Model.Id == second.Model.Id)
        {
            first.Model.IsMatched = true;
            second.Model.IsMatched = true;
        }
        else
        {
            yield return first.FlipToBack();
            yield return second.FlipToBack();
        }

        first = null;
        second = null;
        isResolving = false;
    }
}


