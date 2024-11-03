using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class GeneralResourceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    [SerializeField] private float radiousCollectDistance = 2f;
    [SerializeField] private bool isRadiousEnabled = true;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
    }


    public void ResourceClicked(ResourceType type, Vector2 position)
    {
        AddResourceToInventory(type);

        if (isRadiousEnabled)
        {
            Collider2D[] collision = Physics2D.OverlapCircleAll(position, radiousCollectDistance);

            foreach (Collider2D collision2d in collision)
            {
                ResourceEntity resource = collision2d.GetComponent<ResourceEntity>();

                if(resource != null)
                {
                    if (!resource.IsCollected())
                    {
                        AddResourceToInventory(resource.GetResourceType());
                        Destroy(resource.gameObject);
                    }
                }
            }
        }

    }

    private void AddResourceToInventory(ResourceType type)
    {
        inventoryManager.ClassifyResource(type);
    }

}

public enum ResourceType
{
    EssenceCrystal,
    Gold,
    InfernoEmber,
    FrozenShard,
    VenomGland
}
