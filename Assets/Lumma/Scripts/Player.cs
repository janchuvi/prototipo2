using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Windows.Kinect;
using LightBuzz.Vitruvius;

public class Player : MonoBehaviour
{
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

    public bool[] objetivos;
    public Collider[] coliders;
    public int velocidad;
    public GameObject cameras;
    Vector3 prevPos;
    Vector3 actualPos;
    bool rotar;
  public  GameObject[] turbine;
    bool reRun = false;
    bool sisi = false;
    PointsScript points;
	GameObject sceneObjectActive;
	CharacterControllerScript characterController;
	public SpeedScript speedScript;

    void Start()
    {
		Events.SceneObjectActive += SceneObjectActive;
    }

	public void Init(CharacterControllerScript characterController)
	{
		this.characterController = characterController;
		Rover.SetActive (false);
		PlayerScateboard.SetActive (false);

		if (characterController.planet == CharacterControllerScript.planets.MOON) {
			Rover.SetActive (true);
		} else {
			PlayerScateboard.SetActive (true);
		}
		objetivos = new bool[4];

		points = GameObject.Find("Canvas").GetComponent<PointsScript>();
		personaje = this.gameObject;

		infoText = GameObject.Find("InfoText").GetComponent<Text>();
		infoText.gameObject.SetActive (true);
		infoText.text = "";

		//    turbine = new GameObject[2];

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
        objetivos = null;
        coliders = null;
        cameras = null;
        turbine = null;
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
			personaje.transform.Translate (Vector3.forward * Time.deltaTime * -5);
			return;
		}
		//si se da vuelta:
		if (transform.position.y <0 || transform.up.y < 0) {
			ResetRotation ();
			return;
		}
		Vector3 pos_left = palm_left.transform.position;// +   (palm_left.transform.forward * -0.1f);
		Vector3 pos_right = palm_right.transform.position;// + (palm_right.transform.forward * -0.1f);

		palm_left_InHead.transform.position = pos_left;
		palm_right_InHead.transform.position = pos_right;
   

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StartScreen");
        }
        StartCoroutine(calcSpeed());

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
		

		Vector3 leftPos = palm_left_InHead.transform.localPosition;
		Vector3 rightPos = palm_right_InHead.transform.localPosition;

		Vector3 leftValues = new Vector3 ((int)(leftPos.x * 1000), (int)(leftPos.y * 1000), (int)(leftPos.z * 1000));
		Vector3 rightValues = new Vector3 ((int)(rightPos.x * 1000), (int)(rightPos.y * 1000), (int)(rightPos.z * 1000));
		float acceleration = 0;
		float rotation = 0;
		if (characterController.planet == CharacterControllerScript.planets.MOON) {
			float sum = leftValues.y;
			acceleration = sum - 300;
			float accelerationDiv = 20;

			//ajusta el tamaño de los brazos
			if (name == "Player2")
				accelerationDiv -= 12;
			
			acceleration /= accelerationDiv;
			rotation = rightValues.y - leftValues.y;
			rotation /= 2;
			//Events.OnLog (acceleration + "km/h"); 			
		} else {
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
		speedScript.UpdateSpeed((int)acceleration);
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
	void ResetParticles(GameObject go)
	{
		foreach (ParticleSystem ps in go.GetComponentsInChildren<ParticleSystem>())
			ps.Stop ();
	}
	string lastPicked = "";
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "MóduloLunarDelApolo11")
        {
			ResetParticles (other.gameObject);
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
			ResetParticles (other.gameObject);
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
			ResetParticles (other.gameObject);
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Has descubierto el crater Aristarco.\n Aristarco es un gran crater de impacto que se encuentra en la zona noroeste de la cara visible de la Luna. Es considerado la mas brillante de las grandes formaciones de la superficie lunar";
			other.GetComponent<Collider> ().enabled = false;
			// StartCoroutine(ReRun());
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (50);
			}
        }
		else if (other.name == "AristarcoWarning")
		{
			infoText.text = "Piloto automático";
			sceneObjectActive = other.gameObject;
			Invoke ("ResetSceneObject", 3);
		}
        else if (other.name == "RoverColl")
        {
			ResetParticles (other.gameObject);
			Events.SceneObjectActive (other.gameObject);
            infoText.text = "Has descubierto el Vehiculo Ambulante Lunar.\n El Lunar Roving Vehicle (tambien llamado LRV, Rover lunar o molabs) era un vehiculo todoterreno empleado por los astronautas de las misiones Apolo 15, 16 y 17 en sus desplazamientos por la superficie lunar.";
			other.GetComponent<Collider> ().enabled = false;
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
			//Events.SceneObjectActive (other.gameObject);
            infoText.text = "El polo sur de Enceladus es uno de los lugares mas extraños del Sistema Solar.\n Las temperaturas cerca del polo son mucho mas calientes que cualquier otro lugar en la luna,\n la enorme region de grietas que se cruzan, apodadas Rayas de Tigre, y los geiseres arrogan centenares de cristales de hielo a kilometros en el cielo.";
			other.GetComponent<Collider> ().enabled = false;
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "Surco Alexandria")
        {
			//Events.SceneObjectActive (other.gameObject);
            infoText.text = "Surco Alexandria";
			other.GetComponent<Collider> ().enabled = false;
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "Surco Cairo")
        {
			//Events.SceneObjectActive (other.gameObject);
            infoText.text = "Surco Cairo";
			other.GetComponent<Collider> ().enabled = false;
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "Surco Baghdad")
        {
			//Events.SceneObjectActive (other.gameObject);
            infoText.text = "Surco Baghdad";
			other.GetComponent<Collider> ().enabled = false;
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "Surco Damascus")
        {
			//Events.SceneObjectActive (other.gameObject);
            infoText.text = "Surco Damascus";
			other.GetComponent<Collider> ().enabled = false;
			if (lastPicked != other.name) {
				lastPicked = other.name;
				Events.AddPoints (10);
			}
        }
        else if (other.name == "GameBox")
        {
			Events.OnTeleport ();
			infoText.text = "Transfiriendo a zona segura";
			Invoke ("ResetText", 2);
			GameBox gameBox = other.GetComponent<GameBox> ();
			Vector3 newPos = gameBox.teleportTo;
			newPos.y = transform.position.y+10;
			transform.position = newPos;
			transform.LookAt (new Vector3 (320,1,320));
        }
        else if (other.name == "Warning" )
        {
            infoText.text = "Saliendo de la zona segura, desviese o sera transferido";
			Invoke ("ResetText", 3);
        }
    }
	void ResetText()
	{
		infoText.text = "";
	}
    void MaxTurbines(float intensity)
    {
        turbine[0].GetComponent<LensFlare>().brightness = intensity;
   		turbine[1].GetComponent<LensFlare>().brightness = intensity;
    }
}
