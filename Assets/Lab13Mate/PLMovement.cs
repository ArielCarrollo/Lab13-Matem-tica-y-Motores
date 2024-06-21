using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody myRB;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float x_Movement;
    [SerializeField] private float y_Movement;
    [SerializeField] public bool Victory;
    [SerializeField] public int Jumps;
    [SerializeField] public int fuerza;
    [SerializeField] public LayerMask layermask;
    public float duracionAnimacion = 2f;
    public float alturaSalto = 1.5f;
    public float amplitudOscilacion = 0.5f;
    public float velocidadRotacion = 360f;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private Vector3 escalaInicial;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        escalaInicial = transform.localScale;
    }
    void FixedUpdate()
    {
        myRB.velocity = new Vector2(x_Movement * velocityModifier, myRB.velocity.y);
        Chequearpiso();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        x_Movement = context.ReadValue<Vector2>().x;
        y_Movement = context.ReadValue<Vector2>().y;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumP();
            Debug.Log("Jump");
        }
    }
    void JumP()
    {
        if (Jumps > 0)
        {
            myRB.AddForce(Vector2.up * fuerza, ForceMode.Impulse);
            Jumps--;
        }
    }
    void Chequearpiso()
    {
        RaycastHit raycast;
        if (Physics.Raycast(transform.position, Vector3.down, out raycast, rayDistance, layermask))
        {
            if (raycast.collider.tag == "Piso")
            {
                Jumps = 1;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PisoGanador"))
        {
            transform.position = posicionInicial;
            transform.rotation = rotacionInicial;
            transform.localScale = escalaInicial;
            Sequence secuencia = DOTween.Sequence();
            secuencia.Append(transform.DOScale(transform.localScale * 1.5f, duracionAnimacion / 4f).SetEase(Ease.OutQuad)).Append(transform.DOScale(escalaInicial, duracionAnimacion / 4f).SetEase(Ease.InQuad));
            secuencia.Insert(0f, transform.DOLocalJump(transform.position + Vector3.up * alturaSalto, alturaSalto, 1, duracionAnimacion / 2f).SetEase(Ease.OutQuad));
            secuencia.Join(transform.DORotate(new Vector3(0f, 0f, 360f), duracionAnimacion / 2f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    float angulo = Mathf.Sin(Time.time * velocidadRotacion) * amplitudOscilacion;
                    transform.Rotate(Vector3.forward, angulo);
                }));
            secuencia.Play().OnComplete(() =>
            {
                transform.localScale = escalaInicial;
            });
        }
    }
}


