using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OnTakeDamage : IBaseEvent
{
    int amount;
}
public class OnUpdateHP : IBaseEvent
{
    public int amount;
}

public interface IBaseEvent 
{
}
