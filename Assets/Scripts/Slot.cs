using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public List<Card> CardsOnSlot = new List<Card>();
    internal List<Card> SameColoredCards = new List<Card>();
    Transform CardRef;
    GManager gManager;

    // Start is called before the first frame update
    void Start()
    {
        gManager = FindObjectOfType<GManager>();
        CardRef = transform.Find("CardRef");
        ReDesignCards();
    }

    public IEnumerator TransferCards(Slot fromSLot)
    {
        gManager.isTransferring = true;
        float currentIndex = CardsOnSlot.Count;

        for (int i = 0; i < fromSLot.SameColoredCards.Count; i++)
        {
            fromSLot.SameColoredCards[i].transform.SetParent(CardRef);


            Vector3 target = new Vector3(0, (currentIndex + i) * 0.08f, 0);
            fromSLot.SameColoredCards[i].transform.DOLocalJump(target, 0.8f, 1, 0.5f).SetEase(Ease.Linear);
            fromSLot.SameColoredCards[i].transform.DORotate(fromSLot.SameColoredCards[i].transform.eulerAngles + GetRotate(fromSLot.transform), 0.5f).SetEase(Ease.Linear);

            CardsOnSlot.Add(fromSLot.SameColoredCards[i]);
            fromSLot.CardsOnSlot.Remove(fromSLot.SameColoredCards[i]);
            yield return new WaitForSeconds(0.0001f);
        }

        fromSLot.SameColoredCards.Clear();

        yield return new WaitForSeconds(0.1f);
        gManager.isTransferring = false;

    }

    public void SelectCards()
    {
        int firstType = (int)CardsOnSlot[CardsOnSlot.Count - 1]._type;

        for (int i = CardsOnSlot.Count - 1; i >= 0; i--)
        {
            if ((int)CardsOnSlot[i]._type == firstType)
            {
                CardsOnSlot[i].transform.DOLocalMoveY(CardsOnSlot[i].transform.localPosition.y + 0.1f, 0.1f);
                SameColoredCards.Add(CardsOnSlot[i]);
            }
        }
    }

    public void UnSelectCards()
    {
        int firstType = (int)CardsOnSlot[CardsOnSlot.Count - 1]._type;

        for (int i = CardsOnSlot.Count - 1; i >= 0; i--)
        {
            if ((int)CardsOnSlot[i]._type == firstType)
            {
                CardsOnSlot[i].transform.DOLocalMoveY(CardsOnSlot[i].transform.localPosition.y - 0.1f, 0.1f);
            }
        }
        SameColoredCards.Clear();
    }

    Vector3 GetRotate(Transform fromSLot)
    {
        Vector3 RotateDirection = Vector3.zero;

        if (Mathf.Abs(transform.position.z - fromSLot.position.z) >= 0.1f)
            RotateDirection.x = -180f;
        else
            RotateDirection.z = transform.position.x > fromSLot.position.x ? -180f : 180f;

        return RotateDirection;
    }

    void ReDesignCards()
    {
        for (int i = 0; i < CardsOnSlot.Count; i++)
        {
            CardsOnSlot[i].transform.SetParent(CardRef);
            CardsOnSlot[i].transform.localPosition = new Vector3(0, i * 0.08f, 0);
        }
    }
}
