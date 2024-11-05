using UnityEngine;
using static GeneralResourceController;

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
        // Define a layer mask for the resource objects
        int resourceLayerMask = LayerMask.GetMask("Pickup");

        // Cast a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits an object on the resource layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, resourceLayerMask))
        {
            // Ensure the object hit is this resource entity
            if (hit.collider.gameObject == gameObject)
            {
                CollectResource();
            }
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
            GhostResource GR = Instantiate(ghostResource, Instance.ghostContainer);
            GR.SetResource(type, amount, Instance.ghostResourceDestinations[Instance.GetResourceIndex(type)]);

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
