using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("How much score points the player gets when this target is killed")]
    [SerializeField] int scoreValue;
    [Header("ObjectReferences")]
    [SerializeField] ParticleSystem explosionParticle;

    Rigidbody rb;

    GameManager manager;

    float minSpeed = 12f;
    float maxSpeed = 16f;
    float maxTorque = 10f;
    float xRange = 4f;
    float ySpawnPos = -2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPos();

        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private void OnMouseDown()
    {
        if (manager.isGameActive && !manager.isPaused)
        {
            manager.UpdateScore(scoreValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
            if (gameObject.CompareTag("Bad"))
            {
                manager.SubtractLife();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && manager.isGameActive)
        {
            manager.SubtractLife();
        }
    } 
}
