using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private string aimalName; //동물의 이름
    [SerializeField] private int hp; // 동물의 체력
    [SerializeField] private float walkSpeed; // 걷기 속도

    

    //상태변수

    private bool isAction; // 행동중인지 아닌지 판별
    private bool isWalking; // 걷는지 걷지 않는지 판별

    [SerializeField] private float walkTime; //걷기 시간
    [SerializeField] private float waitTime; //대기 시간
    private float currentTime;

    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;
          
    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        ElapseTime();
    }

    private void ElapseTime()
    {
        if(isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ;                //다음 랜덤행동개시
        }
    }

    private void RandomAction()
    {
        isAction = true;
        //대기 풀뜯기 두리번 걷기
        int _random = Random.Range(0, 4);

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
        Debug.Log("먹기");
    }
    private void Peek()
    {
        currentTime = waitTime;
        Debug.Log("두리번");
    }
    private void TryWalk()
    {
        currentTime = waitTime;
        Debug.Log("걷기");
    }
}

