using UnityEngine;

public class DragAndShoot2DDynamicWithLineRenderer : MonoBehaviour
{
    public float maxDragDistance = 15f;  // Distanța maximă pe care poți trage de obiect
    public float maxForceMultiplier = 20f; // Multiplicator maxim pentru a ajusta forța aplicată
    public LineRenderer lineRenderer;   // Referință la componenta LineRenderer

    private Vector2 dragStartPos;       // Poziția de start când începi să tragi
    private Rigidbody2D rb;             // Referință la Rigidbody2D-ul obiectului
    private bool isDragging = false;    // Flag care indică dacă obiectul este tras
    private float currentForceMultiplier; // Multiplicatorul curent al forței

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Asigură-te că LineRenderer are cel puțin două puncte
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false; // Dezactivează linia inițial
        }
    }

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        // Stochează poziția de start a mouse-ului când începem să tragem
        dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;

        // Oprește temporar mișcarea obiectului
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Activează linia și setează primul punct
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // Calculează diferența dintre poziția curentă a mouse-ului și poziția de start
            Vector2 dragCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = dragStartPos - dragCurrentPos;

            // Limitarea distanței de tragere
            float dragDistance = Mathf.Clamp(dragVector.magnitude, 0, maxDragDistance);
            dragVector = dragVector.normalized * dragDistance;

            // Setează poziția punctului inițial (la start) și punctul final (poziția curentă a mouse-ului)
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + (Vector3)dragVector);
            }

            // Calculează și ajustează currentForceMultiplier în funcție de distanța trasă
            currentForceMultiplier = Mathf.Lerp(0, maxForceMultiplier, dragDistance / maxDragDistance);
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            GetComponent<SpriteRenderer>().color = Color.white;

            // Calculează forța în direcția opusă tragerii
            Vector2 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 forceDirection = (dragStartPos - dragEndPos).normalized;

            // Aplică forța obiectului în funcție de currentForceMultiplier
            rb.AddForce(forceDirection * currentForceMultiplier, ForceMode2D.Impulse);

            // Dezactivează linia
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
            }

            // Resetează flagul de tragere
            isDragging = false;
        }
    }
}
