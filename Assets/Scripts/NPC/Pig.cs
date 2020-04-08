﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : WeakAnimal {
    
    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();
    }

    private void RandomAction()
    {
        RandomSound();

        int _random = Random.Range(0, 4); //대기 , 풀뜯기 , 두리번, 겯기

        if (_random == 0)
            Wait();
        if (_random == 1)
            Eat();
        if (_random == 2)
            Peek();
        if (_random == 3)
            TryWalk();
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("풀뜯기");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Use this for initialization

}