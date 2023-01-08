using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PetrushevskiApps.Utilities;

public class ConnectionManger : MonoBehaviour
{
    public ConnectivityManager _manger;
    public bool isInternet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isInternet = _manger.IsConnected;
    }
}
