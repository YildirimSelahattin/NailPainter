using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rateText;
    // Start is called before the first frame update
   
    void Start()
    {
        rateText.text = "%"+GameManager.Instance.matchRate.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
