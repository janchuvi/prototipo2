using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Windows.Kinect;
using LightBuzz.Vitruvius;

public class Player : MonoBehaviour
{
   public GameObject carteles;
    public Transform brazoForward;
    public Transform rightArm;
    public Transform leftArm;
    public GameObject personaje;
    public Text infoText;
    public GameObject[] veiculos;
    public Transform[] spawnPoints;
    public bool[] objetivos;
    public Collider[] coliders;
    public int velocidad;
    public GameObject cameras;
    Vector3 prevPos;
    Vector3 actualPos;
    bool rotar;
  public  GameObject[] turbine;
    bool rover;
    bool board;
    bool reRun = false;
    bool sisi = false;
    PointsScript points;
    // Use this for initialization
    void Start()
    {
        //  cameras.transform.LookAt(this.gameObject.transform);
        rover = false;
        board = false;
        objetivos = new bool[4];
        veiculos = new GameObject[2];
        veiculos[0] = GameObject.Find("GameObjectRover");
        veiculos[1] = GameObject.Find("PlayerScateboard");
        points = GameObject.Find("Canvas").GetComponent<PointsScript>();
        personaje = this.gameObject;
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        turbine = new GameObject[2];
     
        for (int i = 0; i < objetivos.Length; i++)
        {
          //  objetivos[i] = false;
        }
      
        carteles.SetActive(false);
    }
    private void OnDestroy()
    {
        brazoForward = null;
        rightArm = null;
        leftArm = null;
        personaje = null;
        veiculos = null;
        spawnPoints = null;
        objetivos = null;
        coliders = null;
        cameras = null;
        turbine = null;
    }
    void RotateCamera()
    {
       
        if (rotar == true)
        {
         
            Debug.Log(sisi);
            Transform target = this.gameObject.transform;
            if (cameras.transform.localPosition.z < 3f )
                cameras.transform.localPosition += new Vector3(0, 0, 1) * Time.deltaTime * 3;
        if (cameras.transform.localPosition.y < 3.2f&& sisi == false)
            {
                cameras.transform.localPosition += new Vector3(0, 1, 0) * Time.deltaTime * 1;
                if (cameras.transform.localPosition.y > 3.1f)
                {
                    sisi = true;
                }
            }else if (cameras.transform.localPosition.y > 2.15f && sisi == true)
            {
                cameras.transform.localPosition += new Vector3(0, -1, 0) * Time.deltaTime * 1;
            
            }
            if (cameras.transform.localEulerAngles.y < 180f)
                cameras.transform.localEulerAngles += new Vector3(0, 1, 0) * Time.deltaTime * 30;
        }
    }
    void RotateCameraBack()
    {
        if (rotar == false)
        {
            Transform target = this.gameObject.transform;
            //   cameras.transform.LookAt(target);
            if (this.name == "Player2")
            {
                cameras.transform.localPosition = new Vector3(-3.66f, 1.56f, -4.81f);
                sisi = false;
            }
            else
            {
                cameras.transform.localPosition = new Vector3(-0.11f, 2.15f, -3.792999f); sisi = false;
            }
            //   if (cameras.transform.localEulerAngles.y > 1f)
            cameras.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    IEnumerator calcSpeed()
    {
        DataController data = GameObject.Find("DataController").GetComponent<DataController>();
        prevPos = personaje.transform.position;
        yield return new WaitForSeconds(0.1f);
        actualPos = personaje.transform.position;

        if (prevPos == actualPos)
        {
            //Debug.Log("character stoped");
            if (data.planetSelected.name == "Terrain3" && turbine != null)
            {
                MaxTurbines(0.3f);
            }
            rotar = true;
        }
        if (prevPos != actualPos)
        {
            //Debug.Log("character on the move");
            rotar = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
    if(reRun == true)
        {
            personaje.transform.Translate(Vector3.back * Time.deltaTime * 10);
            personaje.transform.Rotate(Vector3.up * Time.deltaTime * -50);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StartScreen");
        }
        StartCoroutine(calcSpeed());
        if (rotar == true)
        {
            Invoke("RotateCamera", 3);
        }
        else
        {
            RotateCameraBack();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        //   Debug.Log(leftArm.localRotation.eulerAngles.z);
        veiculos[1] = GameObject.Find("PlayerScateboard");
        veiculos[0] = GameObject.Find("GameObjectRover");

        if (veiculos[1] != null)
        {
            board = true;
          
      //      Debug.Log(leftArm.localRotation.y + "left: rigit" + rightArm.localRotation.y);
   
         //   if (gameObject.name == "Player3" || gameObject.name == "Player1")
           // {
                if (rightArm.localRotation.y > -0.2f && leftArm.localRotation.y < 0.2f)
                {
                    personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
                    MaxTurbines(0.55f);
                }
                if (rightArm.localRotation.y > -0.3f && leftArm.localRotation.y > 0.2f)
                {

                    personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
                    personaje.transform.Rotate(Vector3.up * Time.deltaTime * -50);
                    //Debug.Log("izquierda");
                    MaxTurbines(0.65f);
                }
                if (rightArm.localRotation.y < -0.3f && leftArm.localRotation.y < 0.3f)
                {
                    personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
                    personaje.transform.Rotate(Vector3.up * Time.deltaTime * 50);
                  //  Debug.Log("derecha");
                    MaxTurbines(0.65f);
                }
        //    }

        }
        else
        {
            rover = true;
             Debug.Log(leftArm.localRotation.y + "left: rigit" + rightArm.localRotation.y);
         
           
                bool izquierda= false;
                bool derecha = false;
           //    Debug.Log(rightArm.localRotation.y + "right : left  " + leftArm.localRotation.y);

                if (rightArm.localRotation.z < -0.35f && leftArm.localRotation.z < -0.35f)
                {
                    personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
                  //  float AngleAmount = (Mathf.Cos(Time.time * 5) * 180) / Mathf.PI * 0.5f;
                 //   Debug.Log("Rotation " + AngleAmount);
                    //AngleAmount = Mathf.Clamp(AngleAmount, -5, 5);
                    //veiculos[0].transform.parent.transform.localRotation = Quaternion.Euler(0, -180, AngleAmount * Time.deltaTime *5);
                }
             
                if (rightArm.localRotation.y >0.1 && izquierda == false)
                {
                       personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
                       personaje.transform.Rotate(Vector3.up * Time.deltaTime * -50);
                    derecha = true;
                    
                   }
             
                if ( leftArm.localRotation.y < -0.1f && derecha == false)
                {
                       personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
                       personaje.transform.Rotate(Vector3.up * Time.deltaTime * 50);
                    izquierda = true;
                   }
             
            }
        
    }
    IEnumerator ReRun()
    {
        velocidad = 0;
        yield return new WaitForSeconds(3);
        reRun = true;
        yield return new WaitForSeconds(0.5f);
        velocidad = 13;
        reRun = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "MóduloLunarDelApolo11")
        {

            infoText.text = "Has descubierto el Modulo Lunar del Apollo 11.\n El modulo lunar era un vehiculo espacial de dos etapas diseñado para el alunizaje durante el programa Apolo.";
            infoText.gameObject.SetActive(true);
            objetivos[0] = true;
            coliders[0].enabled = false;
            StartCoroutine(ReRun());
            points.points += 50;
        }
        else if (other.name == "EMU")
        {
            infoText.text = "Has descubierto al astronauta perdido";
            infoText.gameObject.SetActive(true);
            objetivos[1] = true;
            coliders[2].enabled = false;
            StartCoroutine(ReRun());
            points.points += 50;
        }
        else if (other.name == "Aristarco")
        {
            infoText.text = "Has descubierto el crater Aristarco.\n Aristarco es un gran crater de impacto que se encuentra en la zona noroeste de la cara visible de la Luna. Es considerado la mas brillante de las grandes formaciones de la superficie lunar";
            infoText.gameObject.SetActive(true);
            objetivos[2] = true;
            coliders[1].enabled = false;
            StartCoroutine(ReRun());
            points.points += 50;
        }
        else if (other.name == "RoverColl")
        {
            infoText.text = "Has descubierto el Vehiculo Ambulante Lunar.\n El Lunar Roving Vehicle (tambien llamado LRV, Rover lunar o molabs) era un vehiculo todoterreno empleado por los astronautas de las misiones Apolo 15, 16 y 17 en sus desplazamientos por la superficie lunar.";
            infoText.gameObject.SetActive(true);
            objetivos[3] = true;
            coliders[3].enabled = false;
            StartCoroutine(ReRun());
            points.points += 50;
        }
        else if (other.name == "WhiteSmoke")
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 10, ForceMode.Impulse);
            Debug.Log("salto");
            points.points += 10;
        }
        else if (other.name == "SouthPole")
        {
            infoText.text = "El polo sur de Enceladus es uno de los lugares mas extraños del Sistema Solar.\n Las temperaturas cerca del polo son mucho mas calientes que cualquier otro lugar en la luna,\n la enorme region de grietas que se cruzan, apodadas Rayas de Tigre, y los geiseres arrogan centenares de cristales de hielo a kilometros en el cielo.";
            infoText.gameObject.SetActive(true);
        }
        else if (other.name == "Surco Alexandria")
        {
            infoText.text = "Surco Alexandria";
            infoText.gameObject.SetActive(true);
        }
        else if (other.name == "Surco Cairo")
        {
            infoText.text = "Surco Cairo";
            infoText.gameObject.SetActive(true);
        }
        else if (other.name == "Surco Baghdad")
        {
            infoText.text = "Surco Baghdad";
            infoText.gameObject.SetActive(true);
        }
        else if (other.name == "Surco Damascus")
        {
            infoText.text = "Surco Damascus";
            infoText.gameObject.SetActive(true);
        }
        else if (other.name == "GameBox" && rover == true)
        {
            this.transform.position = spawnPoints[Random.Range(0, 4)].transform.position;
        }
        else if (other.name == "GameBox" && board == true)
        {
            this.transform.position = spawnPoints[Random.Range(4, 8)].transform.position;
        }
        else if (other.name == "Warning" )
        {
            carteles.SetActive(true);
            infoText.text = "Saliendo de la zona segura, desviese o sera transferido";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "MóduloLunarDelApolo11" || other.name == "EMU" || other.name == "Aristarco" || other.name == "RoverColl")
        {
            infoText.text = "";
        }
        infoText.text = "";
      //  infoText.gameObject.SetActive(false);
        carteles.SetActive(false);
    }
    void MaxTurbines(float intensity)
    {
            turbine[0].GetComponent<LensFlare>().brightness = intensity;
        turbine[1].GetComponent<LensFlare>().brightness = intensity;
    }
}
