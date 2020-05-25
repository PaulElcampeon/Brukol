using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField]
    private float speed;
    private float counter;

    void Update()
    {
        counter += speed;
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z + counter);
    }
}
