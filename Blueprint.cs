using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public Material canBuildMaterial;
    public Material canNotBuildMaterial;

    public bool canBuild;

    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.material = canBuildMaterial;
        canBuild = true;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Building"), LayerMask.NameToLayer("Terrain"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Building"), LayerMask.NameToLayer("BuildableTerrain"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Building"), LayerMask.NameToLayer("Default"));
    }

    private void OnTriggerEnter(Collider other)
    {
        rend.material = canNotBuildMaterial;
        canBuild = false;
    }

    private void OnTriggerExit(Collider other)
    {
        rend.material = canBuildMaterial;
        canBuild = true;
    }


}
