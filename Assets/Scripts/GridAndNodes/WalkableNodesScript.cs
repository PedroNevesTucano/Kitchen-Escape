using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableNodesScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] private Material SelectedMaterial;
    [SerializeField] private Material UnSelectedMaterial;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask pathLayerMask = LayerMask.GetMask("Path");

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, pathLayerMask))
        {
            if (hit.collider.gameObject == gameObject)
            {
                rend.material = SelectedMaterial;
            }
            else
            {
                rend.material = UnSelectedMaterial;
            }
        }
        else
        {
            rend.material = UnSelectedMaterial;
        }
    }
}
