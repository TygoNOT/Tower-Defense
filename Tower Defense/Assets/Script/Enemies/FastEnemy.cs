using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    private void Start() => CallInStart();

    void Update() => CallInUpdate();

    private void FixedUpdate() => CallInFixedUpdate();
}
