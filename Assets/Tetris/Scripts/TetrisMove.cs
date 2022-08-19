using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TetrisMove : MonoBehaviour
{
    private float pTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    public Vector3 rotationP;
    private static Transform[,] grid = new Transform[width, height];

    Vector3 moveDir = Vector3.zero;

    bool isGameOver = false;

    void Awake()
    {
     
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!PossibleMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!PossibleMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //회전하기
            transform.RotateAround(transform.TransformPoint(rotationP), new Vector3(0, 0, 1), 90);
            if (!PossibleMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationP), new Vector3(0, 0, 1), -90);
            }
        }

        //Time.time = 플레이시간
        if(Time.time - pTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/14 : fallTime)) // 삼항연산자 아래키누르면 fallTime/10 아니면 fallTime
        {
            transform.position += new Vector3(0, -1, 0);
            if (!PossibleMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid(); //블럭겹치는거 제한
                CheckForLines();  //블럭 삭제, 줄내리기, 스코어 증가
                this.enabled = false; //스크립트 체크해제
                if (!isGameOver)
                {
                    Destroy(FindObjectOfType<Spawnertetris>().nextTetris);
                    FindObjectOfType<Spawnertetris>().MakeTetris(); //바닥에 닿으면 블록생성
                    //Destroy(FindObjectOfType<Spawnertetris>().nextTetris);
                }
                
            }
            pTime = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            while (SpaceTetro(false)) { }
        }
    }

    void CheckForLines()
    {
        for(int i = height - 1; i >= 0; i--) // 테트리스 블록을 맨 윗줄부터 아래까지 검색한다.
        {
            if(HasLine(i)) // 줄이 블록으로 꽉차 있을경우
            {
                DeleteLine(i);
                RowDown(i);
                Score.scores += 100;
                SoundManager.instance.PlayLineSound();
            }
        }
    }

    bool HasLine(int i) //줄이 블록으로 꽉차있는지 체크 i = 첫값 -> 19 ~
    {
        for(int j = 0; j < width; j++)
        {
            if (grid[j, i] == null) //[1 ~ 10, 19 ~ 0]해당 칸에 오브젝트가 없다면
                return false;
        }
        return true; // null이아니다 = 해당줄이 꽉찬것이다.
    }

    void DeleteLine(int i) // 줄을 삭제한다.
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i) // 줄을 내린다
    {
        for(int y = i; y < height; y++)
        {
            for(int j = 0; j < width; j++)
            {
                if (grid[j, y] != null) // 꽉차면 윗줄을 아랫줄로 복사
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }


    void AddToGrid() // 블럭 겹치게 않게한다.
    {
        foreach (Transform block in transform) //데이터형식 변수명 in 배열
        {
            int roundX = Mathf.RoundToInt(block.transform.position.x);
            int roundY = Mathf.RoundToInt(block.transform.position.y);
            SoundManager.instance.PlayDropSound();

            if (roundY < 17)
                grid[roundX, roundY] = block;
            else
            {
                Gameover();
                SoundManager.instance.PlayOverSound();
            }
        }
    }

    void Gameover()
    {
        isGameOver = true;
        Debug.Log("Game Over");
        SceneManager.LoadScene("End");
    }

    bool PossibleMove() // 블럭의 공간을제약한다
    {
        foreach(Transform block in transform) //데이터형식 변수명 in 배열
        {
            int roundX = Mathf.RoundToInt(block.transform.position.x);
            int roundY = Mathf.RoundToInt(block.transform.position.y);

            if(roundX < 0 || roundX >= width || roundY < 0 || roundY >= height) // or연산자, block이 너비와 높이를 넘어가지못함
            {
                return false;
            }

            if(grid[roundX, roundY] != null) //꽉차면 못감
            {
                return false;
            }
        }

        return true;
    }
    bool SpaceTetro(bool isRotate)
    {
        transform.position += new Vector3(0, -1, 0);
        if (!PossibleMove())
        {
            transform.position -= new Vector3(0, -1, 0);
            return false;
        }

        return true;
    }
}

