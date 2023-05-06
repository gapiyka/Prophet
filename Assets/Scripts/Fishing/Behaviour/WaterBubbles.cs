using System.Collections;
using UnityEngine;
using Random = System.Random;

public class WaterBubbles : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _chanceToAppear;
    [SerializeField] private int _duration;
    [SerializeField] private int _appearInterval;

    private bool _showing;
    
    void Start()
    {
        StartCoroutine("CheckAppear");
    }

    private void Update()
    {
        _sprite.enabled = _showing;
    }

    private IEnumerator CheckAppear()
    {
        for (;;)
        {
            _showing = false;
            
            Random rand = new Random();
            float randomFloat = (float)rand.NextDouble();

            if (randomFloat < _chanceToAppear)
            {
                _showing = true;
                yield return new WaitForSeconds(_duration);
            }
            
            yield return new WaitForSeconds(_appearInterval);
        }
    }
}
