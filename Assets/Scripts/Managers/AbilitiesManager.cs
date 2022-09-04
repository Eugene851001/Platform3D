using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;

public class AbilitiesManager : MonoBehaviour
{
    private HashSet<Ablilities> _ablilities = new HashSet<Ablilities>();

    public event Action<Ablilities> OnAbilityAdd;
    public event Action<Ablilities> OnAbilityRemove;

    public AbilityNode AbilitiesTree { get; private set; }

    private void Awake()
    {
        AbilitiesTree = new AbilityNode()
        {
            Name = "Root",
            Childs = new AbilityNode[]
            {
                new AbilityNode() 
                { 
                    Name = "Movement", 
                    NodeType = NodeType.Group,
                    Childs = new AbilityNode[]
                    {
                        new AbilityNode() 
                        { 
                            Name = "Double Jump", 
                            NodeType = NodeType.Ability,
                            Value = Ablilities.DoubleJump,
                        },
                        new AbilityNode()
                        {
                            Name = "Teleport",
                            NodeType = NodeType.Ability,
                            Value = Ablilities.Teleport,
                        }
                    }
                },
                new AbilityNode()
                {
                    Name = "Force",
                    NodeType = NodeType.Group,
                    Childs = new AbilityNode[]
                    {
                        new AbilityNode()
                        {
                            Name = "Move object",
                            NodeType = NodeType.Ability,
                            Value = Ablilities.MoveObjects,
                        },
                        new AbilityNode()
                        {
                            Name = "Blow object",
                            NodeType = NodeType.Ability,
                            Value = Ablilities.BlowObjects,
                        }
                    }
                }
            }
        };

        AbilitiesTree.InitParents();
    }

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
