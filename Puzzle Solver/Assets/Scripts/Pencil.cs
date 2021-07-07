using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    [SerializeField] float movementSpeed = 20f;
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] float pencilFallWaitTime = 0.7f;
    //Vector3 targetPos;

    
    GameObject mainGameObject;
    float movementThisFrame;
    float rotationThisFrame;
    Transform targetTransform;
    bool movePencil = false;
    MainPencil mainPencil;

    bool reorder = false;
    Transform newTarget;
    Rotator rotator;

    //GameSession gameSession;

    
    // List<int> emptyCell;
    // int index;
    // Start is called before the first frame update

    private void Awake()
    {
        rotator = FindObjectOfType<Rotator>();
       // gameSession = FindObjectOfType<GameSession>();
    }
    void Start()
    {
       
        //transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
    }

    // Update is called once per frame
   void FixedUpdate()
    {
        //Vector3 newRotation = new Vector3(0f, 90f, 0f);
        //transform.Rotate(0, 90f, 0);
        if (movePencil)
        {
            movementThisFrame = movementSpeed * Time.deltaTime;
            rotationThisFrame = rotationSpeed * Time.deltaTime;
        //    var dir = new Vector3(mainTransform.position.x, mainTransform.position.y, mainTransform.position.z) - transform.position;
            Quaternion rotation = Quaternion.AngleAxis(180, Vector3.forward);
            // Quaternion.LookRotation(dir, -Vector3.forward);
            //int zOff = emptyCell[1];
           // Vector3 targetPos = new Vector3(mainTransform.position.x, mainTransform.position.y, mainTransform.position.z - 0.410721f * zOff);
          //  Vector3 dir = targetPos - this.transform.position;

           
         //   Debug.Log(targetPos + ", " + transform.position);
            // int yOff = 0; //temp
            transform.position = Vector3.MoveTowards
                             (transform.position,
                            // new Vector3(mainTransform.position.x - .38f , mainTransform.position.y, mainTransform.position.z),
                            targetTransform.position,
                             movementThisFrame);

           // this.transform.Translate(dir.normalized * movementThisFrame, Space.World);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, rotationThisFrame);
            if(transform.position == targetTransform.position)
            {
                transform.parent = targetTransform.parent;
                movePencil = false;
            }
        }

        if(reorder)
        {
            movementThisFrame = movementSpeed * Time.deltaTime;
            rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards
                             (transform.position,
                            newTarget.position,
                             movementThisFrame);
            transform.rotation = Quaternion.Lerp(transform.rotation, newTarget.rotation, rotationThisFrame);
            if (transform.position == newTarget.position)
            {
                //transform.parent = targetTransform.parent;
                reorder = false;
            }
        }
     
        
    }

    public void ActivatePencil(Transform targetTransform, GameObject mainGameObject)
    {
        this.targetTransform = targetTransform;
        this.mainGameObject = mainGameObject;
        this.mainPencil = this.mainGameObject.GetComponent<MainPencil>();
       
        //Debug.Log("Pencil cell : " + this.emptyCell[0] + "," + this.emptyCell[1] + "\n" + "Pencil Switch Val : " + this.switchSide);
        movePencil = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Pencil" && movePencil)
        {
            //rotator.GetComponent<Rotator>().ZoomOut();
            other.transform.GetComponent<Collider>().isTrigger = false;
           // gameSession.AddPencil();   

            //other.transform.parent = transform.parent;
            int emptyCell;
            emptyCell = this.mainPencil.GetEmptyCell(gameObject);
            if (emptyCell >= 0)
            {
                //Debug.Log("Empty cell : " + emptyCell);
                mainPencil.AddPencil(emptyCell, other.gameObject);
                Transform[] pencilSlots = mainPencil.GetPencilSlots();
                other.GetComponent<Pencil>().ActivatePencil(pencilSlots[emptyCell], mainGameObject);
            }

        }

        if(other.tag  == "Finish")
        {
            rotator.Finish();
        }

    }

    public void ReOrder(bool reorder, Transform newTarget)
    {
        movementSpeed = 1;
        this.reorder = reorder;
        this.newTarget = newTarget;
    }
    
    public void Remove()
    {
        mainPencil.DeletePencil(gameObject);
        mainPencil.Sort();
        Destroy(gameObject, 0f);
    }


    public IEnumerator SpawnComplete()
    {
        yield return new WaitForSeconds(pencilFallWaitTime);

        //Debug.Log("sdfsfdas");
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().isTrigger = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
