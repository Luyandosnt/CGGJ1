using UnityEngine;
using static GeneralResourceController;

public class GhostResource : MonoBehaviour
{
    private int amount = 0;                  // Amount of resource to add
    public float speed = 5f;                 // Movement speed of the ghost resource
    private ResourceType resourceType;        // Type of resource being collected

    public void CollectResource()
    {
        // Add the resource amount to the inventory
        GeneralResourceController.Instance.ResourceClicked(resourceType, amount);

        // Destroy the ghost resource object after reaching its destination
        Destroy(gameObject);
    }

    public void SetResource(ResourceType type, int amount, Transform destination)
    {
        resourceType = type;
        this.amount = amount;

        // Animate movement towards the destination with easing effect
        LeanTween.move(gameObject, destination, speed)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(() => CollectResource());
    }

}
