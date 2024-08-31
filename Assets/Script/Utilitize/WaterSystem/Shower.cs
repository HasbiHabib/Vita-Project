using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using vector2 = UnityEngine.Vector2;

public class Shower : MonoBehaviour
{
    // Get the Simulation object
    public GameObject Simulation;
    // Get the Base_Particle object from Scene
    public GameObject Base_Particle;
    public float spawn_rate = 1f;
    private float time;


    // Update is called once per frame
    void Update()
    {
        // Limit the number of particles
        if (Simulation.transform.childCount < 1000)
        {
            // Spawn particles at a constant rate
            time += Time.deltaTime;
            if (time < 1.0f / spawn_rate)
            {
                return;
            }
            // Create a new particle at the current position of the object
            GameObject new_particle = Instantiate(Base_Particle, transform.position, Quaternion.identity);

            // Set as child of the Simulation object
            new_particle.transform.parent = Simulation.transform;

            // Reset time
            time = 0.0f;
        }
    }
}
