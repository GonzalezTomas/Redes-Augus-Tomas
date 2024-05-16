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

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * velocidadRotacion;
        jugadorTransform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * velocidadRotacion;
        _rotX -= mouseY;
        _rotX = Mathf.Clamp(_rotX, -90f, 40f);
        transform.localRotation = Quaternion.Euler(_rotX, 0f, 0f);

        // Calcular la rotación del arma
        Quaternion rotationOffset = Quaternion.Euler(0f, 90f, 0f); // Ajusta el ángulo aquí
        Quaternion armaRotation = transform.rotation * rotationOffset;
        Vector3 posicionArma = transform.position + transform.forward * distanciaArma + transform.up * alturaArma;
        armaTransform.position = posicionArma;
        armaTransform.rotation = armaRotation;
    }
}





