using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Chair : NetworkBehaviour
{
    [SerializeField] NetworkVariable<bool> _isBusy;

    public Transform PointSeat;
    public Renderer Circle;
    public Color ColorBusy;
    public Color ColorFree;
    // Start is called before the first frame update
    void Start()
    {
        Draw(false);
    }

    public override void OnNetworkSpawn()
    {
        _isBusy.OnValueChanged += OnChairChanged;
        Draw(_isBusy.Value);
    }

    void Draw(bool value)
    {
        Circle.material.color = value ? ColorBusy : ColorFree;
    }

    private void OnChairChanged(bool previousValue, bool newValue)
    {
        Draw(newValue);
    }

    public bool Seat()
    {
        if (_isBusy.Value)
            return false;

        BorrowServerRpc();
        return true;
    }

    public void GetUp()
    {
        ReleaseServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void BorrowServerRpc()
    {
        _isBusy.Value = true;
    }

    [ServerRpc(RequireOwnership = false)]
    void ReleaseServerRpc()
    {
        _isBusy.Value = false;
    }
}
