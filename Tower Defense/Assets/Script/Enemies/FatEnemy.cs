using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatEnemy : Enemy
{
    private void Update() => CallInUpdate();

    private void FixedUpdate() => CallInFixedUpdate();
}
