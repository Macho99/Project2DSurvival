using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] Sprite aimSprite;
    [SerializeField] float aimScale = 3;
    [SerializeField] float aimDist = 3;

    private Transform aimPoint;
    private Transform aimPivot;
    private void Awake()
    {
        aimPivot = new GameObject("AimPivot").transform;
        aimPivot.parent = transform;
        aimPivot.localPosition = Vector2.zero;

        aimPoint = new GameObject("AimPoint").transform;
        aimPoint.parent = aimPivot;
        aimPoint.localPosition = Vector2.up * aimDist;
        aimPoint.localScale = Vector3.one * aimScale;
        SpriteRenderer aimRenderer = aimPoint.AddComponent<SpriteRenderer>();
        aimRenderer.sprite = aimSprite;
        aimRenderer.sortingLayerName = "UI";
    }
    public Transform GetAimTransform()
    {
        return aimPoint.transform;
    }

    private void Update()
    {
        if (Time.timeScale < 0.001f) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - aimPivot.position.y, mousePos.x - aimPivot.position.x) * Mathf.Rad2Deg;
        aimPivot.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
