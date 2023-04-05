using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component destroys its object whenever it triggers a 2D collider with the given tag.
 */
public class DestroyOnTrigger2D : MonoBehaviour
{
    [Tooltip("Every object tagged with this tag will trigger the destruction of this object")]
    [SerializeField] List<string> triggeringTags;
    public int hits = 1;
    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (triggeringTags.Contains(other.tag) && enabled)
        {
            hits--;
            if (hits == 0)
            {
                // show explosion
                Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
                ShipCamera cameraTracking = gameObject.GetComponent<ShipCamera>();
                if (cameraTracking && cameraTracking.target_object == gameObject)
                {
                    cameraTracking.enabled = false; // disable camera tracking explosion if its tracking this object
                }
                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        /* Just to show the enabled checkbox in Editor */
    }


}
