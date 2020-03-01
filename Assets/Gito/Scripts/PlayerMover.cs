using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private int posNum = 1, prevPos = 1;
    private int column;
    private Rigidbody rb;
    private float speed;
    [SerializeField] private float playerStepSpeed = 5.0f;

    private SoundEffecter soundEffecter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        soundEffecter = GameObject.FindWithTag("SoundEffect").GetComponent<SoundEffecter>();
    }

    public void MoveStart(float speed, int column)
    {
        this.speed = speed;
        this.column = column;
    }

    private int InputLeftRight()
    {
        if (Input.GetButtonDown("Right"))
        {
            return 1;
        }
        if (Input.GetButtonDown("Left"))
        {
            return -1;
        }
        return 0;
    }

    public void PUpdate()
    {
        rb.velocity = new Vector3(0.0f, 0.0f, -speed);

        posNum += InputLeftRight();
        if(posNum < 0)
        {
            posNum = 0;
        }
        if(posNum > column - 1)
        {
            posNum = column - 1;
        }
        if (posNum != prevPos)
        {
            soundEffecter.SoundEffect(SoundEffect.Swing);
        }
        prevPos = posNum;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, 0.75f + (posNum * 1.25f), playerStepSpeed * Time.deltaTime), 0.5f, transform.position.z);
    }
}
