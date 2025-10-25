using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EatFoodBehaviorTree : MonoBehaviour
{
    // Get referemces to other scripts
    private PlayerHunger _hunger;
    private Awareness _awareness;
    private Inventory _inventory;

    // Declare all the nodes of the BT
    private SequenceNode _rootNode;
    private ActionNode _anCheckHunger;
    private SelectorNode _selInventoryCheck;
    private ActionNode _anCheckMeat;
    private ActionNode _anCheckVegetable;
    private ActionNode _anCheckFruit;
    private InverterNode _inCheckEnemyProximity;
    private ActionNode _anCheckEnemyProximity;
    private ActionNode _anEatFood;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _awareness = GetComponent<Awareness>();
        _hunger = GetComponent<PlayerHunger>();
    }

    private void Start()
    {
        // Build the behavior tree
        // Use the constructors to build up instances of each nodes

        _anEatFood = new ActionNode(EatFood);
        _anCheckEnemyProximity = new ActionNode(CheckForEnemies);
        _anCheckFruit = new ActionNode(CheckForFruit);
        _anCheckVegetable = new ActionNode(CheckForVegetable);
        _anCheckMeat = new ActionNode(CheckForMeat);
        _anCheckHunger = new ActionNode(CheckHunger);

        // Define Inverter
        _inCheckEnemyProximity = new InverterNode(_anCheckEnemyProximity);

        // Define inventory selector
        List<Node> selectorChildren = new()
        {
            _anCheckFruit,
            _anCheckVegetable,
            _anCheckMeat
        };
        // Initialize with constructor
        _selInventoryCheck = new SelectorNode(selectorChildren);

        // Store all nodes into the root node
        List<Node> rootChildren = new()
        {
            _anCheckHunger,
            _selInventoryCheck,
            _inCheckEnemyProximity,
            _anEatFood
        };
        _rootNode = new SequenceNode(rootChildren);
    }

    private void Update()
    {
        _rootNode.Evaluate();
    }

    private NodeState EatFood()
    {
        _hunger.IncreaseHunger(50);
        return NodeState.SUCCESS;
    }

    private NodeState CheckForEnemies()
    {
        return _awareness.IsNearEnemy() ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private NodeState CheckForMeat()
    {
        return _inventory.CheckInventory("Meat");
    }
    private NodeState CheckForVegetable()
    {
        return _inventory.CheckInventory("Vegetable");
    }
    private NodeState CheckForFruit()
    {
        return _inventory.CheckInventory("Fruit");
    }

    private NodeState CheckHunger()
    {
        return _hunger.IsHungry ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}


