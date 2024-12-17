using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsAggroed { get; set; }

    bool IsWithinstrikingDistance { get; set; }

    void SetAggroStatus(bool isAggroed);
    void SetstrikingDistanceBooL(bool isWithinstrikingDistance);
}


