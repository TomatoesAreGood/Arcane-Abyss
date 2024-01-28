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
    public float MoveSpeed;
    private Vector2 _movementDirection;
    public ContactFilter2D ContactFilter;
    private float _collisionOffset = 0.04f;
    public static Vector3 CharacterPos; 

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

    //Spells
    [HideInInspector] public Spell equippedSpell;
    private List<Spell> _activeSpells;

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
        _activeSpells = new List<Spell>();
        MoveSpeed = 5f;
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
        if(inventory.EquippedStaff != null){
            equippedStaff.gameObject.SetActive(true);
            equippedStaffSprite = equippedStaff.GetComponent<SpriteRenderer>().sprite;
        }else{
            equippedStaff.gameObject.SetActive(false);
            equippedStaffSprite = null;
        }   

        //Spell switching (pain)
        if(!inventoryUI.IsOpen){
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                if(inventory.EquippedSpells[0] == null){
                    equippedSpell = null;
                }else{
                    if(_activeSpells.Contains(inventory.EquippedSpells[0].reference.GetComponent<Spell>())){
                        equippedSpell = _activeSpells.Find(e => e.Equals(inventory.EquippedSpells[0].reference.GetComponent<Spell>()));
                    }else{
                        equippedSpell = Instantiate(inventory.EquippedSpells[0].reference).GetComponent<Spell>();
                        _activeSpells.Add(equippedSpell);
                    }
                }
                inventoryUI.EquippedSpellsRenderer.SelectSlot(0);
            }else if(Input.GetKeyDown(KeyCode.Alpha2)){
                if(inventory.EquippedSpells[1] == null){
                    equippedSpell = null;
                }else{
                    if(_activeSpells.Contains(inventory.EquippedSpells[1].reference.GetComponent<Spell>())){
                        equippedSpell = _activeSpells.Find(e => e.Equals(inventory.EquippedSpells[1].reference.GetComponent<Spell>()) );
                    }else{
                        equippedSpell = Instantiate(inventory.EquippedSpells[1].reference).GetComponent<Spell>();
                        _activeSpells.Add(equippedSpell);
                    }
                }
                inventoryUI.EquippedSpellsRenderer.SelectSlot(1);
            }else if(Input.GetKeyDown(KeyCode.Alpha3)){
                if(inventory.EquippedSpells[2] == null){
                    equippedSpell = null;
                }else{
                    if(_activeSpells.Contains(inventory.EquippedSpells[2].reference.GetComponent<Spell>())){
                        equippedSpell = _activeSpells.Find(e => e.Equals(inventory.EquippedSpells[2].reference.GetComponent<Spell>()) );
                    }else{
                        equippedSpell = Instantiate(inventory.EquippedSpells[2].reference).GetComponent<Spell>();
                        _activeSpells.Add(equippedSpell);
                    }
                }
                inventoryUI.EquippedSpellsRenderer.SelectSlot(2);
            }else if(Input.GetKeyDown(KeyCode.Alpha4)){
                if(inventory.EquippedSpells[3] == null){
                    equippedSpell = null;
                }else{
                    if(_activeSpells.Contains(inventory.EquippedSpells[3].reference.GetComponent<Spell>())){
                        equippedSpell = _activeSpells.Find(e => e.Equals(inventory.EquippedSpells[3].reference.GetComponent<Spell>()) );
                    }else{
                        equippedSpell = Instantiate(inventory.EquippedSpells[3].reference).GetComponent<Spell>();
                        _activeSpells.Add(equippedSpell);
                    }
                }
                inventoryUI.EquippedSpellsRenderer.SelectSlot(3);
            }
        }

        //shooting spells
        if (Input.GetMouseButtonDown(0)){
                if(equippedSpell != null && Time.timeScale != 0f){
                    equippedSpell.Fire();
                }
        }

        //inventory
        if(Input.GetKeyDown(KeyCode.E)){
                if (inventoryUI.IsOpen){
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

        //increase max mana if player hasn't fired in a while
        if(equippedSpell == null){
            if(Mana < MaxMana){
                Mana+= 0.1f;

            }
        }else if(Time.time - equippedSpell.NextAvailFire > 1){
            if(Mana < MaxMana){
                Mana+= 0.1f;

            }        
        }

        //player death
        if (Health <= 0)
        {
            DeathManager.instance.isDead = true;
        }

    }


    private void FixedUpdate() {
        //movement loop
        if(_movementDirection != Vector2.zero){
            if(CanMove(_movementDirection)){
                _rb.MovePosition(_rb.position + _movementDirection * MoveSpeed * Time.fixedDeltaTime);
                _animator.SetBool("isWalking", true);
            }else{
                if(CanMove(new Vector2(_movementDirection.x, 0))){
                    _rb.MovePosition(_rb.position + MoveSpeed * Time.fixedDeltaTime * new Vector2(_movementDirection.x, 0));
                    _animator.SetBool("isWalking", true);
                }else if(CanMove(new Vector2(0, _movementDirection.y))){
                        _rb.MovePosition(_rb.position + MoveSpeed * Time.fixedDeltaTime * new Vector2(0, _movementDirection.y));
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
            int raycastHits = _rb.Cast(direction, ContactFilter, _raycastHit2Ds, MoveSpeed * Time.fixedDeltaTime + _collisionOffset);
            if(raycastHits == 0){
                return true;
            }
            return false;
        }
        return false;
    }

    public void EquipStaff(StaffItem staff){
        inventory.EquippedStaff = staff;
        equippedStaff.GetComponent<SpriteRenderer>().sprite = staff.GetComponent<Image>().sprite;
    }

    public void EquipSpell(SpellItem spell, int spellIndex){
        if(FindEquippedSpell(spell) != -1){
            inventory.EquippedSpells[FindEquippedSpell(spell)] = null;
        }
        inventory.EquippedSpells[spellIndex] = spell;

    }
    public int FindEquippedSpell(SpellItem spell){
        for(int i = 0 ; i < 4 ;i++){
            if(inventory.EquippedSpells[i] == spell){
                return i;
            }
        }
        return -1;
    }

    public int FindSpellInInventory(SpellItem spell){
        for(int i = 0 ; i < inventory.Spells.Length ;i++){
            if(inventory.Spells[i] == null){
                continue;
            }   
            if(inventory.Spells[i].GetType().Name == spell.GetType().Name){
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
    
    //when game starts up
    public void LoadData(GameData data){
        transform.position = data.playerPos;
        MaxHealth = data.maxHealth;
        Health = data.health;
        Mana = data.mana;
        Coins = data.coins;

        int[] itemIDs = data.itemsIDs;
        for(int i = 0; i < itemIDs.Length; i++){
            inventory.Items[i] = itemLibrary.GetReferenceFromID(itemIDs[i]);
        }

        int[] potionIDs = data.potionIDs;
        for(int i = 0; i < potionIDs.Length; i++){
            inventory.Potions[i] = (PotionItem)itemLibrary.GetReferenceFromID(potionIDs[i]);
        }

        int[] spellsIDs = data.spellsIDs;
        for(int i = 0; i < spellsIDs.Length; i++){
            inventory.Spells[i] = (SpellItem)itemLibrary.GetReferenceFromID(spellsIDs[i]);
        }
        
    }

    //when game closes
    public void SaveData(ref GameData data){
        data.playerPos = transform.position;
        data.health = Health;
        data.maxHealth = MaxHealth;
        data.health = Health;
        data.mana = Mana;
        data.coins = Coins;

        for(int i = 0; i < inventory.Items.Length; i++){
            if(inventory.Items[i] != null){
                int itemID = ItemLibrary.instance.GetIDFromReference(inventory.Items[i]);
                data.itemsIDs[i] = itemID;
            }else{
                data.itemsIDs[i] = 0;
            }
        }

        for(int i = 0; i < inventory.Potions.Length; i++){
            if(inventory.Potions[i] != null){
                int itemID = ItemLibrary.instance.GetIDFromReference(inventory.Potions[i]);
                data.potionIDs[i] = itemID;
            }else{
                data.potionIDs[i] = 0;
            }
        }

        for(int i = 0; i < inventory.Spells.Length; i++){
            if(inventory.Spells[i] != null){
                int itemID = ItemLibrary.instance.GetIDFromReference(inventory.Spells[i]);
                data.spellsIDs[i] = itemID;
            }else{
                data.spellsIDs[i] = 0;
            }
        }
    }

    public bool HasKey()
    {
        for (int i = 0; i < inventory.Items.Length; i++)
        {
            if (inventory.Items[i] is Key)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveKey()
    {
        for (int i = 0; i < inventory.Items.Length; i++)
        {
            if (inventory.Items[i] is Key)
            {
                inventory.Items[i] = null;
                inventoryUI.InventoryRenderer.RedrawMatrix();
            }
        }
    }


}
