using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    [SerializeField] private StageSO _stageData;

    public int MainNumber { get; private set; }
    public int SubNumber { get; private set; }
}
