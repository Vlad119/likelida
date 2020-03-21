using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UI/ExtendButton", 31)]
public class ExtendButton : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler
{
    public bool interactable = true;
    [SerializeField] private bool colorChangePress = true;
    [SerializeField] private bool scboll = true;
    public Graphic targetGraphic;
    public Color BeginColor = new Color(1, 1, 1, 1);
    public Color OverColor = new Color(.8f, .8f, .8f, 1);
    public Color PressColor = new Color(.7f, .7f, .7f, 1);
    [SerializeField]
    private Vector3 beginSize = Vector3.one;
    [SerializeField]
    private Vector3 targetSize = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private int framesPress = 10;

    [SerializeField] private UnityEvent PressButtonInvoke;
    [Serializable]
    public class VectorChange : UnityEvent<Vector2> { };
    [SerializeField]
    private VectorChange DragInvoke = new VectorChange();
    public bool SwipesNeed = false;
    public UnityEvent SwipeLeft;
    public UnityEvent SwipeRight;
    public UnityEvent SwipeUp;
    public UnityEvent SwipeDown;

    //[Range(1, 10)][SerializeField] private float spsc = 5;
    private bool activeUse = false;
    private void Awake()
    {

    }

    private void OnValidate()
    {
        if (GetComponent<Graphic>())
        {
            targetGraphic = GetComponent<Graphic>();
        }
    }



    public async void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable || activeUse)
            return;
        activeUse = true;
        var t1_1 = targetGraphic.ColorStepTo(PressColor, framesPress);
        var t2_1 = transform.ScaleStepTo(targetSize, framesPress);
        await Task.WhenAll(scboll ? t1_1 : new Task(null), colorChangePress ? t2_1 : new Task(null));
        var t1_2 = targetGraphic.ColorStepTo(BeginColor, framesPress);
        var t2_2 = transform.ScaleStepTo(beginSize, framesPress);
        await Task.WhenAll(scboll ? t1_2 : new Task(null), colorChangePress ? t2_2 : new Task(null));
        targetGraphic.color = BeginColor;
        transform.localScale = beginSize;
        activeUse = false;
        PressButtonInvoke?.Invoke();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (interactable)
        {
            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {
                if (eventData.delta.x > 0) SwipeRight.Invoke();
                else SwipeLeft.Invoke();
            }
            else
            {
                if (eventData.delta.y > 0) SwipeUp.Invoke();
                else SwipeDown.Invoke();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragInvoke?.Invoke(eventData.delta);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (interactable || targetGraphic == null || activeUse)
            return;
        targetGraphic.color = OverColor;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (interactable || targetGraphic == null || activeUse)
            return;
        targetGraphic.color = BeginColor;
    }
}



