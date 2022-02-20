using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Source : https://www.youtube.com/watch?v=8ZxVBCvJDWk&list=WL&index=89&t=47s&ab_channel=Tarodev

    // Inputs variables
    private PlayerInput _inputs;

    private Vector2 _moveInput;

    // Character variables
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _initialSpeed = 12.5f;
    private float playerSpeed;
    [SerializeField] private float _turnSpeed = 360f;

    private Vector3 moveDirection;

    // TEST
    public bool _hasBox = false;
    [SerializeField] private GameObject boxHeld;
    private bool inGoalZone = false;
    private GameObject gameManager;


    private void Awake() {
        _inputs = new PlayerInput();

        _inputs.Player.Move.performed += OnMove;
        _inputs.Player.Move.canceled += OnMove;

        _inputs.Player.Interact.performed += OnInteract;
        _inputs.Player.Interact.canceled += OnInteract;

        _inputs.Player.Dash.performed += OnDash;
    }

    //
    private void Start()
    {
        boxHeld.SetActive(false);
        gameManager = GameObject.Find("GameManager");

        playerSpeed = _initialSpeed;
    }
    //

    private void OnEnable() {
        _inputs.Enable();
    }

    private void OnDisable() {
        _inputs.Disable();
    }

    private void FixedUpdate() {
        Move();
        Look();
    }

    public void OnMove(InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context) {
       
        playerSpeed = _initialSpeed * 3f;
        Invoke("InitialSpeed", 0.25f);

    }

    public void OnInteract(InputAction.CallbackContext context) { 
        if (inGoalZone && _hasBox) {
            boxHeld.SetActive(false);
            _hasBox = false;
            gameManager.GetComponent<GameManager>()._boxes ++;
        } 

        Debug.Log(gameManager.GetComponent<GameManager>()._boxes);
    }



    private void Look() {
        if (moveDirection != Vector3.zero) {
            var relative = (transform.position + moveDirection.ToIso()) - transform.position;
            //var relative = (transform.position + moveDirection) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _turnSpeed * Time.deltaTime);
        }
    }

    private void Move() {
        moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);

        _rb.MovePosition(transform.position + (transform.forward * moveDirection.magnitude) * playerSpeed * Time.deltaTime);
    }

    private void InitialSpeed() {
        playerSpeed = _initialSpeed;
    }

    private void BoxStatus() {
        if (!_hasBox) {
            boxHeld.SetActive(true);
            _hasBox = true;
        }
    }

    //TEST
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "Box") {
            BoxStatus();
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Goal") {
            inGoalZone = true;
        }

    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.tag == "Goal") {
            inGoalZone = false;
        }

    }

}
