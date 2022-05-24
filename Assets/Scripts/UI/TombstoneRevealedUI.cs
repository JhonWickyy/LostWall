using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class TombstoneRevealedUI : MonoBehaviour
{
    public Button[] TombStones;
    public RectTransform[] Slots;
    private int[] finalSlotTombStones;
    private int[] slotTombStones = new int[5];
    private int curSelect = -1;
    private RectTransform rectTransform;
    Vector2 pos = Vector2.one;

    public void SetFinalTombstone(string org_slots, string end_slots)
    {
        string[] arr = end_slots.Split(',');
        if (arr != null && arr.Length == 5)
        {
            finalSlotTombStones = new int[5];
            for (int i = 0; i < arr.Length; i++)
            {
                finalSlotTombStones[i] = int.Parse(arr[i]) - 1;
            }
        }
        
        arr = org_slots.Split(',');
        if (arr != null && arr.Length == 5)
        {
            slotTombStones = new int[5];
            for (int i = 0; i < arr.Length; i++)
            {
                int index = int.Parse(arr[i]) - 1;
                slotTombStones[i] = index;
                TombStones[index].GetComponent<RectTransform>().localPosition = Slots[i].localPosition;
            }
        }
    }
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        for (int i = 0; i < TombStones.Length; i++)
        {
            int index = i;
            TombStones[i].onClick.AddListener(() =>
            {
                setSelect(index);
            });
        }

        for (int i = 0; i < Slots.Length; i++)
        {
            int index = i;
            Slots[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                setSlot(index);
                releaseSelect(index);
            });
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (curSelect > -1)
        {
            var mousePos = Input.mousePosition;
            mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width); 
            mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height); 
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, Camera.current, out pos))
                TombStones[curSelect].transform.localPosition = pos;

            if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
            {
                releaseSelect(-1);
            }
        }
    }

    private void setSelect(int index)
    {
        if (curSelect > -1)
        {
            TombStones[curSelect].GetComponent<RectTransform>().localPosition = TombStones[index].GetComponent<RectTransform>().localPosition;
            TombStones[curSelect].GetComponent<Image>().raycastTarget = true;
            
            for (int i = 0; i < slotTombStones.Length; i++)
            {
                if (slotTombStones[i] == index)
                    slotTombStones[i] = curSelect; 
            }
        }
        
        TombStones[index].GetComponent<Image>().raycastTarget = false;
        curSelect = index;  
    }

    private void releaseSelect(int slotIndex)
    {
        if (curSelect > -1)
        {
            if (slotIndex > -1)
            {
                TombStones[curSelect].GetComponent<RectTransform>().localPosition = Slots[slotIndex].localPosition;
            }
            TombStones[curSelect].GetComponent<Image>().raycastTarget = true;
            curSelect = -1; 
        }
    }

    private void setSlot(int slotIndex)
    {
        if (curSelect > -1)
        {
            slotTombStones[slotIndex] = curSelect;
        }

        bool bEnd = true;
        for (int i = 0; i < slotTombStones.Length; i++)
        {
            bEnd &= slotTombStones[i] == finalSlotTombStones[i];
        }

        if (bEnd)
        {
            PlayerMenus.GetMenuWithName("TombstoneRevealedUI").TurnOff();
        }
    }
}
