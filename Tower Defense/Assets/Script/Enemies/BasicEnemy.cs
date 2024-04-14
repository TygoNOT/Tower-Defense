using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    //Первый базовый враг
    private void Start() => CallInStart();

    private void Update() => CallInUpdate();

    private void FixedUpdate() => CallInFixedUpdate();
}
