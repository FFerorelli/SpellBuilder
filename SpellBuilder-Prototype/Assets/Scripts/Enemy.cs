using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAction, IChannelable
{
    [SerializeField] Health target; 
    Player player;
    float timeSinceLastAttack = Mathf.Infinity;
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] float range = 2f;
    [SerializeField] float damage = 10f;
    [SerializeField] float timeBetweenAttacks = 1f;

    [SerializeField] float power = 10f;
    [SerializeField] SpellType spellType = SpellType.BASIC;

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
        Player player = target.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (target == null) return;
        if (target.IsDead()) return;
        if (target != null && !IsInRange())
        {
            if (power > 0.1)
                MoveTo(target);
        }
        else
        {
            //GetComponent<Mover>().Cancel();
            //AttackBehaviour();
        }
    }

    public float GetPower()
    {
        return power;
    }

    private void MoveTo(Health target)
    {
        transform.Translate(target.transform.position * Time.deltaTime * movementSpeed);
    }

    void Hit()
    {
        if (target == null) return;

        target.TakeDamage(damage);
    }

    private bool IsInRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) < range;
    }

    public bool CanAttack(GameObject combatTarget)
    {
        if (combatTarget == null) return false;

        Health targetToTest = combatTarget.GetComponent<Health>();
        return targetToTest != null && !targetToTest.IsDead();
    }

    public void Cancel()
    {
        target = null;
        //StopAttack();
        //GetComponent<Mover>().Cancel();
    }


    //IChannelable Interface
    //_______________________________________________________________




    private Channel channel;
    bool isChanneled = false;
    [SerializeField] bool isChannelable = true;

    public SpellType GetSpellType()
    {
        return spellType;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public bool IsChannelable()
    {
        return isChannelable;
    }

    public float DrainPower(float amount)
    {
        float to_remove = Mathf.Min(power,amount);
        power -= to_remove;
        if (power < 0.1)
            isChannelable = false;
        return to_remove;
    }
    public bool ChannelAttach(Channel channel)
    {
        if (!IsChannelable())
            return false;

        Debug.Log("Getting Channeled");
        this.channel = channel;
        isChanneled = true;
        return true;
    }
    public void ChannelInterrupted()
    {
        Debug.Log($"{this.name} Not channeled anymore");
        isChanneled = false;
    }
    //_______________________________________________________________



}
