using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TakeChair : NetworkBehaviour
{
    [SerializeField]
    Chair _chair;
    Vector3 _lastPosition;

    public bool IsSeat { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        var chair = other.GetComponent<Chair>();
        if (chair != null)
            _chair = chair;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_chair == null) return;

        var chair = other.GetComponent<Chair>();
        if (chair != null)
            _chair = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_chair != null)
            {
                if (IsSeat)
                {
                    _chair.GetUp();
                    IsSeat = false;
                    transform.position = _lastPosition;
                }
                else
                {
                    if (_chair.Seat())
                    {
                        IsSeat = true;
                        _lastPosition = transform.position;
                        transform.position = _chair.PointSeat.position;
                    }
                    
                }
            }
        }
    }

}
