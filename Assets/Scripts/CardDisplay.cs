using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
{
    public Card card;
    [SerializeField] Text title;
    [SerializeField] Text cost;
    [SerializeField] Image tex;
    [SerializeField] Text descrip;
    [SerializeField] Outline playableOutline;

    [Header("")]
    [SerializeField] float normalScale = 1f;
    [SerializeField] float hoverScale = 1.15f;
    [SerializeField] AudioClip hoverSound;
    
    public Vector3 assignedPos;
    Vector3 targetPos;
    Vector3 targetScale = Vector3.one;
    int siblingIndex;

    RectTransform rt;
    bool dragging;
    GameController gc;
    CanvasGroup canvasGroup;

    void Awake () 
    {
        rt = GetComponent<RectTransform>();
        gc = GameController.instance;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update () 
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 5f * Time.deltaTime);
        rt.position = Vector3.Lerp(rt.position, targetPos, 10f * Time.deltaTime);
        playableOutline.enabled = gc.energy >= card.cost;
    }

    public void AssignPosition (Vector3 pos) 
    {
        rt.localPosition = pos;
        assignedPos = rt.TransformPoint(pos);
        targetPos = assignedPos;
    }

    public void DisplayInfo (Card c, int cardCost, string cardName, Sprite pic, string desc)
    {
        card = c;
        cost.text = cardCost.ToString();
        title.text = cardName;
        tex.sprite = pic;
        descrip.text = desc;
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        if (!dragging) 
        {
            targetScale = new Vector3(hoverScale, hoverScale, 1f);
            targetPos = new Vector3(assignedPos.x, assignedPos.y + 0.5f, assignedPos.z);
            SoundManager.instance.PlaySound(hoverSound, 0.1f, 1f);
        }

        siblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        if (!dragging) 
        {
            targetScale = new Vector3(normalScale, normalScale, 1f);
            targetPos = assignedPos;
        }
        
        transform.SetSiblingIndex(siblingIndex);
    }

    public void OnDrag (PointerEventData eventData) 
    {
        dragging = true;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = new Vector3(pos.x, pos.y, 0f);
        targetScale = new Vector3(0.8f, 0.8f, 1f);
        GetComponent<PlayableCard>().DraggingCard();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.25f;
    }

    public void OnEndDrag (PointerEventData eventData) 
    {
        dragging = false;
        targetPos = assignedPos;
        targetScale = new Vector3(normalScale, normalScale, 1f);
        GetComponent<PlayableCard>().PlayCard();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
