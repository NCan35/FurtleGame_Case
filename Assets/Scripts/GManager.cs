using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GManager : MonoBehaviour
{
    Slot SelectedSlot;
    internal bool isTransferring = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SelectSlot(Slot slot)
    {
        if (isTransferring) return;  // mevcut transfer bitesiye kadar yeni touchlari alma.

        if((!slot && SelectedSlot) || (slot && slot==SelectedSlot)) // bosa tikladiysa ilk secileni iptal et, veya aynı slota tikladiysa secileni iptal et.
        {
            SelectedSlot.UnSelectCards();
            SelectedSlot = null;
            return;
        }

        if (!slot) return; // bosa tikladiysa ve ilk secilen yoksa bir şey yapma.

        if (!SelectedSlot) // ilk secilen yoksa, ilk secilen atamasini yap.
        {
            if (slot.CardsOnSlot.Count <= 0) return; // ilk secilende kart yoksa, atama yapma.
            SelectedSlot = slot;
            slot.SelectCards();
            return;
        }


        if ((slot.CardsOnSlot.Count > 0) && // slotta kart var fakat kart renkleri uygun degilse iptal et.
            (int)slot.CardsOnSlot[slot.CardsOnSlot.Count - 1]._type != (int)SelectedSlot.CardsOnSlot[SelectedSlot.CardsOnSlot.Count - 1]._type)
        {
            Shaking(slot.transform);
            SelectedSlot.UnSelectCards();
            SelectedSlot = null;
            return;
        }
        StartCoroutine(slot.TransferCards(SelectedSlot)); //transferi gerceklestir.
        SelectedSlot = null;

    }

    void Shaking(Transform slot)
    {
        slot.DOMoveX(slot.position.x + 0.1f, 0.05f).SetEase(Ease.Linear).OnComplete(() =>
        {
            slot.DOMoveX(slot.position.x - 0.2f, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                slot.DOMoveX(slot.position.x + 0.1f, 0.05f).SetEase(Ease.Linear);
            });
        });
    }
}
