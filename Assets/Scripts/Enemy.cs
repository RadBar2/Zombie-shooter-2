using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void Die()
    {
        //TODO: fancy death animations
        Destroy(gameObject);
    }
}
