using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkopoloManager : MonoBehaviour
{
    [SerializeField] private int minNumber = 1;
    [SerializeField] private int maxNumber = 100;
    [Tooltip("Displays Marko if the current number is divided by the value")]
    [SerializeField] private int dividedByMarko = 3;
    [Tooltip("Displays Polo if the current number is divided by the value")]
    [SerializeField] private int dividedByPolo = 5;

    private void Start() {
        for (int i = minNumber; i <= maxNumber; i++) {
            if (i%dividedByMarko == 0 && i%dividedByPolo == 0) { Debug.Log($"{i} - MarkoPolo"); }
            else {
                if (i%dividedByMarko == 0) { Debug.Log($"{i} - Marko"); }
                if (i%dividedByPolo == 0) { Debug.Log($"{i} - Polo"); }
            }
        }
    }
}
