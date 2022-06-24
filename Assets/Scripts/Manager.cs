using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public void SetTrue(GameObject setTrue)
    {
        setTrue.SetActive(true);
    }
    public void SetFalse(GameObject setFalse)
    {
        setFalse.SetActive(false);
    }
    public void SetReverse(GameObject setReverse)
    {
        setReverse.SetActive(!setReverse.activeSelf);
    }
    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
