using UnityEngine;

public class ObjectReflection: MonoBehaviour
{
	public GameObject created; // GameObject sẽ đc tạo ra đối diện với source 

    public GameObject source; // GameObject sẽ tác dụng lên gameObject này!

    private Vector2 midPoint; // = ((created.x + source.x)/2,
							  //	(created.y + source.y)/2);   

    private float lastValueX; 

    private float lastValueY;

    private bool isMoving;
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
    [SerializeField] private float originalSpeed = 3f;
	
	private void Start()
    {
        speed = originalSpeed;
    }

    private void Update()
    {
        Movement();
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
            transform.Translate(this.Speed * Time.deltaTime * transform.up, Space.World);
        }
        else 
            this.Speed = originalSpeed;
    }
	
	// Hàm tính toán việc hướng tới mục tiêu nào
    private void DirectTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Source")
        {
            transform.eulerAngles = new (0, 0, Random.Range(110, 130)); // tmp...

            isMoving = true;

            midPoint.y = source.transform.position.y; // set y value tai vi tri xuat phat (source)
												   // khi vua collision voi source
            lastValueX = source.transform.position.x;
            lastValueY = source.transform.position.y;
        }

        if (collision.gameObject.name == "Reflection")
        {
            midPoint.x = this.transform.position.x; // set y value của tại vị trí 
												 // khi vừa chạm 1 reflector (bức tường, rào chắn,...)
            created.transform.position = 
            new(midPoint.x * 2 - lastValueX, midPoint.y * 2 - lastValueY);

            DirectTarget(created.transform.position);
        }
    }
}