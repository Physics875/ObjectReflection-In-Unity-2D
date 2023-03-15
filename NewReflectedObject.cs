using UnityEngine;

public class NewReflectedObject : MonoBehaviour
{
    [SerializeField] private bool isMoving;
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField] private float originalSpeed = 4;

    [SerializeField] private Transform direction; // hướng chuyển động của GameObject, là con của GameObject này 

    //[SerializeField] private Transform inDirection;
    //[SerializeField] private Transform inNormal;
    //[SerializeField] private Transform result;

    private void Start()
    {
        speed = originalSpeed;
        direction = this.transform.GetChild(0); // trả về con vị trí đầu tiên của transform này
    }

    private void Update()
    {
        Movement();
        //result.position = Vector3.Reflect(inDirection.right, inNormal.up) * 5;
        //Debug.DrawRay(inDirection.right, inDirection.right * 5, Color.green);
        //Debug.DrawRay(inNormal.right, inNormal.right * 5, Color.red);
        //Debug.DrawRay(inNormal.position, result.position, Color.blue);
    }

    private void Movement()
    {
        if (isMoving)
        {
            this.Speed -= Time.deltaTime;
            if (this.Speed < 0)
            {
                isMoving = false;
            }
            transform.Translate(this.Speed * Time.deltaTime * direction.up, Space.World);
        }
        else
            this.Speed = originalSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Reflector") // bề mặt gây ra lực phản xạ
        {
            isMoving = true;
            //collision.GetContact(0).normal
            //Đây là vectơ pháp tuyến, tức là vectơ vuông góc với bề mặt hoặc Collider2D tới tại điểm tiếp xúc.
            direction.up = Vector3.Reflect(direction.up.normalized, collision.GetContact(0).normal); 
        }
    }
    
    /*
	=Các method tính toán sự phản xạ trong Unity=
	
	float Dot(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }

    Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
    {
        float num = -2f * Dot(inNormal, inDirection);
        return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
    }
	*/
}
