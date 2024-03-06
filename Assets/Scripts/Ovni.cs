using UnityEngine;

public class Ovni : MonoBehaviour
{
    public float levitationHeight = 7.0f;
    public float levitationSpeed = 2.0f;
    public float rotationSpeed = 30.0f;  // Velocidad de rotación en grados por segundo
    public float moveSpeed = 3.0f;
    public float timeToMove = 2.0f;

    private float elapsedTime = 0.0f;
    private Vector3 targetPosition;

    void Start()
    {
        SetRandomTargetPosition();
    }

    void Update()
    {
        RotateAndMove();
    }

    void RotateAndMove()
    {
        // Rotación en su propio eje
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Mueve el OVNI hacia la posición objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Calcula la nueva posición en el eje Y para la levitación
        float newY = Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;

        // Aplica la nueva posición en el eje Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Verifica si ha alcanzado la posición objetivo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeToMove)
            {
                SetRandomTargetPosition();
                elapsedTime = 0.0f;
            }
        }
    }

    void SetRandomTargetPosition()
    {
        // Establece una nueva posición aleatoria en el plano XZ
        targetPosition = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
    }
}
