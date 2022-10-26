using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> roomParent;
    int mirrorParent;
    int windowParent;
    int flowerParent;
    int wallParent;
    int sofaParent;
    int komodinParent;
    int platformParent;
    int chairParent;
    int pictureParent;
    int tabureParent;
    int floorParent;

    Room room;
    void Start()
    {
        room = GameDataManager.Instance.dataLists.room;
        for(int i = 0;i< roomParent.Count; i++)
        {
            switch()
            roomParent[i].transform.GetChild()
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCurrentObjectIndexes()
    {
       
    }
}
