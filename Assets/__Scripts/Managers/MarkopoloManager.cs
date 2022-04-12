using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarkopoloManager : MonoBehaviour
{
    [SerializeField] private int minNumber = 1;
    [SerializeField] private int maxNumber = 100;
    [Tooltip("Displays Marko if the current number is divided by the value")]
    [SerializeField] private int dividedByMarko = 3;
    [Tooltip("Displays Polo if the current number is divided by the value")]
    [SerializeField] private int dividedByPolo = 5;

    private List<int> markoNumbers = new List<int>();
    private List<int> poloNumbers = new List<int>();
    private List<int> markopoloNumbers = new List<int>();

    [SerializeField] private GameObject specificInfoObject = null;
    [SerializeField] private TMP_Text displaySpecificInfo = null;
    [SerializeField] private TMP_Text displaySpecificInfoHeader = null;

    // private void Start() {
    //     for (int i = minNumber; i <= maxNumber; i++) {
    //         if (i%dividedByMarko == 0 && i%dividedByPolo == 0) { Debug.Log($"{i} - MarkoPolo"); }
    //         else {
    //             if (i%dividedByMarko == 0) { Debug.Log($"{i} - Marko"); }
    //             if (i%dividedByPolo == 0) { Debug.Log($"{i} - Polo"); }
    //         }
    //     }
    // }
    public void ButtonOnDoTheMath() {
        for (int i = minNumber; i <= maxNumber; i++) {
            if (i%dividedByMarko == 0 && i%dividedByPolo == 0) { 
                markopoloNumbers.Add(i);
            }
            else {
                if (i%dividedByMarko == 0) { 
                    markoNumbers.Add(i);
                }
                if (i%dividedByPolo == 0) { 
                    poloNumbers.Add(i);
                }
            }
        }
    }
    public void ButtonOnMarkopolo() {
        string markoPoloText = "";
        foreach (var item in markopoloNumbers) {
            markoPoloText += $"{item}\n";
        }
        SetSpecificInfo(markoPoloText, "MarkoPolo");
    }
    public void ButtonOnMarko() {
        string markoText = "";
        foreach (var item in markoNumbers) {
            markoText += $"{item}\n";
        }
        SetSpecificInfo(markoText, "Marko");
    }
    public void ButtonOnPolo() {
        string poloText = "";
        foreach (var item in poloNumbers) {
            poloText += $"{item}\n";
        }
        SetSpecificInfo(poloText, "Polo");
    }

    private void SetSpecificInfo(string text, string header) {
        specificInfoObject.SetActive(true);
        displaySpecificInfoHeader.text = header;
        displaySpecificInfo.text = text;
    }
}
