using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamera : MonoBehaviour
{
    public Transform jugadorTransform; 
    public Transform armaTransform; 
    public float distanciaArma = 2.0f; 
    public float alturaArma = -0.5f; 
    public float velocidadRotacion = 3.0f; 

    private float _rotX = 0; 

    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * velocidadRotacion;

        
        jugadorTransform.Rotate(Vector3.up * mouseX);

        
        float mouseY = Input.GetAxis("Mouse Y") * velocidadRotacion;

        
        _rotX -= mouseY;
        _rotX = Mathf.Clamp(_rotX, -90f, 90f);

        
        transform.localRotation = Quaternion.Euler(_rotX, 0f, 0f);

        
        Vector3 posicionArma = transform.position + transform.forward * distanciaArma + transform.up * alturaArma;

        
        armaTransform.position = posicionArma;

        
        armaTransform.rotation = transform.rotation;
    }
}





