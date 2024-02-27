using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    #region sinleton
    public static PlayerInput Instance;
    #endregion

    #region Keycodes
    [SerializeField] KeyCode  sprintKey;
    [SerializeField] KeyCode  shootKey;
    [SerializeField] KeyCode  bagKey;
    #endregion

    #region Values
    public Vector3  moveInput;
    public Vector3  mousePosition;
    public bool     isSprinting;
    public bool     isShooting;
    #endregion

    #region Events
    public delegate void OnBagChange();
    public static event OnBagChange onBagChange;
    #endregion

    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this);
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isSprinting = Input.GetKey(sprintKey);
        isShooting  = Input.GetKey(shootKey);
        if (Input.GetKeyDown(bagKey))
            onBagChange?.Invoke();
    }
}
