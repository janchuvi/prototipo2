﻿#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6
#define UNITY_4_X
#endif

using UnityEngine;
using System.Collections;

public class KinectButton3D : MonoBehaviour
{
    #region Variables

#if UNITY_4_X
        new
#endif
    static KinectButton3D active = null;

    [HideInInspector, SerializeField]
    bool checkedForCollider;
    [SerializeField]
    Collider _collider;
#if UNITY_EDITOR
    new
#endif
    public Collider collider
    {
        get
        {
            if (!checkedForCollider && _collider == null)
            {
                _collider = GetComponent<Collider>();

                checkedForCollider = true;
            }

            return _collider;
        }
    }

    public bool isDraggable = false;
    float dragStartTime = 0;
    public float dragTime = 0.2f;
    bool isHovering = false;
    bool isDragging = false;
    bool wasDragging = false;
    bool isPressing = false;
    bool wasHovering = false;
    bool canceled = false;

    CursorState cursorState = CursorState.None;

    #endregion

    #region Validate and Trigger methods

    public void ValidateButton(Vector2 inputPoint, CursorState cursorState)
    {
        canceled = false;

        this.cursorState = cursorState;

        if (collider == null)
        {
            return;
        }

        if (active != null && active != this)
        {
            return;
        }

        isDragging = false;

        if (isDraggable)
        {
            if (cursorState == CursorState.Down)
            {
                dragStartTime = Time.timeSinceLevelLoad + dragTime;
            }
            else if (cursorState == CursorState.Pressing)
            {
                if (dragStartTime < Time.timeSinceLevelLoad)
                {
                    isDragging = true;
                }
            }
        }

        isHovering = IsContained(ref inputPoint);

        // Debug.Log(string.Concat("Validando Botón: ", name, isDraggable.ToString(), isDragging.ToString(), isHovering.ToString()));

        ValidateEvents();
    }

    void ValidateEvents()
    {
        if (isHovering)
        {
            OnPersistentHovering();

            if (canceled)
            {
                return;
            }

            if (cursorState != CursorState.Up)
            {
                if (!isDragging)
                {
                    if (!wasHovering)
                    {
                        OnHoverEnter();

                        if (canceled)
                        {
                            return;
                        }
                    }
                    else
                    {
                        OnHoverStay();

                        if (canceled)
                        {
                            return;
                        }
                    }
                }

                if (cursorState == CursorState.Down)
                {
                    active = this;
                    isPressing = true;
                }
            }
        }
        else if (wasHovering)
        {
            if (cursorState == CursorState.None)
            {
                OnNormal();

                if (canceled)
                {
                    return;
                }
            }

            OnHoverExit();

            if (canceled)
            {
                return;
            }
        }

        if (isPressing)
        {
            if (isDragging)
            {
                if (!wasDragging)
                {
                    OnDraggingStarted();

                    if (canceled)
                    {
                        return;
                    }
                }
                else if (!isHovering)
                {
                    OnOutsideDragging();

                    if (canceled)
                    {
                        return;
                    }
                }
                else
                {
                    OnDragging();

                    if (canceled)
                    {
                        return;
                    }
                }
            }

            if (isHovering)
            {
                if (cursorState == CursorState.Down)
                {
                    OnPreClick();

                    if (canceled)
                    {
                        return;
                    }
                }
                else if (cursorState == CursorState.Up)
                {
                    OnClick();

                    if (canceled)
                    {
                        return;
                    }
                }
            }
        }

        if (cursorState == CursorState.Up)
        {
            if (!isHovering && isPressing)
            {
                OnNormal();

                if (canceled)
                {
                    return;
                }
            }

            active = null;
            isPressing = false;
        }

        wasHovering = isHovering;
        wasDragging = isDragging;
    }

    public void TriggerHover()
    {
        isHovering = true;
        isDragging = false;
        wasHovering = false;

        ValidateEvents();
    }

    public void TriggerClick()
    {
        CancelClick();

        isHovering = true;
        isDragging = isDraggable;
        cursorState = CursorState.Down;

        ValidateEvents();
    }

    public void TriggerNormal()
    {
        isHovering = false;
        isPressing = true;
        cursorState = CursorState.Up;

        ValidateEvents();
    }

    #endregion

    #region Overridable events

    protected virtual void OnPersistentHovering()
    {

    }

    protected virtual void OnHoverEnter()
    {

    }

    protected virtual void OnHoverStay()
    {

    }

    protected virtual void OnHoverExit()
    {

    }

    protected virtual void OnNormal()
    {

    }

    protected virtual void OnPreClick()
    {

    }

    protected virtual void OnClick()
    {

    }

    protected virtual void OnDraggingStarted()
    {

    }

    protected virtual void OnDragging()
    {

    }

    protected virtual void OnOutsideDragging()
    {

    }

    #endregion

    #region IsContained

    public bool IsContained(ref Vector2 inputPoint)
    {
        if (collider == null)
        {
            return false;
        }

        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 objPos = new Vector3(inputPoint.x, inputPoint.y, 0);
        Vector3 cameraDir = objPos - cameraPos;
        Ray ray = new Ray(cameraPos, cameraDir.normalized);
        
        RaycastHit hit;
        /*
        if (isDragging)
        {
            Debug.Log(string.Concat("Calculando Hit - x:", inputPoint.x.ToString(), " y:", inputPoint.y.ToString(), ray.origin.ToString(),  ray.direction.ToString()));
        }
        */
        bool resp = collider.Raycast(ray, out hit, 100.0F);
        /*
        if (isDragging)
        {
            Debug.Log(string.Concat("  y ", resp.ToString()));
        }
        */
        return resp;
    }

    #endregion

    #region Static methods

    public static void CancelClick()
    {
        if (active != null)
        {
            active.canceled = true;
            active.isPressing = false;
            active = null;
        }
    }

    #endregion
}