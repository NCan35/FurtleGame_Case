using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardType { Red, Green, Blue }
    public CardType _type;


    private void OnValidate()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        transform.GetChild((int)_type).gameObject.SetActive(true);

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

}
