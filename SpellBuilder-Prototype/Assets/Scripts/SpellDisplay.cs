using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    SpellManager spellManager;


    // Start is called before the first frame update
    void Start()
    {
        spellManager = GetComponent<SpellManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = spellManager.GetPower().ToString("0.00");
    }
}
