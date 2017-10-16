using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Windows.Kinect;
using LightBuzz.Vitruvius;

public class Player : MonoBehaviour
{
	//pontura:
	public GameObject PlayerScateboard;
	public GameObject Rover;
	public GameObject palm_left_InHead;
	public GameObject palm_right_InHead;

	public Transform palm_right;
	public Transform palm_left;
	public Transform head;

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
	GameObject sceneObjectActive;
    // Use this for initialization
    void Start()
    {
		Events.SceneObjectActive += SceneObjectActive;
        rover = false;
        board = false;
        objetivos = new bool[4];
        veiculos = new GameObject[2];
        points = GameObject.Find("Canvas").GetComponent<PointsScript>();
        personaje = this.gameObject;

        infoText = GameObject.Find("InfoText").GetComponent<Text>();
		infoText.gameObject.SetActive (true);
		infoText.text = "";

        turbine = new GameObject[2];
     
        for (int i = 0; i < objetivos.Length; i++)
        {
          //  objetivos[i] = false;
        }
      
        carteles.SetActive(false);
    }
	void SceneObjectActive(GameObject go)
	{
		if (go == null)
			return;
		sceneObjectActive = go;
		Invoke ("ResetSceneObject", 6);
	}
	void ResetSceneObject()
	{
		infoText.text = "";

		Events.SceneObjectActive (null);
		sceneObjectActive = null;
	}
    private void OnDestroy()
    {
		Events.SceneObjectActive -= SceneObjectActive;
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
   
	void ResetRotation()
	{
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		print ("ResetRotation");
		GetComponent<Rigidbody> ().isKinematic = true;
		transform.localEulerAngles = Vector3.zero;
		Vector3 pos = transform.position;
		pos.y += 3;
		transform.position = pos;
		Invoke ("ResetDone", 0.1f);
	}
	void ResetDone()
	{
		GetComponent<Rigidbody> ().isKinematic = false;
	}
    void Update()
    {
		if (sceneObjectActive != null) {
			//reverse
			personaje.transform.Translate (Vector3.forward * Time.deltaTime * -4);
			return;
		}
		//si se da vuelta:
		if (transform.position.y <0 || transform.up.y < 0) {
			ResetRotation ();
			return;
		}
		palm_left_InHead.transform.position = palm_left.transform.position;
		palm_right_InHead.transform.position = palm_right.transform.position;
   

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StartScreen");
        }
        StartCoroutine(calcSpeed());
    /*    if (rotar == true)
        {
            Invoke("RotateCamera", 3);
        }
        else
        {
            RotateCameraBack();
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
		

		Vector3 leftPos =palm_left_InHead.transform.localPosition;
		Vector3 rightPos =palm_right_InHead.transform.localPosition;

		Vector3 leftValues = new Vector3 ((int)(leftPos.x * 1000), (int)(leftPos.y * 1000), (int)(leftPos.z * 1000));
		Vector3 rightValues = new Vector3 ((int)(rightPos.x * 1000), (int)(rightPos.y * 1000), (int)(rightPos.z * 1000));
		float acceleration = 0;
		float rotation = 0;
		if (Rover != null) {
			rover = true;

			float sum = leftValues.y;
			acceleration = sum - 300;
			acceleration /= 20;
			rotation = rightValues.y - leftValues.y;
			rotation /= 2;
			//Events.OnLog (acceleration + "km/h"); 

			
		} else {
			board = true;

			float sum = (Mathf.Abs (leftValues.x - 100) + Mathf.Abs (rightValues.x - 100)) / 2;
			acceleration = 450 - sum;
			rotation = leftValues.y - rightValues.y;
			acceleration /= 50;
			rotation /= 10;
			//Events.OnLog (acceleration + "km/h"); 

			personaje.transform.Rotate (Vector3.up * Time.deltaTime * (rotation / 10));
			personaje.transform.Translate (Vector3.forward * Time.deltaTime * (acceleration / 50));

			if (acceleration > 0)
				MaxTurbines (acceleration / 500);
			else
				MaxTurbines (0);
			
		}
		if (acceleration < 0)
			acceleration = 0;
		
		if (acceleration > 0) {
			personaje.transform.Rotate (Vector3.up * Time.deltaTime * rotation);
			personaje.transform.Translate (Vector3.forward * Time.deltaTime * acceleration);
		}
          
      //      Debug.Log(leftArm.localRotation.y + "left: rigit" + rightArm.localRotation.y);
   
         //   if (gameObject.name == "Player3" || gameObject.name == "Player1")
           // {
//             
//                {
//                    personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
//                    MaxTurbines(0.55f);
//                }
//                if (rightArm.localRotation.y > -0.3f && leftArm.localRotation.y > 0.2f)
//                {
//
//                    personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
//                    personaje.transform.Rotate(Vector3.up * Time.deltaTime * -50);
//                    //Debug.Log("izquierda");
//                    MaxTurbines(0.65f);
//                }
//                if (rightArm.localRotation.y < -0.3f && leftArm.localRotation.y < 0.3f)
//                {
//                    personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
//                    personaje.transform.Rotate(Vector3.up * Time.deltaTime * 50);
//                  //  Debug.Log("derecha");
//                    MaxTurbines(0.65f);
//                }
//        //    }

//      
//        else
//        {
//            rover = true;
//             Debug.Log(leftArm.localRotation.y + "left: rigit" + rightArm.localRotation.y);
//         
//           
//                bool izquierda= false;
//                bool derecha = false;
//           //    Debug.Log(rightArm.localRotation.y + "right : left  " + leftArm.localRotation.y);
//
//                if (rightArm.localRotation.z < -0.35f && leftArm.localRotation.z < -0.35f)
//                {
//                    personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
//                  //  float AngleAmount = (Mathf.Cos(Time.time * 5) * 180) / Mathf.PI * 0.5f;
//                 //   Debug.Log("Rotation " + AngleAmount);
//                    //AngleAmount = Mathf.Clamp(AngleAmount, -5, 5);
//                    //veiculos[0].transform.parent.transform.localRotation = Quaternion.Euler(0, -180, AngleAmount * Time.deltaTime *5);
//                }
//             
//                if (rightArm.localRotation.y >0.1 && izquierda == false)
//                {
//                       personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
//                       personaje.transform.Rotate(Vector3.up * Time.deltaTime * -50);
//                    derecha = true;
//                    
//                   }
//             
//                if ( leftArm.localRotation.y < -0.1f && derecha == false)
//                {
//                       personaje.transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
//                       personaje.transform.Rotate(Vector3.up * Time.deltaTime * 50);
//                    izquierda = true;
//                   }
//             
//            }
        
    }
    
	string lastPicked = "";
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "MóduloLunarDelApolo11")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Has descubierto el Modulo Lunar del Apollo 11.\n El modulo lunar era un vehiculo espacial de dos etapas diseñado para el alunizaje durante el programa Apolo.";
           
            objetivos[0] = true;
            coliders[0].enabled = false;
           // StartCoroutine(ReRun());
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (50);
			}
        }
        else if (other.name == "EMU")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Has descubierto al astronauta perdido";
           
            objetivos[1] = true;
            coliders[2].enabled = false;
			// StartCoroutine(ReRun());
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (50);
			}
        }
        else if (other.name == "Aristarco")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Has descubierto el crater Aristarco.\n Aristarco es un gran crater de impacto que se encuentra en la zona noroeste de la cara visible de la Luna. Es considerado la mas brillante de las grandes formaciones de la superficie lunar";
           
            objetivos[2] = true;
           // coliders[1].enabled = false;
			// StartCoroutine(ReRun());
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (50);
			}
        }
        else if (other.name == "RoverColl")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Has descubierto el Vehiculo Ambulante Lunar.\n El Lunar Roving Vehicle (tambien llamado LRV, Rover lunar o molabs) era un vehiculo todoterreno empleado por los astronautas de las misiones Apolo 15, 16 y 17 en sus desplazamientos por la superficie lunar.";
            
            objetivos[3] = true;
            coliders[3].enabled = false;
			// StartCoroutine(ReRun());
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (50);
			}
        }
        else if (other.name == "WhiteSmoke")
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 10, ForceMode.Impulse);
            Debug.Log("salto");
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "SouthPole")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "El polo sur de Enceladus es uno de los lugares mas extraños del Sistema Solar.\n Las temperaturas cerca del polo son mucho mas calientes que cualquier otro lugar en la luna,\n la enorme region de grietas que se cruzan, apodadas Rayas de Tigre, y los geiseres arrogan centenares de cristales de hielo a kilometros en el cielo.";
           
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "Surco Alexandria")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Surco Alexandria";
           
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "Surco Cairo")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Surco Cairo";
            
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "Surco Baghdad")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Surco Baghdad";
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "Surco Damascus")
        {
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Surco Damascus";
           
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "GameBox" && rover == true)
        {
           // this.transform.position = spawnPoints[Random.Range(0, 4)].transform.position;
        }
        else if (other.name == "GameBox" && board == true)
        {
           // this.transform.position = spawnPoints[Random.Range(4, 8)].transform.position;
        }
        else if (other.name == "Warning" )
        {
          //  carteles.SetActive(true);
           // infoText.text = "Saliendo de la zona segura, desviese o sera transferido";
        }
    }

    void MaxTurbines(float intensity)
    {
        turbine[0].GetComponent<LensFlare>().brightness = intensity;
   		turbine[1].GetComponent<LensFlare>().brightness = intensity;
    }
}
