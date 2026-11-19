using System.Collections;
using UnityEngine;

public class ParticleInstatiator : MonoBehaviour
{
    [SerializeField] ParticleSystem particlesPrefab;

    public void Emit()
    {
        ParticleSystem particles = Instantiate(particlesPrefab);
        particles.transform.position = transform.position;
        particles.Emit(9);
        StartCoroutine(DestroyEmitter(particles.gameObject));
    }

    IEnumerator DestroyEmitter(GameObject emitter)
    {
        float time = 1;
        float timer= 0;
        while(time > timer)
        {
            timer+= Time.deltaTime;
            yield return null;
        }
        Destroy(emitter);
    }
}
