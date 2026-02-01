using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject standing, destroyed;

    private void Awake()
    {
        standing.SetActive(true);
        destroyed.SetActive(false);
    }

    public void Destroy()
    {
        standing.SetActive(false);
        destroyed.SetActive(true);
    }
}
