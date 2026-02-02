
using UnityEngine;

public interface IUpgradable 
{
    int Level { get; }

    public void Upgrade() { }
}