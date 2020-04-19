using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void Attack(int direction = 0, Vector3 target = new Vector3());
}
