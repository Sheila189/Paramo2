using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaZombie : MonoBehaviour
{
    public int hp;
    public int dañoArma;
    public int dañoPuño;
    public Animator anim;

    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "armaImpacto")
        {
            if (anim != null)
            {
                anim.SetTrigger("isHit");
            }
            hp -= dañoArma;
        }
        else if (other.gameObject.tag == "golpeImpacto")
        {
            if (anim != null)
            {
                anim.SetTrigger("isHit");
            }
            hp -= dañoPuño;
        }

        if (hp <= 0)
        {
            if (anim != null)
            {
                anim.SetTrigger("isDead");
            }
            Destroy(gameObject, 2f); // Ajusta el tiempo según la duración de la animación de muerte
        }
    }

    public void Attack()
    {
        if (anim != null)
        {
            anim.SetTrigger("isAttacking");
        }
    }
}
