using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] SpellManager spellManager;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = spellManager.GetPower().ToString("0.00");
    }
}
