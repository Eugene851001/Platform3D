using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilitiesManager : MonoBehaviour
{
    private HashSet<Ablilities> _ablilities = new HashSet<Ablilities>();

    public event Action<Ablilities> OnAbilityAdd;
    public event Action<Ablilities> OnAbilityRemove;

    public void Add(Ablilities ability)
    {
        _ablilities.Add(ability);

        OnAbilityAdd?.Invoke(ability);
    }

    public void Remove(Ablilities ability)
    {
        _ablilities.Remove(ability);

        OnAbilityRemove?.Invoke(ability);
    }

    public bool Contains(Ablilities ability) => _ablilities.Contains(ability);
}
