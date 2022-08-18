using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ButtonStartHost()
    {
        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Start Host");
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Ќе удалось создать хост, возможно он уже был создан");
        }
    }

    public void ButtonStartClient()
    {
        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Start Client");
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Ќе удалось присоединитьс€ к хосту");
        }
    }
}
