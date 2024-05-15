using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    private Transform _transform;
   
   public void SetTarget(Transform target)
    {
        _transform = target;
    }
  
    void Update()
    {
        
    }
}
