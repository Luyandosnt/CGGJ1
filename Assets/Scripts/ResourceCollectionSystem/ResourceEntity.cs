using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceEntity : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    private GeneralResourceController controller;
    private bool collected = false;


    private void Start()
    {
       controller = GameObject.FindGameObjectWithTag("ResourceController").GetComponent<GeneralResourceController>();

    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            collected = true;
            controller.ResourceClicked(type, transform.position);
            Destroy(gameObject);
        }
    }

    public bool IsCollected()
    {
        return collected;
    }

    public ResourceType GetResourceType()
    {
        return type;
    }

}



