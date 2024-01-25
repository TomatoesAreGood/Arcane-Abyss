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
    public static PlayerController Instance;

    //Movement
    public float _moveSpeed;
    private Vector2 _movementDirection;
    public ContactFilter2D ContactFilter;
    private float _collisionOffset = 0.04f;
    public static Vector3 CharacterPos; //used by enemy and bullet class 

    //Components
    private SpriteRenderer _sr;
    private Animator _animator;
    private Rigidbody2D _rb;
    private List<RaycastHit2D> _raycastHit2Ds;

    //Player Stats
    public static int MaxHealth;
    public static int Health;
    public static float MaxMana = 100;
    public static float Mana;
    private bool _isImmune;
    public int Coins;

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
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(this);
        }

        //set default player stats
        activeSpells = new List<Spell>();
        _moveSpeed = 5f;
        inventoryHeight = 4;
        inventoryWidth = 10;
        spellInventorySize = 8;
        potionBagSize = 4;
        Coins = 0;

        //creating data storage (for saving)
        inventory = new Inventory(inventoryWidth*inventoryHeight, spellInventorySize, potionBagSize);

        //components
        _rb = GetComponent<Rigidbody2D>();
        _raycastHit2Ds = new List<RaycastHit2D>(0);
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        equippedStaffSprite = equippedStaff.GetComponent<SpriteRenderer>().sprite;

        //for shooting magic
        pivot = transform.GetChild(0).gameObject.transform;
        firePoint = pivot.transform.GetChild(0);
        
        MaxMana = 100;
        Mana = MaxMana;

        // Mana Bar UI has same max mana as player stats
        ManaBar.SetMaxMana(MaxMana);
        ManaBar.SetMana(MaxMana);

    }
    private void Start(){
        inventoryUI.UpdateData();

        // after increasing max health, set current health to max health
        InstantiateHeart(MaxHealth);
        int difference = MaxHealth - Health;
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
                    //equippedSpell = null;
                    inventoryUI.Disable();
                }else{
                    inventoryUI.Enable();
                }
        }
        
        //movement
        _movementDirection.x = Input.GetAxisRaw("Horizontal");
        _movementDirection.y = Input.GetAxisRaw("Vertical");
        CharacterPos = transform.position; 

        if(equippedSpell == null){
            if(Mana < MaxMana){
                Mana+= 0.1f;

            }
        }else if(Time.time - equippedSpell.nextAvailFire > 1){
            if(Mana < MaxMana){
                Mana+= 0.1f;

            }        
        }

        if (Health <= 0)
        {
            DeathManager.instance.isDead = true;
        }

    }


    private void FixedUpdate() {
        //movement loop
        if(_movementDirection != Vector2.zero){
            if(CanMove(_movementDirection)){
                _rb.MovePosition(_rb.position + _movementDirection * _moveSpeed * Time.fixedDeltaTime);
                _animator.SetBool("isWalking", true);
            }else{
                if(CanMove(new Vector2(_movementDirection.x, 0))){
                    _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * new Vector2(_movementDirection.x, 0));
                    _animator.SetBool("isWalking", true);
                }else if(CanMove(new Vector2(0, _movementDirection.y))){
                        _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * new Vector2(0, _movementDirection.y));
                        _animator.SetBool("isWalking", true);
                }else{
                    _animator.SetBool("isWalking", false);
                } 
            }
        }else{
            _animator.SetBool("isWalking", false); 
        }

        //flip the character sprite to face movement direction
        if(_movementDirection.x > 0){
            _sr.flipX = false;
        }else if(_movementDirection.x < 0){
            _sr.flipX = true;
        }
    }


    private bool CanMove(Vector2 direction){
        if (direction != Vector2.zero){
            int raycastHits = _rb.Cast(direction, ContactFilter, _raycastHit2Ds, _moveSpeed * Time.fixedDeltaTime + _collisionOffset);
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

    public int FindSpellInInventory(SpellItem spell){
        for(int i = 0 ; i < inventory.spells.Length ;i++){
            if(inventory.spells[i] == null){
                continue;
            }   
            if(inventory.spells[i].GetType().Name == spell.GetType().Name){
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
            Health -= damage;
            SoundManager.instance.PlayPlayerDamageSFX();
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
        MaxHealth++;
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
        if (Health < MaxHealth)
        {
            HealthBarList.FillEmptyHeart();
            Health += 1;
            Debug.Log(Health);
        }
    }
    public void GainHeart(int num){
        for(int i = 0;i < num; i++){
            GainHeart();
        }
    }


    public void AddMana(float num){
        if(Mana + num > MaxMana){
            Mana = MaxMana;
        }else{
            Mana += num;
        }
    }

    public void LoadData(GameData data){
        transform.position = data.playerPos;
        MaxHealth = data.maxHealth;
        Health = data.health;
        Mana = data.mana;
        Coins = data.coins;

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
        data.health = Health;
        data.maxHealth = MaxHealth;
        data.health = Health;
        data.mana = Mana;
        data.coins = Coins;


        for(int i = 0; i < inventory.items.Length; i++){
            if(inventory.items[i] != null){
                int itemID = ItemLibrary.instance.GetIDFromReference(inventory.items[i]);
                data.itemsIDs[i] = itemID;
            }else{
                data.itemsIDs[i] = 0;
            }
        }

        for(int i = 0; i < inventory.potions.Length; i++){
            if(inventory.potions[i] != null){
                int itemID = ItemLibrary.instance.GetIDFromReference(inventory.potions[i]);
                data.potionIDs[i] = itemID;
            }else{
                data.potionIDs[i] = 0;
            }
        }

        for(int i = 0; i < inventory.spells.Length; i++){
            if(inventory.spells[i] != null){
                int itemID = ItemLibrary.instance.GetIDFromReference(inventory.spells[i]);
                data.spellsIDs[i] = itemID;
            }else{
                data.spellsIDs[i] = 0;
            }
        }
    }

    public bool HasKey()
    {
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i] is Key)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveKey()
    {
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i] is Key)
            {
                inventory.items[i] = null;
                inventoryUI.inventoryRenderer.RedrawMatrix();
            }
        }
    }


}
