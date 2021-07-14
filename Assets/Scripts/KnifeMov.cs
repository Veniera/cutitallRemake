using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.UI;


public class KnifeMov : MonoBehaviour
{   //Knife hareketleri

    Rigidbody knifeRigid;
    public float power;
    public float torque ;
    public float maxSpeed;

    // Obje kesim elemanlarý

    public Transform KnifeTr;
    public Material mat;
    public LayerMask mask;

    //skor deðiskenleri

    public int count;
    public Text Score;
    public Text Multiplier;

    // Kesme sesi

    public AudioSource CutSound;
    public AudioClip Kesme1;
    public AudioClip KnifeJump;
    

    void Start()
    {
        CutSound = GetComponent<AudioSource>();
        knifeRigid = GetComponent<Rigidbody>();
        knifeRigid.AddForce(0, 3f, 0, ForceMode.Impulse);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            KnifeForce();
            CutSound.PlayOneShot(KnifeJump);
        }
      

        Collider[] wCutObj = Physics.OverlapBox(transform.position, new Vector3(0.66f, 0.005f, 0.46f), transform.rotation, mask);

        foreach (Collider nObject in wCutObj)
        {
            Destroy(nObject.gameObject);
            SlicedHull cuttenObj = Cut(nObject.GetComponent<Collider>().gameObject, mat);
            GameObject cutDown = cuttenObj.CreateUpperHull(nObject.gameObject, mat);
            GameObject cutUp = cuttenObj.CreateLowerHull(nObject.gameObject, mat);
            count++;
            print(count);

            KnifeTr.transform.localRotation = Quaternion.Euler(125, 0,-90);
          
            addComp(cutUp);
            addComp(cutDown);
            CutSound.PlayOneShot(Kesme1);
            
        }
        Score.text = " Score " +" = "+ count;
    }
    void KnifeForce()
    {
        knifeRigid.AddForce(0f, power, 1.95f, ForceMode.Impulse);
        knifeRigid.AddTorque(torque, 0f, 0f, ForceMode.Impulse);
        knifeRigid.velocity = Vector3.ClampMagnitude(knifeRigid.velocity, maxSpeed);
    }
    public SlicedHull Cut(GameObject obj,Material mat =null)
    {
        return obj.Slice(transform.position, transform.up, mat);
       
    }
    
    void addComp(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        obj.GetComponent<Rigidbody>().AddExplosionForce(100, obj.transform.position, 10000);
        obj.gameObject.tag = "Cutten";
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(GameObject.FindWithTag("Cutten"));

        if (collision.gameObject.tag == "1x")
        {
            Multiplier.gameObject.SetActive(true);
            Multiplier.text = "1x ! ";
            count *= 1;
            Score.text = "" + count;
            print(count + " is your score");
            Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "2x")
        {
            Multiplier.gameObject.SetActive(true);
            Multiplier.text = "2x !! ";
            count *= 2;
            Score.text = "" + count;
            print(count + " is your score");

            Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "4x")
        {
            Multiplier.gameObject.SetActive(true);
            Multiplier.text = "4x !!!! ";
            count *= 4;
            Score.text = "" + count;
            print(count + " is your score");
            Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "6x")
        {
            Multiplier.gameObject.SetActive(true);
            Multiplier.text = "6x !!!!!!!!";
            count *= 6;
            Score.text = "" + count;
            print(count + " is your score");
            Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "8x")
        {
            Multiplier.gameObject.SetActive(true);
            Multiplier.text = "8x !!!!!!!!";
            count *= 6;
            Score.text = "" + count;
            print(count + " is your score");
            Time.timeScale = 0;
        }
    }

}
