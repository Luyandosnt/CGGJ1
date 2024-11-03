using UnityEngine;

public class GhostResource : MonoBehaviour
{
    private int amount = 0;                  // Amount of resource to add
    public Vector2 destination;              // Destination for the ghost resource
    public float speed = 5f;                 // Movement speed of the ghost resource
    private ResourceType resourceType;        // Type of resource being collected

    private void Start()
    {
        // Animate movement towards the destination with easing effect
        LeanTween.move(gameObject, destination, speed)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(() => CollectResource());
    }

    public void CollectResource()
    {
        // Add the resource amount to the inventory
        GeneralResourceController.Instance.ResourceClicked(resourceType, transform.position, amount);

        // Destroy the ghost resource object after reaching its destination
        Destroy(gameObject);
    }

    public void SetResource(ResourceType type, int amount)
    {
        resourceType = type;
        this.amount = amount;
    }

}
