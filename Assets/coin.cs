using UnityEngine;
using UnityEngine.Events;
public class coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float multiplier;
   public UnityEvent gotCoin = new();
    void Start()
    {
         multiplier = Random.Range(1, 3);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 100*multiplier * Time.deltaTime);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            gotCoin.Invoke();
            Destroy(this.gameObject);
        }
    }
}
