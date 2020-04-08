﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour {

    [SerializeField] protected string animalName; //동물의 이름
    [SerializeField] protected int hp; //동물의 체력

    [SerializeField] protected float walkSpeed; // 걷기 스피드
    [SerializeField] protected float runSpeed; // 뛰기 스피드
    
    protected Vector3 destination; // 목적지

    //상태변수
    protected bool isAction; //행동중인지 아닌지 판별
    protected bool isWalking; // 걷는지 안걷는지 판별
    protected bool isRunning; // 뛰는지 안걷는지 판별
    protected bool isDead;    // 죽었는지 판별

    [SerializeField] protected float walkTime; //걷기시간
    [SerializeField] protected float waitTime; //대기 시간
    [SerializeField] protected float runTime; //대기 시간
    protected float currentTime;

    //필요한 컴포넌트
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider Boxcol;
    protected AudioSource theAudio;
    protected NavMeshAgent nav;

    [SerializeField] protected AudioClip[] sound_Nomal;
    [SerializeField] protected AudioClip sound_Hurt;
    [SerializeField] protected AudioClip sound_Dead;

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            ElapseTime();
        }
    }

    protected void Move()
    {
        if (isWalking || isRunning)
            nav.SetDestination(transform.position + destination * 5f);
    }

    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();
        }
    }

    protected virtual void ReSet()
    {
        isWalking = false; isRunning = false; isAction = true;
        nav.speed = walkSpeed;
        nav.ResetPath();
        anim.SetBool("Walking", isWalking); anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
    }

    protected void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        nav.speed = walkSpeed;
        Debug.Log("걷기");
    }


    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }
            PlayeSE(sound_Hurt);
            anim.SetTrigger("Hurt");
         }
    }

    protected void Dead()
    {
        PlayeSE(sound_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");

    }

    protected void RandomSound()
    {
        int _random = Random.Range(0, 3);
        PlayeSE(sound_Nomal[_random]);
    }

    protected void PlayeSE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
