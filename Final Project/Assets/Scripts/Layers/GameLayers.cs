using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask characterLayer;
    [SerializeField] LayerMask solidObjectLayer;
    [SerializeField] LayerMask triggerableLayer;

    public static GameLayers i { get; set; }

    private void Awake()
    {
        i = this;
    }

    public LayerMask CharacterLayer
    {
        get => characterLayer;
    }

    public LayerMask SolidLayer
    {
        get => solidObjectLayer;
    }

    public LayerMask TriggerableLayer
    {
        get => triggerableLayer;
    }
}
