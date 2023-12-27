using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected abstract void OnEnable();

    protected abstract void OnDisable();

    protected abstract void OnLevelUp();
}