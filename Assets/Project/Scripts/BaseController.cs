using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum BaseType
{
    Player, Enemy
}

public abstract class BaseController : MonoBehaviour
{
    public int id;

    public BaseType type;
    public abstract void UpdateController();

    public abstract void Restart();
}
