using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour, IDamageable
{
    [SerializeField] private fov _fov;
    private int health = 2;
    public int disguise = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spot = new Vector3(-.2f, .7f, 0) + transform.position;
        _fov.SetOrigin(spot);
        _fov.SetAimDirection(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage();
        }
    }

    public void Damage()
    {
        // If not alerted deal 2 damage, 1 if in alert state
        health -= 2;
        if (health <= 0)
            Debug.Log("Guard Died");
        return;
    }
}
