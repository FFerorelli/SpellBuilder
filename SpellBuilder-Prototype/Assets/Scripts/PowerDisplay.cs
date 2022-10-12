using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI powerText;
    Enemy enemyObject;


    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        powerText.text = enemyObject.GetPower().ToString("0.00");
    }
}
