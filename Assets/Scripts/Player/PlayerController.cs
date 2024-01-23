using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour, IDataPersistance
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
    public static float maxMana = 100;
    public static float mana;
    private bool _isImmune;
    public int money;

    //UI
    public HealthBarList HealthBarList;
    public ManaBar ManaBar;

    //Staffs 
    [HideInInspector] public Transform firePoint;
    [HideInInspector] public static Transform pivot;
    public Staff equippedStaff;
    [HideInInspector] public Sprite equippedStaffSprite;

    [HideInInspector]public Spell equippedSpell;
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
        money = 0;

        //creating data storage (for saving)
        inventory = new Inventory(inventoryWidth*inventoryHeight, spellInventorySize, potionBagSize);

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

    }
    private void Start(){
        inventoryUI.UpdateData();

        // after increasing max health, set current health to max health
        InstantiateHeart(maxHealth);
        int difference = maxHealth - health;
        for (int i = 0; i < difference; i++) {
            HealthBarList.EmptyFullHeart();
        }
    }

    private void Update(){
        //have to set this for equipped staff img in inventoryUI
        if(inventory.equippedStaff != null){
            equippedStaff.gameObject.SetActive(true);
            equippedStaffSprite = equippedStaff.GetComponent<SpriteRenderer>().sprite;
        }else{
            equippedStaff.gameObject.SetActive(false);
            equippedStaffSprite = null;
        }   

        //Spell switching (pain)
        if(!inventoryUI.isOpen){
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                if(inventory.equippedSpells[0] == null){
                    equippedSpell = null;
                }else{
                    if(activeSpells.Contains(inventory.equippedSpells[0].reference.GetComponent<Spell>())){
                        equippedSpell = activeSpells.Find(e => e.Equals(inventory.equippedSpells[0].reference.GetComponent<Spell>()));
                    }else{
                        equippedSpell = Instantiate(inventory.equippedSpells[0].reference).GetComponent<Spell>();
                        activeSpells.Add(equippedSpell);
                    }
                }
                inventoryUI.equippedSpellsRenderer.SelectSlot(0);
            }else if(Input.GetKeyDown(KeyCode.Alpha2)){
                if(inventory.equippedSpells[1] == null){
                    equippedSpell = null;
                }else{
                    if(activeSpells.Contains(inventory.equippedSpells[1].reference.GetComponent<Spell>())){
                        equippedSpell = activeSpells.Find(e => e.Equals(inventory.equippedSpells[1].reference.GetComponent<Spell>()) );
                    }else{
                        equippedSpell = Instantiate(inventory.equippedSpells[1].reference).GetComponent<Spell>();
                        activeSpells.Add(equippedSpell);
                    }
                }
                inventoryUI.equippedSpellsRenderer.SelectSlot(1);
            }else if(Input.GetKeyDown(KeyCode.Alpha3)){
                if(inventory.equippedSpells[2] == null){
                    equippedSpell = null;
                }else{
                    if(activeSpells.Contains(inventory.equippedSpells[2].reference.GetComponent<Spell>())){
                        equippedSpell = activeSpells.Find(e => e.Equals(inventory.equippedSpells[2].reference.GetComponent<Spell>()) );
                    }else{
                        equippedSpell = Instantiate(inventory.equippedSpells[2].reference).GetComponent<Spell>();
                        activeSpells.Add(equippedSpell);
                    }
                }
                inventoryUI.equippedSpellsRenderer.SelectSlot(2);
            }else if(Input.GetKeyDown(KeyCode.Alpha4)){
                if(inventory.equippedSpells[3] == null){
                    equippedSpell = null;
                }else{
                    if(activeSpells.Contains(inventory.equippedSpells[3].reference.GetComponent<Spell>())){
                        equippedSpell = activeSpells.Find(e => e.Equals(inventory.equippedSpells[3].reference.GetComponent<Spell>()) );
                    }else{
                        equippedSpell = Instantiate(inventory.equippedSpells[3].reference).GetComponent<Spell>();
                        activeSpells.Add(equippedSpell);
                    }
                }
                inventoryUI.equippedSpellsRenderer.SelectSlot(3);
            }
        }
       
        //debug
        if(Input.GetKeyDown(KeyCode.Space)){
            // inventoryUI.inventoryRenderer.MergeSortSortAlpha();
            Debug.Break();
        }

        //shooting spells
        if (Input.GetMouseButtonDown(0)){
                if(equippedSpell != null){
                    equippedSpell.Fire();
                }
        }

        //inventory
        if(Input.GetKeyDown(KeyCode.E)){
                if (inventoryUI.isOpen){
                    if(MousePointer.instance.selectedItem != null){
                        MousePointer.instance.selectedItem.SnapBack();
                    }
                    equippedSpell = null;
                    inventoryUI.Disable();
                }else{
                    inventoryUI.Enable();
                }
        }
        
        //movement
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        characterPos = transform.position; 

        if(equippedSpell == null){
            if(mana < maxMana){
                mana+= 0.1f;

            }
        }else if(Time.time - equippedSpell.nextAvailFire > 1){
            if(mana < maxMana){
                mana+= 0.1f;

            }        
        }
        Debug.Log(health);

        if (Input.GetKeyDown(KeyCode.C)) {
            HealthBarList.EmptyFullHeart();
            health -= 1;
        }

        //if (health <= 0) {
        //    Debug.Break();
        //}

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

    public void EquipStaff(StaffItem staff){
        inventory.equippedStaff = staff;
        equippedStaff.GetComponent<SpriteRenderer>().sprite = staff.GetComponent<Image>().sprite;
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
    public void TakeDamage(int damage){
        if (!_isImmune)
        {
            for(int i = 0; i < damage; i++){
                HealthBarList.EmptyFullHeart();
            }
            health -= damage;
        }
        StartCoroutine(ImmunityHandler());
    }

    IEnumerator ImmunityHandler(){
        _isImmune = true;
        yield return new WaitForSeconds(1);
        _isImmune = false;
    }

    public void IncreaseMaxHealth(){
        HealthBarList.InstantiateHeart();
        maxHealth++;
    }

    public void InstantiateHeart(int num) {
        for (int i = 0; i < num; i++) {
            HealthBarList.InstantiateHeart();
        }
    }

    public void IncreaseMaxHealth(int num){
        for (int i = 0; i < num;i++){
            IncreaseMaxHealth();
        }
    }

    public void GainHeart(){
        if (health < maxHealth)
        {
            HealthBarList.FillEmptyHeart();
            health += 1;
            Debug.Log(health);
        }
    }
    public void GainHeart(int num){
        for(int i = 0;i < num; i++){
            GainHeart();
        }
    }


    public void AddMana(float num){
        if(mana + num > maxMana){
            mana = maxMana;
        }else{
            mana += num;
        }
    }

    public void LoadData(GameData data){
        transform.position = data.playerPos;
        maxHealth = data.maxHealth;
        health = data.health;
        mana = data.mana;

        int[] itemIDs = data.itemsIDs;
        for(int i = 0; i < itemIDs.Length; i++){
            inventory.items[i] = itemLibrary.GetReferenceFromID(itemIDs[i]);
        }

        int[] potionIDs = data.potionIDs;
        for(int i = 0; i < potionIDs.Length; i++){
            inventory.potions[i] = (PotionItem)itemLibrary.GetReferenceFromID(potionIDs[i]);
        }

        int[] spellsIDs = data.spellsIDs;
        for(int i = 0; i < spellsIDs.Length; i++){
            inventory.spells[i] = (SpellItem)itemLibrary.GetReferenceFromID(spellsIDs[i]);
        }
        
    }

    public void SaveData(ref GameData data){
        data.playerPos = transform.position;
        data.health = health;
        data.maxHealth = maxHealth;
        data.health = health;
        data.mana = mana;

        for(int i = 0; i < inventory.items.Length; i++){
            if(inventory.items[i] != null){
                data.itemsIDs[i] = inventory.items[i].itemID;
            }else{
                data.itemsIDs[i] = 0;
            }
        }

        for(int i = 0; i < inventory.potions.Length; i++){
            if(inventory.potions[i] != null){
                data.potionIDs[i] = inventory.potions[i].itemID;
            }else{
                data.potionIDs[i] = 0;
            }
        }

        for(int i = 0; i < inventory.spells.Length; i++){
            if(inventory.spells[i] != null){
                data.spellsIDs[i] = inventory.spells[i].itemID;
            }else{
                data.spellsIDs[i] = 0;
            }
        }
    }

}
