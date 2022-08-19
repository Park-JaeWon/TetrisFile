using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnertetris : MonoBehaviour
{
    public GameObject[] Tetris;
    public GameObject[] NextTetris;

    public GameObject currenTetris = null;
    public GameObject nextTetris = null;

    private int nextTetrisnum = 0;
    // Start is called before the first frame update
    void Start()
    {
        MakeTetris();
    }

    public void NewTetirs()
    {
    }

    public void MakeTetris()
    {
        if(NextTetris == null)
        {
            var Tetrisnum = Random.Range(0, Tetris.Length);
            currenTetris = Instantiate(Tetris[Tetrisnum], transform.position, Quaternion.identity);
        }
        else
        {
            currenTetris = Instantiate(Tetris[nextTetrisnum], transform.position, Quaternion.identity);
        }

        MakeNextTetris();
    }
    public void MakeNextTetris()
    {
        nextTetrisnum = Random.Range(0, NextTetris.Length);
        Vector3 nextTetrisPos = new Vector3(-2.5f, 16.2f);
        if (nextTetrisnum == 0)
            nextTetrisPos += new Vector3(0.2f, 0.2f);
        else if (nextTetrisnum == 1)
            nextTetrisPos += new Vector3(0.2f, 0.0f);

        nextTetris = Instantiate(NextTetris[nextTetrisnum], nextTetrisPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
