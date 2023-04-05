using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class ShieldThePlayer : MonoBehaviour
{
    [Tooltip("The number of seconds that the shield remains active")][SerializeField] float duration;
    private SpriteRenderer shieldIndicator;

    private void Start()
    {
        shieldIndicator = GameObject.FindWithTag("ShieldIndicator").GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Shield triggered by player");
            var destroyComponent = other.GetComponent<DestroyOnTrigger2D>();

            if (destroyComponent)
            {
                Destroy(gameObject);  // Destroy the shield itself - prevent double-use

                destroyComponent.StopAllCoroutines(); // stop all corutines if exists, 
                destroyComponent.StartCoroutine(ShieldTemporarily(destroyComponent));        // co-routines
            }
        }
        else
        {
            Debug.Log("Shield triggered by " + other.name);
        }
    }

    private IEnumerator ShieldTemporarily(DestroyOnTrigger2D destroyComponent)
    {   // co-routines
        // private async void ShieldTemporarily(DestroyOnTrigger2D destroyComponent) {      // async-await
        destroyComponent.enabled = false;
        // enable the shield indicator 
        shieldIndicator.enabled = true;
        shieldIndicator.color = new Color(shieldIndicator.color.r, shieldIndicator.color.g, 1);
        float waitingTime = Time.deltaTime;

        for (float i = duration; i > 0; i -= waitingTime)
        {
            yield return new WaitForSeconds(waitingTime);       // wait
            shieldIndicator.color = new Color(shieldIndicator.color.r, shieldIndicator.color.g, shieldIndicator.color.b, i / duration);
        }
        Debug.Log("Shield gone!");
        shieldIndicator.enabled = false;
        destroyComponent.enabled = true;
    }
}
