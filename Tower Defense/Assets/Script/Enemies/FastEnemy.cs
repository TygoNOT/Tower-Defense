using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    void Update()
    {
        CallInUpdate();
    }

    private void FixedUpdate()
    {
        CallInFixedUpdate();
    }
}
