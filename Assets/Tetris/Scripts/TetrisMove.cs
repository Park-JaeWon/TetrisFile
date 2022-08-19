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
            //ȸ���ϱ�
            transform.RotateAround(transform.TransformPoint(rotationP), new Vector3(0, 0, 1), 90);
            if (!PossibleMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationP), new Vector3(0, 0, 1), -90);
            }
        }

        //Time.time = �÷��̽ð�
        if(Time.time - pTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/14 : fallTime)) // ���׿����� �Ʒ�Ű������ fallTime/10 �ƴϸ� fallTime
        {
            transform.position += new Vector3(0, -1, 0);
            if (!PossibleMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid(); //����ġ�°� ����
                CheckForLines();  //�� ����, �ٳ�����, ���ھ� ����
                this.enabled = false; //��ũ��Ʈ üũ����
                if (!isGameOver)
                {
                    Destroy(FindObjectOfType<Spawnertetris>().nextTetris);
                    FindObjectOfType<Spawnertetris>().MakeTetris(); //�ٴڿ� ������ ��ϻ���
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
        for(int i = height - 1; i >= 0; i--) // ��Ʈ���� ����� �� ���ٺ��� �Ʒ����� �˻��Ѵ�.
        {
            if(HasLine(i)) // ���� ������� ���� �������
            {
                DeleteLine(i);
                RowDown(i);
                Score.scores += 100;
                SoundManager.instance.PlayLineSound();
            }
        }
    }

    bool HasLine(int i) //���� ������� �����ִ��� üũ i = ù�� -> 19 ~
    {
        for(int j = 0; j < width; j++)
        {
            if (grid[j, i] == null) //[1 ~ 10, 19 ~ 0]�ش� ĭ�� ������Ʈ�� ���ٸ�
                return false;
        }
        return true; // null�̾ƴϴ� = �ش����� �������̴�.
    }

    void DeleteLine(int i) // ���� �����Ѵ�.
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i) // ���� ������
    {
        for(int y = i; y < height; y++)
        {
            for(int j = 0; j < width; j++)
            {
                if (grid[j, y] != null) // ������ ������ �Ʒ��ٷ� ����
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }


    void AddToGrid() // �� ��ġ�� �ʰ��Ѵ�.
    {
        foreach (Transform block in transform) //���������� ������ in �迭
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

    bool PossibleMove() // ���� �����������Ѵ�
    {
        foreach(Transform block in transform) //���������� ������ in �迭
        {
            int roundX = Mathf.RoundToInt(block.transform.position.x);
            int roundY = Mathf.RoundToInt(block.transform.position.y);

            if(roundX < 0 || roundX >= width || roundY < 0 || roundY >= height) // or������, block�� �ʺ�� ���̸� �Ѿ������
            {
                return false;
            }

            if(grid[roundX, roundY] != null) //������ ����
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

