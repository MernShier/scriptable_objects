using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnter : MonoBehaviour
{
    public int Number = 0;
    [SerializeField] private GameObject Manager;
    private void Awake()
    {
        Manager.GetComponent<InventoryManager>().MouseExitText();
    }
    private void OnMouseEnter()
    {
        //Debug.Log("MouseEnter");
        Manager.GetComponent<InventoryManager>().MouseEnterText(Number);
    }
    private void OnMouseExit()
    {
        //Debug.Log("MouseExit");
        Manager.GetComponent<InventoryManager>().MouseExitText();
    }
    private void OnMouseDown()
    {
        //Debug.Log("MouseDown");
        Manager.GetComponent<InventoryManager>().MouseDown(Number);
    }
}
