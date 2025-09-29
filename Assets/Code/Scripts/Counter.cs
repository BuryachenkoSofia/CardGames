using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Counter : MonoBehaviour
{
    private int moves = 0, record = 0;
    [SerializeField] private Text text, recordText;
    [SerializeField] private List<GameObject> foundationDropZones = new List<GameObject>();
    public bool win = false;

    private void Awake()
    {
        win = false;
        if (!PlayerPrefs.HasKey("record"))
        {
            record = 0;
            PlayerPrefs.SetInt("record", record);
        }
        else
        {
            record = PlayerPrefs.GetInt("record");
        }
        UpdateText();
    }

    private void Update()
    {
        if (Check())
        {
            win = true;
        }
        if (win)
        {
            if (moves < record || record == 0)
            {
                record = moves;
                PlayerPrefs.SetInt("record", record);
                UpdateText();
            }
            DisableAllCardsDrag();
        }
    }

    public void Add()
    {
        moves++;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = moves + "\n";
        if (record != 0)
        {
            recordText.text = "Record: " + record;
        }
    }

    private bool Check()
    {
        int k = 0;
        foreach (GameObject gameObject in foundationDropZones)
        {
            if (gameObject.GetComponent<FoundationDropZone>().transform.childCount == 13) k++;
        }
        return k == 4;
    }

    private void DisableAllCardsDrag()
    {
        CardDragHandler[] allCards = GameObject.FindObjectsByType<CardDragHandler>(FindObjectsSortMode.None);
        foreach (CardDragHandler card in allCards)
        {
            card.enabled = false;
        }
    }
}