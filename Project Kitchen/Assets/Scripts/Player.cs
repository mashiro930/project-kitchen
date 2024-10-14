using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    public ParticleSystem dirtParticle;
    public bool isWalking = false;
    private BaseCounter selectedCounter;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnOperateAction += GameInput_OnOperateAction;
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        if (isWalking ) { dirtParticle.Play(); }
        else { dirtParticle.Stop(); }
    }

    public bool IsWalking
    {
        get
        {
            return isWalking;
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        selectedCounter?.Interact(this);
    }

    private void GameInput_OnOperateAction(object sender, System.EventArgs e)
    {
        selectedCounter?.InteractOperate(this);
    }

    private void HandleMovement()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();

        isWalking = direction != Vector3.zero;

        transform.position += direction * Time.deltaTime * moveSpeed;

        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
        }
    }

    private void HandleInteraction()
    {
        RaycastHit hitinfo;
        bool isCollide = Physics.Raycast(transform.position, transform.forward, out hitinfo, 2f, counterLayerMask);
        if (isCollide)
        {
            if (hitinfo.transform.TryGetComponent<BaseCounter>(out BaseCounter counter))
            {
                //counter.Interact();
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    public void SetSelectedCounter(BaseCounter counter)
    {
        if (counter != selectedCounter)
        {
            selectedCounter?.CancelSelect();
            counter?.SelectCounter();

            this.selectedCounter = counter;
        }
    }
}
