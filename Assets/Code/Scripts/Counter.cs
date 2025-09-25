using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private int moves = 0, record = 0;
    [SerializeField] private Text text;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("record"))
        {
            record = 0;
            PlayerPrefs.SetInt("record", record);
        }
        else
        {
            record = PlayerPrefs.GetInt("record");
        }
    }

    public void Add()
    {
        moves++;
    }

    private void UpdateText()
    {
        text.text = moves + "\n";
        if (record != 0)
        {
            text.text += "Record: " + record;
        }
    }
}