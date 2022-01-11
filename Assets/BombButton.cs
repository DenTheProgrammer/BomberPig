using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombButton : MonoBehaviour
{
    public static event Action onBombButtonPress;

    public static void OnBombButtonPress()
    {
        onBombButtonPress?.Invoke();
    }
}
