using UnityEngine;

public class Interactable : MonoBehaviour
{
    public int interactionTime;
    
    private GameObject _circleCursor;
    
    protected virtual void Awake()
    {
        _circleCursor = transform.Find("CircleCursor").gameObject;
    }

    public void Select()
    {
        _circleCursor.SetActive(true);
    }

    public void Deselect()
    {
        _circleCursor.SetActive(false);
    }

    public virtual void Interact()
    {
        
    }
}
