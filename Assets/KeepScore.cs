using UnityEngine;
using TMPro;
public class KeepScore : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private coin[] coins;
    void Start()
    {
        coins = FindObjectsByType<coin>(FindObjectsInactive.Include, FindObjectsSortMode.None); ;
        foreach (coin coin in coins)
        {
           coin.gotCoin.AddListener(gotCoin);
        }
  
  

    }
    private void gotCoin()
    {
        score++;
        scoreText.text = $"Score: {score}"+"/"+coins.Length;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
