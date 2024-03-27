using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SOintUpdate : MonoBehaviour
{
    public SOint soInt;
    public TextMeshProUGUI uiTextValue;

    void Start()
    {
        uiTextValue.text = soInt.value.ToString();
    }

    void Update()
    {
          uiTextValue.text = soInt.value.ToString();
    }
}
