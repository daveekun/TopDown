using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public UI_Slot origin;
    private RectTransform rt;
    private CanvasGroup cv;
    private Image image;
    public int id;
    public delegate void OnPickUp(int id);
    public static event OnPickUp onPickUp;
    public delegate void OnDropOff();
    public static event OnDropOff onDropOff;
    private Text text;

    public void Awake()
    {
        origin = transform.parent.GetComponent<UI_Slot>();
        id = origin.id;
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = origin.GetComponent<RectTransform>().anchoredPosition;
        cv = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        onPickUp += CheckPickUp;
        onDropOff += CheckDropOff;
        text = GetComponentInChildren<Text>();
        UpdateContent();
    }

    void CheckPickUp(int id)
    {
        if (this.id == id)
            return;
        cv.blocksRaycasts = false;
    }

    void CheckDropOff()
    {
        cv.blocksRaycasts = true;
    }

    public void UpdateContent()
    {
        if (origin.slot.item == null || origin.slot.amount == 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            image.sprite = null;
            origin.slot.item = null;
            text.text = "";
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            image.sprite = origin.slot.item.sprite;
            text.text = origin.slot.amount.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.shoot.canShoot = false;
        cv.blocksRaycasts = false;
        onPickUp?.Invoke(id);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.instance.shoot.canShoot = true;
        onDropOff?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rt.anchoredPosition += eventData.delta;   
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        cv.alpha = 0.6f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cv.alpha = 1f;
        rt.anchoredPosition = origin.GetComponent<RectTransform>().anchoredPosition;
    }

    
}
