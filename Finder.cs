using System.Collections.Generic;
using UnityEngine;

public static class Finder 
{
    //Ignores all other layes than the layerName
    public static (Vector3, bool) GetMousePosAtLayer(string layerName)
    {
        Vector3 mousePos;
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out rayHit, 200f,  (1 << LayerMask.NameToLayer(layerName))) && rayHit.transform.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            mousePos = rayHit.point;
            return (mousePos, true);
        }
        return (new Vector3(0,0,0), false);
    }

    public static T GetObject<T>(string layerName) where T : class
    {
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit) && rayHit.transform.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            return rayHit.transform.gameObject.GetComponent<T>() ?? rayHit.transform.gameObject.GetComponentInParent<T>();
        }
        return null;
    }


    public static (Vector3, bool) GetMousePosAtLayerOld(string layerName)
    {
        Vector3 mousePos;
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit) && rayHit.transform.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            mousePos = rayHit.point;
            return (mousePos, true);
        }
        return (new Vector3(0, 0, 0), false);
    }

}
