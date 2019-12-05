using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyCardData
{
    public string cardObtained;

    public KeyCardData(GameObject card) {

        cardObtained = card.GetComponent<KeyCard>().obtainedString;

    }
}
