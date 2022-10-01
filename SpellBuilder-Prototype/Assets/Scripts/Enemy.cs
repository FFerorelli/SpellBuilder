using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAction
{
    [SerializeField]
    Health target;
    Player player;
   // Player player;
    float timeSinceLastAttack = Mathf.Infinity;
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] float range = 2f;
    [SerializeField] float damage = 10f;
    [SerializeField] float timeBetweenAttacks = 1f;
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
            MoveTo(target);
        }
        else
        {
            //GetComponent<Mover>().Cancel();
            //AttackBehaviour();
        }
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

}
