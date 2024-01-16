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

    //Staffs 
    [HideInInspector] public Transform firePoint;
    [HideInInspector] public static Transform pivot;
    public Staff equippedStaff;
    [HideInInspector] public Sprite equippedStaffSprite;

    public Spell equippedSpell;
    private List<Spell> activeSpells;

    //Inventory
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public int inventoryHeight;
    [HideInInspector] public int inventoryWidth;
    [HideInInspector] public int spellInventorySize;
    [HideInInspector] public int potionBagSize;
    public ItemLibrary itemLibrary;
    public InventoryUI inventoryUI;



    private void Awake(){
        //Singleton
        if (instance == null){
            instance = this;
        }else{
            Destroy(this);
        }

        //set default player stats
        activeSpells = new List<Spell>();
        moveSpeed = 5f;
        inventoryHeight = 4;
        inventoryWidth = 10;
        spellInventorySize = 8;
        potionBagSize = 4;

        //creating data storage
        inventory = new Inventory(inventoryWidth*inventoryHeight, spellInventorySize, potionBagSize);

        //creating UI
        inventoryUI.inventoryRenderer.width = inventoryWidth;
        inventoryUI.inventoryRenderer.height = inventoryHeight;

        inventoryUI.potionBagRenderer.width = 1;
        inventoryUI.potionBagRenderer.height = potionBagSize;

        inventoryUI.spellsRenderer.width = spellInventorySize;
        inventoryUI.spellsRenderer.height = 1;

        inventoryUI.equippedSpellsRenderer.width = 4;
        inventoryUI.equippedSpellsRenderer.height = 1;

        inventory.items[0] = itemLibrary.basicStaff;
        inventory.items[1] = itemLibrary.forestStaff;
        inventory.items[2] = itemLibrary.darkstaff;

        inventory.spells[0] = itemLibrary.fireball;
        inventory.spells[1] = itemLibrary.iceShot;
        inventory.spells[2] = itemLibrary.magicShot;

        inventory.potions[0] = itemLibrary.healthPotion;
        
        EquipStaff((StaffItem)inventory.items[0]);

        //components
        rb = GetComponent<Rigidbody2D>();
        raycastHit2Ds = new List<RaycastHit2D>(0);
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        equippedStaffSprite = equippedStaff.GetComponent<SpriteRenderer>().sprite;

        //for shooting magic
        pivot = transform.GetChild(0).gameObject.transform;
        firePoint = pivot.transform.GetChild(0);
        
        maxMana = 100;
        mana = maxMana;

        // Mana Bar UI has same max mana as player stats
        ManaBar.SetMaxMana(maxMana);
        ManaBar.SetMana(maxMana);

        // after increasing max health, set current health to max health
        IncreaseMaxHealth(5);
        health = maxHealth;

        activeCoroutine = null;
    }


    private void Update(){
        //have to set this for inventory 
        equippedStaffSprite = equippedStaff.GetComponent<SpriteRenderer>().sprite;

        
        // string a = "";
        // foreach (Item item in inventory.items) {
        //     if (item == null)
        //     {
        //         a += " ,";
        //     }
        //     else {
        //         a += item.ToString() + ",";
        //     }

        // }

      
        // Debug.Log(a);    
        

        if(Input.GetKeyDown(KeyCode.Alpha1) && inventory.equippedSpells[0] != null){
            if(activeSpells.Contains(inventory.equippedSpells[0].reference.GetComponent<Spell>())){
                equippedSpell = activeSpells.Find(e => e.Equals(inventory.equippedSpells[0].reference.GetComponent<Spell>()));
            }else{
                equippedSpell = Instantiate(inventory.equippedSpells[0].reference).GetComponent<Spell>();
                activeSpells.Add(equippedSpell);
            }
        }else if(Input.GetKeyDown(KeyCode.Alpha2) && inventory.equippedSpells[1] != null){
            if(activeSpells.Contains(inventory.equippedSpells[1].reference.GetComponent<Spell>())){
                equippedSpell = activeSpells.Find(e => e.Equals(inventory.equippedSpells[1].reference.GetComponent<Spell>()) );
            }else{
                equippedSpell = Instantiate(inventory.equippedSpells[1].reference).GetComponent<Spell>();
                activeSpells.Add(equippedSpell);
            }
        }else if(Input.GetKeyDown(KeyCode.Alpha3) && inventory.equippedSpells[2] != null){
            if(activeSpells.Contains(inventory.equippedSpells[2].reference.GetComponent<Spell>())){
                equippedSpell = activeSpells.Find(e => e.Equals(inventory.equippedSpells[2].reference.GetComponent<Spell>()) );
            }else{
                equippedSpell = Instantiate(inventory.equippedSpells[2].reference).GetComponent<Spell>();
                activeSpells.Add(equippedSpell);
            }
        }else if(Input.GetKeyDown(KeyCode.Alpha4) && inventory.equippedSpells[3] != null){
            if(activeSpells.Contains(inventory.equippedSpells[3].reference.GetComponent<Spell>())){
                equippedSpell = activeSpells.Find(e => e.Equals(inventory.equippedSpells[3].reference.GetComponent<Spell>()) );
            }else{
                equippedSpell = Instantiate(inventory.equippedSpells[3].reference).GetComponent<Spell>();
                activeSpells.Add(equippedSpell);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Break();
        }

       if (Input.GetMouseButtonDown(0)){
            if(equippedSpell != null){
                equippedSpell.Fire();
            }
       }

       if(Input.GetKeyDown(KeyCode.E)){
            if (inventoryUI.isOpen){
                if(MousePointer.instance.selectedItem != null){
                    MousePointer.instance.selectedItem.SnapBack();
                }
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

    private Spell GetActiveSpell(Spell spell){
        foreach(Spell spl in activeSpells){
            if(spl == spell){
                return spl;
            }
        }
        throw new ArgumentException("active spell does not exist");
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

    public void EquipStaff(StaffItem staff){
        inventory.equippedStaff = staff;
        equippedStaff.GetComponent<SpriteRenderer>().sprite = staff.reference.GetComponent<SpriteRenderer>().sprite;
        equippedStaff.damageBonus = staff.reference.GetComponent<Staff>().damageBonus;
    }

    public void EquipSpell(SpellItem spell, int spellIndex){
        if(FindEquippedSpell(spell) != -1){
            inventory.equippedSpells[FindEquippedSpell(spell)] = null;
        }
        inventory.equippedSpells[spellIndex] = spell;
    }

    public int FindEquippedSpell(SpellItem spell){
        for(int i = 0 ; i < 4 ;i++){
            if(inventory.equippedSpells[i] == spell){
                return i;
            }
        }
        return -1;
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



