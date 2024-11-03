using UnityEngine;

public class ResourceEntity : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int amount = 1;
    [SerializeField] private GhostResource ghostResource; // Optional ghost resource prefab

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after lifetime if not collected
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if mouse is over the resource
        if (GetComponent<Collider2D>().OverlapPoint(mousePos))
        {
            CollectResource();
        }
    }

    private void CollectResource()
    {
        // Instantiate a ghost resource (visual feedback)
        if (ghostResource != null)
        {
            // Convert the resource's world position to a screen position
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

            // Instantiate the ghost resource as a child of the UI container
            GhostResource GR = Instantiate(ghostResource, GeneralResourceController.Instance.ghostContainer);
            GR.SetResource(type, amount);

            // Set the ghost resource's UI position
            GR.transform.position = screenPosition;
        }

        // Destroy the original resource object
        Destroy(gameObject);
    }

    public ResourceType GetResourceType()
    {
        return type;
    }
}
