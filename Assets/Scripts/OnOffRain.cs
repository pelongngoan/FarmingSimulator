using System.Collections;
using UnityEngine;

public class OnOffRain : MonoBehaviour
{
    private ParticleSystem abc;

    void Start()
    {
        abc = GetComponent<ParticleSystem>();

        StartCoroutine(StartRainLoop());
    }

    IEnumerator StartRainLoop()
    {
        while (true)
        {
            abc.Play();

            yield return new WaitForSeconds(10f);

            abc.Stop();

            yield return new WaitForSeconds(10f);
        }
    }
}
