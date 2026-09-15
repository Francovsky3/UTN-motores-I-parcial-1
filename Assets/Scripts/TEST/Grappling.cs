using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovementGrappling pm;
    public Transform cam;
    public Transform gunTip;
    public Transform holdPoint;
    public LayerMask whatIsGrappleable;
    public LayerMask packageLayer;
    public LineRenderer lr;
    public Transform orientation;

    [Header("Grappling Settings")]
    public float maxGrappleDistance = 25f;
    public float grappleDelayTime = 0.1f;
    public float overshootYAxis = 2f;

    [Header("Package Mechanics")]
    public float packageAttractSpeed = 25f;
    public float throwForce = 30f;

    private Vector3 grapplePoint;
    private ThrowablePackage currentPackage;
    private bool isAttractingPackage = false;

    [Header("Cooldown")]
    public float grapplingCd = 0.2f;
    private float grapplingCdTimer;

    [Header("Camera Logic Reference")]
    public ThirdPersonCam camStyleScript; // Referencia al script ThirdPersonCam
    public LayerMask ignorePlayerLayers;   // Capa para ignorar al jugador en la cámara Combat

    private bool grappling;

    private void Start()
    {
        pm = GetComponentInParent<PlayerMovementGrappling>();
    }

    private void Update()
    {
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }

        Mouse mouse = Mouse.current;
        if (mouse == null) return;

        // Disparo con clic derecho (Gancho / Atraer paquete)
        if (mouse.rightButton.wasPressedThisFrame)
        {
            if (grappling)
            {
                CancelInvoke(nameof(ExecuteGrapple));
                CancelInvoke(nameof(StopGrapple));
                if (pm != null) pm.ResetRestrictions();
            }

            StartGrapple();
        }

        // Lanzamiento del paquete con clic izquierdo
        if (mouse.leftButton.wasPressedThisFrame && currentPackage != null)
        {
            ThrowPackage();
        }

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;

        // Lógica de atracción progresiva hacia el HoldPoint
        if (isAttractingPackage && currentPackage != null)
        {
            currentPackage.transform.position = Vector3.MoveTowards(
                currentPackage.transform.position,
                holdPoint.position,
                packageAttractSpeed * Time.deltaTime
            );

            if (Vector3.Distance(currentPackage.transform.position, holdPoint.position) < 0.2f)
            {
                AttachPackage();
            }
        }
    }

    private void LateUpdate()
    {
        // Dibujado de la cuerda en pantalla
        if ((grappling || isAttractingPackage) && lr != null)
        {
            lr.SetPosition(0, gunTip.position);
            Vector3 target = isAttractingPackage && currentPackage != null ? currentPackage.transform.position : grapplePoint;
            lr.SetPosition(1, target);
        }
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        RaycastHit hit;

        // Si NO llevamos un paquete, verificamos primero si apuntamos a uno para atraelo
        if (currentPackage == null)
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, packageLayer))
            {
                ThrowablePackage pack = hit.collider.GetComponent<ThrowablePackage>();
                if (pack != null)
                {
                    currentPackage = pack;
                    isAttractingPackage = true;
                    grapplePoint = hit.point;

                    if (lr != null)
                    {
                        lr.enabled = true;
                        lr.positionCount = 2;
                    }
                    return;
                }
            }
        }

        // Si ya llevamos un paquete O si apuntamos a una pared, realizamos el gancho de movimiento
        grappling = true;
        if (pm != null)
        {
            pm.ResetRestrictions();
            pm.freeze = true;
        }

        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        if (lr != null)
        {
            lr.enabled = true;
            lr.positionCount = 2;
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, grapplePoint);
        }
    }

    private void AttachPackage()
    {
        isAttractingPackage = false;
        if (lr != null) lr.enabled = false;

        currentPackage.OnGrabbed(holdPoint);
    }

    private void ThrowPackage()
    {
        if (currentPackage == null) return;

        Vector3 throwDirection;

        // -------------------------------------------------------------
        // MODO COMBAT: Lanzamiento preciso al Crosshair (Raycast Aiming)
        // -------------------------------------------------------------
        if (camStyleScript != null && camStyleScript.currentStyle == ThirdPersonCam.CameraStyle.Combat)
        {
            RaycastHit hit;
            Vector3 targetPoint;

            Vector3 rayOrigin = cam.position + cam.forward * 1.5f;

            if (Physics.Raycast(rayOrigin, cam.forward, out hit, 100f, ignorePlayerLayers))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = rayOrigin + cam.forward * 100f;
            }

            throwDirection = (targetPoint - holdPoint.position).normalized;
        }
        // -------------------------------------------------------------
        // MODO BASIC / THIRD PERSON: Dirección frontal de la vista de la cámara
        // -------------------------------------------------------------
        else
        {
            // En lugar de orientation.forward (que puede desalinearse al no moverse), 
            // usamos la dirección a la que apunta la vista de la cámara en el espacio 3D.
            throwDirection = cam.forward.normalized;
        }

        // Ignorar temporalmente colisiones entre el jugador y el paquete para evitar trabas
        Collider playerCol = GetComponent<Collider>();
        Collider packageCol = currentPackage.GetComponent<Collider>();
        if (playerCol != null && packageCol != null)
        {
            Physics.IgnoreCollision(playerCol, packageCol, true);
        }

        // Ejecutar el lanzamiento
        currentPackage.OnThrown(throwDirection, throwForce);
        currentPackage = null;
    }

    private void ExecuteGrapple()
    {
        if (pm != null) pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        if (pm != null) pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        if (pm != null) pm.freeze = false;

        grappling = false;
        isAttractingPackage = false;
        grapplingCdTimer = grapplingCd;

        if (lr != null) lr.enabled = false;
    }

    public bool IsGrappling() => grappling;
    public Vector3 GetGrapplePoint() => grapplePoint;
}