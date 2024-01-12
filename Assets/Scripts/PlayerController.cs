using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //SINGLETON
    public static PlayerController instance;

    //Movement
    public float moveSpeed;
    private Vector2 movementDirection;
    public ContactFilter2D contactFilter;
    private float collisionOffset = 0.04f;
    public static Vector3 characterPos; //used by enemy and bullet class 

    //Components
    private SpriteRenderer sr;
    private Animator animator;
    private Rigidbody2D rb;
    private List<RaycastHit2D> raycastHit2Ds;

    //Player Stats
    public static int maxHealth;
    public static int health;
    public static int maxMana = 100;
    public static int mana;

    //UI
    public HealthBarList HealthBarList;
    public ManaBar ManaBar;

    //For Zooming in and out
    [SerializeField] CinemachineVirtualCamera cam;
    private Coroutine activeCoroutine;
    private const float DEFAULTZOOM = 5f;

    //For orienting staffs 
    public Transform firePoint;
    public static Transform pivot;
    public Staff equippedStaff;
    public Spell equippedSpell;
    private Spell spell1;
    private Spell spell2;
    private Spell spell3;
    private Spell spell4;

    //Spells
    [SerializeField] GameObject ice;
    [SerializeField] GameObject fire;
    [SerializeField] GameObject magicMissle;


    //Staff
    [SerializeField] GameObject basicStaff;

    //Inventory
    public Inventory inventory;
    public InventoryRenderer inventoryUI;
    public int inventoryHeight;
    public int inventoryWidth;
    public int spellSlots;


    private void Awake(){
        //Singleton
        if (instance == null){
            instance = this;
        }else{
            Destroy(this);
        }

        //set default player stats
        moveSpeed = 5f;
        inventoryHeight = 4;
        inventoryWidth = 10;
        spellSlots = 4;
        inventory = new Inventory(inventoryWidth*inventoryHeight, 4);

        inventory.items[0] = ItemLibrary.instance.basicStaff.GetComponent<StaffItem>();
     
        //components
        rb = GetComponent<Rigidbody2D>();
        raycastHit2Ds = new List<RaycastHit2D>(0);
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //for shooting magic
        pivot = transform.GetChild(0).gameObject.transform;
        firePoint = pivot.transform.GetChild(0);

        equippedStaff = basicStaff.GetComponent<Staff>();
        equippedSpell = magicMissle.GetComponent<Spell>();
        
        maxMana = 100;
        mana = maxMana;

        ManaBar.SetMaxMana(maxMana);
        ManaBar.SetMana(maxMana);
        // Mana Bar UI has same max mana as player stats

        IncreaseMaxHealth(5);
        health = maxHealth;

        // after increasing max health, set current health to max health

        activeCoroutine = null;
    }

    private void Update(){ 
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Break();
        }

       if (Input.GetMouseButtonDown(0)){
            equippedSpell.Fire();
       }

       if(Input.GetKeyDown(KeyCode.E)){

            if (inventoryUI.isOpen){
                inventoryUI.Disable();
            }else{
                inventoryUI.Enable();
            }
       }

        if (Input.GetMouseButtonDown(1)) {
            if (activeCoroutine != null) {
                StopCoroutine(activeCoroutine);
            }
            activeCoroutine = StartCoroutine(ZoomOut(DEFAULTZOOM*2));
        }else if (Input.GetMouseButtonUp(1)) {
            if (activeCoroutine != null){
                StopCoroutine(activeCoroutine);
            }
            activeCoroutine = StartCoroutine(ZoomIn());
        }
    
        //movement
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        characterPos = transform.position; 
    }


    private void FixedUpdate() {
        //movement loop
        if(movementDirection != Vector2.zero){
            if(CanMove(movementDirection)){
                rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
                animator.SetBool("isWalking", true);
            }else{
                if(CanMove(new Vector2(movementDirection.x, 0))){
                    rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * new Vector2(movementDirection.x, 0));
                    animator.SetBool("isWalking", true);
                }else if(CanMove(new Vector2(0, movementDirection.y))){
                        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * new Vector2(0, movementDirection.y));
                        animator.SetBool("isWalking", true);
                }else{
                    animator.SetBool("isWalking", false);
                } 
            }
        }else{
            animator.SetBool("isWalking", false); 
        }

        //flip the character sprite to face movement direction
        if(movementDirection.x > 0){
            sr.flipX = false;
        }else if(movementDirection.x < 0){
            sr.flipX = true;
        }
    }

   
    private bool CanMove(Vector2 direction){
        if (direction != Vector2.zero){
            int raycastHits = rb.Cast(direction, contactFilter, raycastHit2Ds, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if(raycastHits == 0){
                return true;
            }
            return false;
        }
        return false;
    }

    private IEnumerator ZoomOut(float maxZoom){
        float currentZoom = cam.m_Lens.OrthographicSize;

        for (float i = 0; i <= 1; i+=0.05f){
            cam.m_Lens.OrthographicSize = Mathf.Lerp(currentZoom, maxZoom, i);
            yield return new WaitForSeconds(.01f);
        }
    }

    private IEnumerator ZoomIn(){
        float currentZoom = cam.m_Lens.OrthographicSize;

        for (float i = 0; i <= 1; i+=0.05f){
            cam.m_Lens.OrthographicSize = Mathf.Lerp(currentZoom, DEFAULTZOOM, i);
            yield return new WaitForSeconds(.01f);
        }
    }

    public void IncreaseMaxHealth()
    {
        HealthBarList.InstantiateHeart();
        maxHealth++;
    }

    public void IncreaseMaxHealth(int num){
        for (int i = 0; i < num;i++){
            IncreaseMaxHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        HealthBarList.EmptyFullHeart();
        health -= damage;
    }

    public void GainHeart()
    {
        if (health < maxHealth)
        {
            HealthBarList.FillEmptyHeart();
            health += 1;
            Debug.Log(health);
        }

    }


}



