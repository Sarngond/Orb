using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonData
{
    public string buttonActive;

    public ButtonData(GameObject button) {

        buttonActive = button.GetComponent<DoorButton>().isPushedString;

    }
}
