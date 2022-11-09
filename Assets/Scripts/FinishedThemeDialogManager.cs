using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedThemeDialogManager : MonoBehaviour
{
    [SerializeField] GameObject[] GiftBraceletArray;
    [SerializeField] GameObject[] GiftRingArray;
    [SerializeField] Transform braceletSpawnPoint;
    [SerializeField] Transform ringSpawnPoint;
    GameObject ring;
    GameObject bracelet;
    // Start is called before the first frame update

    void Start()
    {
        bracelet = Instantiate(GameDataManager.Instance.GetGiftBracelet(), braceletSpawnPoint.position, braceletSpawnPoint.rotation, braceletSpawnPoint.parent);
        ring =Instantiate(GameDataManager.Instance.GetGiftRing(), ringSpawnPoint.position, ringSpawnPoint.rotation, ringSpawnPoint.parent);
        //make them rotate
        ring.AddComponent<RotateFurniture>();
        bracelet.AddComponent<RotateFurniture>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
