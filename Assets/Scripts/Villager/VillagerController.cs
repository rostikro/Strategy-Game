using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{
    private static readonly int IsBuilding = Animator.StringToHash("IsBuilding");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
    private GameObject _circleCursor;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    private bool isWalking = false;
    
    private WorkType _workType;
    private bool workChanged = false;
    
    [CanBeNull] private Interactable _interactable;
    
    // Collection Work
    private ResourceType _heldResourceType = ResourceType.None;
    private int _heldResourceAmount = 0;
    
    // Food timer
    private float timer = 0f;
    private float interval = 20f;
    
    void Awake()
    {
        _circleCursor = transform.Find("CircleCursor").gameObject;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Select()
    {
        _circleCursor.SetActive(true);
    }

    public void Deselect()
    {
        _circleCursor.SetActive(false);
    }

    public void SetCollectWork(ResourceBase resource)
    {
        if (_interactable)
            _interactable.Deselect();
        
        _workType = WorkType.Collect;
        _interactable = resource;
        _heldResourceType = resource.resourceType;
        _heldResourceAmount = resource.collectionValue;
        resource.Select();
        
        SetDestination(_interactable.transform.position);
    }

    private void SetDestination(Vector3 position)
    {
        _agent.SetDestination(position);
        isWalking = true;
        _animator.SetBool(IsWalking, true);
    }

    private void CollectResource()
    {
        StartCoroutine(CollectRoutine());
    }
    
    private IEnumerator CollectRoutine()
    {
        Interactable currentResource = _interactable;
        
        _animator.SetBool(IsBuilding, true);
        
        yield return new WaitForSeconds(_interactable.interactionTime);
        
        _animator.SetBool(IsBuilding, false);

        // If work has been changed
        if (currentResource != _interactable)
            yield break;
        
        if (workChanged)
        {
            workChanged = false;
        }
        
        _interactable.Interact();
        
        // Bring resource to TownHall
        _workType = WorkType.Delivery;
        
        // A little hack to bring resources to different location around town hall.
        // Without that, villagers will just stop each other on bring point.
        
        Vector2 dir = Random.insideUnitCircle.normalized; // random direction
        Vector3 offset = new Vector3(dir.x, 0f, dir.y) * 5.0f;
        
        SetDestination(GameMode.Instance.townHallTransform.position + offset);
    }

    public void SetBuildingWork(Building building)
    {
        if (_interactable)
            _interactable.Deselect();

        Debug.Log(building.isInteractable);
        if (!building.isInteractable)
            return;
        
        _workType = WorkType.Build;
        _interactable = building;
        building.Select();
        
        SetDestination(_interactable.transform.position);
    }

    private void Build()
    {
        StartCoroutine(BuildRoutine());
    }
    
    private IEnumerator BuildRoutine()
    {
        Interactable currentBuild = _interactable;
        
        _animator.SetBool(IsBuilding, true);
        
        yield return new WaitForSeconds(_interactable.interactionTime);
        
        _animator.SetBool(IsBuilding, false);

        // If work has been changed
        if (currentBuild != _interactable)
            yield break;
        
        if (workChanged)
        {
            workChanged = false;
        }
        
        _interactable.Interact();
        _interactable.Deselect();

        _workType = WorkType.Idle;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Food timer
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            GameMode.Instance.ConsumeFood();
        }
        
        // Check if villager gets to work destination
        if (isWalking && !_agent.pathPending &&
            _agent.remainingDistance <= _agent.stoppingDistance)
        {
            OnDestinationReached();
        }
    }

    private void OnDestinationReached()
    {
        Debug.Log("Destination reached: " + gameObject.name);
        isWalking = false;
        _animator.SetBool(IsWalking, false);
        
        if (_workType == WorkType.Collect)
        {
            CollectResource();
        } else if (_workType == WorkType.Delivery)
        {
            if (_heldResourceType == ResourceType.Food)
                GameMode.Instance.BringFood(_heldResourceAmount);
            else if (_heldResourceType == ResourceType.Wood)
                GameMode.Instance.BringWood(_heldResourceAmount);
            else if (_heldResourceType == ResourceType.Stone)
                GameMode.Instance.BringStone(_heldResourceAmount);

            if (_interactable)
            {
                _workType = WorkType.Collect;
                SetDestination(_interactable.transform.position);
            }
            else
            {
                _workType = WorkType.Idle;
                
                // Hack: Walk away from TownHall to get ability other villagers to bring resources.
                SetDestination(new Vector3(5f, 0f, 0f));
            }
        } else if (_workType == WorkType.Build)
        {
            Build();
        }
    }
}

public enum WorkType
{
    Idle,
    Collect,
    Delivery,
    Build,
}
