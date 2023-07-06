using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    GManager gManager;
    // Start is called before the first frame update
    void Start()
    {
        gManager = FindObjectOfType<GManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckTouch(Input.mousePosition);
        }
    }   

    private void CheckTouch(Vector3 pos)
    {
        Ray raycast = Camera.main.ScreenPointToRay(pos);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            if (raycastHit.collider.CompareTag("HittableObject"))
                gManager.SelectSlot(raycastHit.collider.transform.parent.parent.GetComponent<Slot>());
            else
                gManager.SelectSlot(null);
        }
    }
}
