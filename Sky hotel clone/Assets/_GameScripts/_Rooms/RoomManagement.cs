using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour
{

    [SerializeField] private List<GameObject> roomList = new List<GameObject>();
    public static RoomManagement Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void createRoomList()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf && !roomList.Contains(child.gameObject))
                roomList.Add(child.gameObject);
        }
    }

    public Transform IsroomAvailable()
    {
        foreach (var child in roomList)
        {
             if(child.GetComponent<Room>().roomState == RoomState.available)
                return child.transform;
            
        }

        return null;


    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
