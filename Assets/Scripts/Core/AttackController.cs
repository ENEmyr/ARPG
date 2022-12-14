using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using ItemSystem;
using CharacterSystem;

public class AttackController : MonoBehaviour
{
    // private ARSessionOrigin aRSession;
    private GameObject DartTemp;
    private Rigidbody rb;
    private Transform player;
    [SerializeField]
    private Transform DartThrowPoint;
    [SerializeField]
    private Transform firePoint;
    private bool shootBool = false;

    void Start()
    {
        // aRSession = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
        player = Player.s_Instance.transform;
    }

    void Update()
    {
        // if (PlaceObjectOnPlane.key)
        // {
        if (EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand) != null)
        {
            Equipment weapon = EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand);
            firePoint = GameObject.Find(weapon.Prefab.name + "(Clone)").transform.Find("Fire Point").GetComponent<Transform>();
            DartThrowPoint = GameObject.Find(weapon.Prefab.name + "(Clone)").transform.Find("DartThrowPoint").GetComponent<Transform>();
            if (Input.GetMouseButtonDown(0))
            // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                Skill sk = EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand).EquipmentSkill;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("dart"))
                    {
                        //Disable back touch Collider from dart
                        raycastHit.collider.enabled = false;
                        DartTemp.transform.parent = player;

                        //Dart currentDartScript = DartTemp.GetComponent<Dart>();
                        //currentDartScript.isForceOK = true;
                    }
                }

                if (Input.GetMouseButtonDown(0) && shootBool)
                // if (Input.touchCount > 0 && shootBool == true)
                {
                    Instantiate(sk.Renderer, firePoint.position, firePoint.rotation);
                }
                shootBool = true;
            }
        }
        // }   
    }
}
