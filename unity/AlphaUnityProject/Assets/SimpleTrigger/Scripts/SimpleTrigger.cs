using UnityEngine;
using System.Collections;

public class SimpleTrigger : MonoBehaviour {

	public enum Actions
	{
		RigidbodyFall,
		RigidbodyStop,
		RigidbodyAddForce,
		SpawnGameObject,
		DestroyGameObject,
		EnableGameObject,
		DisableGameObject,
		MoveGameObject,
		RotateGameObject,
		ScaleGameObject,
		EnableComponent,
		DisableComponent,
		ParticleBegin,
		ParticleStop,
		PlayAnimation,
		StopAnimation,
		PlayAudioClip,
		StopAudioClip,
		IncreaseVolume,
		DecreaseVolume,
		ChangeVolume,
		IncreaseQualityLevel,
		DecreaseQualityLevel,
		ChangeQualityLevel,
		LoadLevel,
		LoadLevelAsync,
		LightIntensityIncrease,
		LightIntensityDecrease,
		LightIntensityChange,
		DebugToConsole,
		ChangeCameraFOV,
		ChangeTimeScale,
		TextChange,
		TextColorChange,
		ColorChange
	}

	public enum ComponentType
	{
		Animation,
		BoxCollider,
		BoxCollider2D,
		Camera,
		CapsuleCollider,
		CharacterController,
		CircleCollider2D,
		Cloth,
		Collider,
		EdgeCollider2D,
		LensFlare,
		Light,
		LineRenderer,
		MeshCollider,
		MeshFilter,
		MeshRenderer,
		NavMeshAgent,
		ParticleEmitter,
		PolygonCollider2D,
		Projector,
		SkinnedMeshRenderer,
		SphereCollider,
		TextMesh,
		TrailRenderer,
		WheelCollider
	}

	// Local access to the "Action" enum.
	public Actions action;

	// Action variables
	public ComponentType componentType;
	public int actionInteger = 0;
	public float actionFloat = 0.0f;
	public string actionString = "";
	public Vector3 actionVector3 = new Vector3(0.0f, 0.0f, 0.0f);
	public Color actionColor = Color.blue;
	public bool triggerOnlyOnce = false;

	// Affected GameObjects
	public GameObject affectedGameObject;
	public GameObject triggeringGameObject;

	public void OnDrawGizmos()
	{
		// NOTE: If you change the name of the Gizmo .PSD file, then replace the string here to your new file name.
		Gizmos.DrawIcon(transform.position, "SimpleTrigger_Gizmo.psd");
	}

	public void OnTriggerEnter(Collider other)
	{
		#region ActionDecision
		
		// Action
		//
		// How they work:
		// First, it checks to see if it's particular action was selected inside of the inspector.
		// Then, if it is, it checks to see if the "Specified GameObject" was the object that went through the trigger.
		// If it wasn't the "Specified GameObject", then it will check to see if the "Specified GameObject" is null, or empty.
		// If it is empty, then it will perform the action anyways.  However, if the "Specified GameObject" is detected, then it will also perform the action.
		
		switch (action)
		{
			// Rigidbody Fall action: Triggers a rigidbody's Kinematic property "true" to make it fall.
			// NOTE: This action will only work if you first set the Triggered Object's rigidbody as Kinematic in the Inspector first.
		case  Actions.RigidbodyFall:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<Rigidbody>().isKinematic = false;
			}
			break;
			// Rigidbody Stop action: Triggers a rigidbody's Kinematic property "false" to make it stop.
			// NOTE: This action will only work if your Triggered Object's rigidbody isn't set as Kinematic.
		case Actions.RigidbodyStop:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<Rigidbody>().isKinematic = true;
			}
			break;
			// Rigidbody AddForce action: Adds force to the Triggered Object's rigidbody, causing it to move in a particular direction, according to "Special Actions Vector".
		case Actions.RigidbodyAddForce:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<Rigidbody>().AddForce(actionVector3);
			}
			break;
			// Spawn GameObject action: Instantiates, or "spawns", a GameObject, specified in the "Affected GameObject" variable, 
			// from your Project into the scene corresponding to the "Action Vector" variable.
		case Actions.SpawnGameObject:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				Instantiate(affectedGameObject);
				affectedGameObject.transform.position = actionVector3;
			}
			break;
			// Destroy GameObject action: Destroys the Triggered Object currently in the scene.
		case Actions.DestroyGameObject:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				Destroy(affectedGameObject);
			}
			break;
			// Move GameObject action: Moves the Triggered Object to the specified in "Special Action Vector".
		case Actions.EnableGameObject:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.SetActive(true);
			}
			break;
			// Disable GameObject action: Disables the Triggered Object currently in the scene.
		case Actions.DisableGameObject:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.SetActive(false);
			}
			break;
			// Move GameObject action: Moves the Triggered Object to the specified in "Special Action Vector".
		case Actions.MoveGameObject:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.transform.position = actionVector3;
			}
			break;
			// Rotate GameObject action: Rotates the Triggered Object according to the specified in "Special Action Vector".
		case Actions.RotateGameObject:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.transform.rotation = affectedGameObject.transform.rotation * Quaternion.Euler(actionVector3);
			}
			break;
			// Scale GameObject action: Scales the Triggered Object according to the specified value in "Special Action Vector".
		case Actions.ScaleGameObject:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.transform.localScale = actionVector3;
			}
			break;
			// Enable Component action: Enables the Triggered Object's specailized component declared inside of the "Special Action String" parameter.
			// NOTE: This action uses obselete properties that may need to be replaced in later version of Unity.  You may change these to your likings.
			//       If, however, these obselete properties ARE deleted, then this plugin should immediatly be updated upon such action.
			//       If we don't update this plugin upon these circumstances, then please contact us using the Contact page in the documentation.
		case Actions.EnableComponent:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				switch (componentType)
				{
				case ComponentType.Animation:
					affectedGameObject.GetComponent<Animation>().gameObject.SetActive(true);
					break;
				case ComponentType.BoxCollider:
					affectedGameObject.GetComponent<BoxCollider>().gameObject.SetActive(true);
					break;
				case ComponentType.BoxCollider2D:
					affectedGameObject.GetComponent<BoxCollider2D>().gameObject.SetActive(true);
					break;
				case ComponentType.Camera:
					affectedGameObject.GetComponent<Camera>().gameObject.SetActive(true);
					break;
				case ComponentType.CapsuleCollider:
					affectedGameObject.GetComponent<CapsuleCollider>().gameObject.SetActive(true);
					break;
				case ComponentType.CharacterController:
					affectedGameObject.GetComponent<CharacterController>().gameObject.SetActive(true);
					break;
				case ComponentType.CircleCollider2D:
					affectedGameObject.GetComponent<CircleCollider2D>().gameObject.SetActive(true);
					break;
				case ComponentType.Cloth:
					affectedGameObject.GetComponent<Cloth>().gameObject.SetActive(true);
					break;
				case ComponentType.Collider:
					affectedGameObject.GetComponent<Collider>().gameObject.SetActive(true);
					break;
				case ComponentType.EdgeCollider2D:
					affectedGameObject.GetComponent<EdgeCollider2D>().gameObject.SetActive(true);
					break;
				case ComponentType.LensFlare:
					affectedGameObject.GetComponent<LensFlare>().gameObject.SetActive(true);
					break;
				case ComponentType.Light:
					affectedGameObject.GetComponent<Light>().gameObject.SetActive(true);
					break;
				case ComponentType.LineRenderer:
					affectedGameObject.GetComponent<LineRenderer>().gameObject.SetActive(true);
					break;
				case ComponentType.MeshCollider:
					affectedGameObject.GetComponent<MeshCollider>().gameObject.SetActive(true);
					break;
				case ComponentType.MeshFilter:
					affectedGameObject.GetComponent<MeshFilter>().gameObject.SetActive(true);
					break;
				case ComponentType.MeshRenderer:
					affectedGameObject.GetComponent<MeshRenderer>().gameObject.SetActive(true);
					break;
				case ComponentType.NavMeshAgent:
					affectedGameObject.GetComponent<NavMeshAgent>().gameObject.SetActive(true);
					break;
				case ComponentType.ParticleEmitter:
					affectedGameObject.GetComponent<ParticleEmitter>().gameObject.SetActive(true);
					break;
				case ComponentType.PolygonCollider2D:
					affectedGameObject.GetComponent<PolygonCollider2D>().gameObject.SetActive(true);
					break;
				case ComponentType.Projector:
					affectedGameObject.GetComponent<Projector>().gameObject.SetActive(true);
					break;
				case ComponentType.SkinnedMeshRenderer:
					affectedGameObject.GetComponent<SkinnedMeshRenderer>().gameObject.SetActive(true);
					break;
				case ComponentType.SphereCollider:
					affectedGameObject.GetComponent<SphereCollider>().gameObject.SetActive(true);
					break;
				case ComponentType.TextMesh:
					affectedGameObject.GetComponent<TextMesh>().gameObject.SetActive(true);
					break;
				case ComponentType.TrailRenderer:
					affectedGameObject.GetComponent<TrailRenderer>().gameObject.SetActive(true);
					break;
				case ComponentType.WheelCollider:
					affectedGameObject.GetComponent<WheelCollider>().gameObject.SetActive(true);
					break;
				}
			}
			break;
			// Disable Component action: Disables the Triggered Object's specailized component declared inside of the "Special Actions String" parameter.
			// NOTE: This action uses obselete properties that may need to be replaced in later version of Unity.  You may change these to your likings.
			//       If, however, these obselete properties ARE deleted, then this plugin should immediatly be updated upon such action.
			//       If we don't update this plugin upon these circumstances, then please contact us using the Contact page in the documentation.
		case Actions.DisableComponent:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				switch (componentType)
				{
				case ComponentType.Animation:
					affectedGameObject.GetComponent<Animation>().gameObject.SetActive(false);
					break;
				case ComponentType.BoxCollider:
					affectedGameObject.GetComponent<BoxCollider>().gameObject.SetActive(false);
					break;
				case ComponentType.BoxCollider2D:
					affectedGameObject.GetComponent<BoxCollider2D>().gameObject.SetActive(false);
					break;
				case ComponentType.Camera:
					affectedGameObject.GetComponent<Camera>().gameObject.SetActive(false);
					break;
				case ComponentType.CapsuleCollider:
					affectedGameObject.GetComponent<CapsuleCollider>().gameObject.SetActive(false);
					break;
				case ComponentType.CharacterController:
					affectedGameObject.GetComponent<CharacterController>().gameObject.SetActive(false);
					break;
				case ComponentType.CircleCollider2D:
					affectedGameObject.GetComponent<CircleCollider2D>().gameObject.SetActive(false);
					break;
				case ComponentType.Cloth:
					affectedGameObject.GetComponent<Cloth>().gameObject.SetActive(false);
					break;
				case ComponentType.Collider:
					affectedGameObject.GetComponent<Collider>().gameObject.SetActive(false);
					break;
				case ComponentType.EdgeCollider2D:
					affectedGameObject.GetComponent<EdgeCollider2D>().gameObject.SetActive(false);
					break;
				case ComponentType.LensFlare:
					affectedGameObject.GetComponent<LensFlare>().gameObject.SetActive(false);
					break;
				case ComponentType.Light:
					affectedGameObject.GetComponent<Light>().gameObject.SetActive(false);
					break;
				case ComponentType.LineRenderer:
					affectedGameObject.GetComponent<LineRenderer>().gameObject.SetActive(false);
					break;
				case ComponentType.MeshCollider:
					affectedGameObject.GetComponent<MeshCollider>().gameObject.SetActive(false);
					break;
				case ComponentType.MeshFilter:
					affectedGameObject.GetComponent<MeshFilter>().gameObject.SetActive(false);
					break;
				case ComponentType.MeshRenderer:
					affectedGameObject.GetComponent<MeshRenderer>().gameObject.SetActive(false);
					break;
				case ComponentType.NavMeshAgent:
					affectedGameObject.GetComponent<NavMeshAgent>().gameObject.SetActive(false);
					break;
				case ComponentType.ParticleEmitter:
					affectedGameObject.GetComponent<ParticleEmitter>().gameObject.SetActive(false);
					break;
				case ComponentType.PolygonCollider2D:
					affectedGameObject.GetComponent<PolygonCollider2D>().gameObject.SetActive(false);
					break;
				case ComponentType.Projector:
					affectedGameObject.GetComponent<Projector>().gameObject.SetActive(false);
					break;
				case ComponentType.SkinnedMeshRenderer:
					affectedGameObject.GetComponent<SkinnedMeshRenderer>().gameObject.SetActive(false);
					break;
				case ComponentType.SphereCollider:
					affectedGameObject.GetComponent<SphereCollider>().gameObject.SetActive(false);
					break;
				case ComponentType.TextMesh:
					affectedGameObject.GetComponent<TextMesh>().gameObject.SetActive(false);
					break;
				case ComponentType.TrailRenderer:
					affectedGameObject.GetComponent<TrailRenderer>().gameObject.SetActive(false);
					break;
				case ComponentType.WheelCollider:
					affectedGameObject.GetComponent<WheelCollider>().gameObject.SetActive(false);
					break;
				}
			}
			break;
			// Particle Begin action: Starts a particle emitter.
			// NOTE: This action will only work if: 1) Your Trigger Object has a particle emitter attatched to it.
			//                                      2) "Emit" is set to false on your particle emitter.
		case Actions.ParticleBegin:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<ParticleEmitter>().emit = true;
			}
			break;
			// Particle Stop action: Stops a particle emitter.
			// NOTE: This action will only work if: 1) Your Trigger Object has a particle emitter attatched to it.
			//                                      2) "Emit" is set to true on your particle emitter.
		case Actions.ParticleStop:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<ParticleEmitter>().emit = false;
			}
			break;
			// Play Animation action: Plays the animation clip specified in the "Special Action" parameter in the Inspector.
			//                        If there is nothing specified in the "Special Action" parameter, then it will automatiacally
			//                        play the default AnimationClip for the Trigger Object.
			// NOTE: This action will only work if: 1) Your Trigger Object has an Animation component attatched.
			//                                      2) "Automatically Play" is set to false.
		case Actions.PlayAnimation:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				if (actionString != null)
				{
					GetComponent<Animation>().Play(actionString);
				}
				else
				{
					GetComponent<Animation>().Play();
				}
			}
			break;
			// Stop Animation action: Stops the animation clip specified in the "Special Action" parameter in the Inspector.
			//                        If there is nothing specified in the "Special Action" parameter, then it will automatiacally
			//                        stop the default AnimationClip for the Trigger Object.
			// NOTE: This action will only work if: 1) Your Trigger Object has an Animation component attatched.
			//                                      2) "Automatically Play" is set to false.
		case Actions.StopAnimation:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				if (actionString != null)
				{
					GetComponent<Animation>().Stop(actionString);
				}
				else
				{
					GetComponent<Animation>().Stop();
				}
			}
			break;
			// Play AudioClip action: Plays the default AudioClip attatched to the Trigger Object.
			// NOTE: This action will only work if: 1) Your Trigger Object has an AudioSource component attatched.
			//                                      2) "Play on Awake" is set to false.
		case Actions.PlayAudioClip:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<AudioSource>().Play();
			}
			break;
			// Stop AudioClip action: Stops the default AudioClip attatched to the Trigger Object.
			// NOTE: This action will only work if your Trigger Object has an AudioSource component attatched.
		case Actions.StopAudioClip:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<AudioSource>().Stop();
			}
			break;
			// Incease Volume action: Increases the volume dynamically by the amount specified in the "Special Action Float" parameter.
		case Actions.IncreaseVolume:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				AudioListener.volume += actionFloat;
			}
			break;
			// Decrease Volume action: Decreases the volume dynamically by the amount specified in the "Special Action Float" parameter.
		case Actions.DecreaseVolume:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				AudioListener.volume -= actionFloat;
			}
			break;
			// Change Volume action: Changes the volume to the amount specified in the "Special Action Float" parameter.
		case Actions.ChangeVolume:
			if (other.gameObject == triggeringGameObject)
			{
				AudioListener.volume = actionFloat;
			}
			break;
			// Increase Quality Level action: Increases the quality level.
		case Actions.IncreaseQualityLevel:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				QualitySettings.IncreaseLevel();
			}
			break;
			// Decrease Quality Level action: Decreases the quality level.
		case Actions.DecreaseQualityLevel:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				QualitySettings.DecreaseLevel();
			}
			break;
			// Set Quality Level action: Set the quality level.
		case Actions.ChangeQualityLevel:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				QualitySettings.SetQualityLevel(actionInteger);

				if(actionInteger < 0)
				{
					Debug.LogError("SimpleTrigger: The Action Quality Level variable is less than zero (invalid quality level).");
				}
			}
			break;
			// Load Level action: Loads the level NAME specified in the "Special Action String" parameter.
		case Actions.LoadLevel:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				Application.LoadLevel(actionInteger);
			}
			break;
			// Light Intensity Increase action: Increases the light's intensity, (attatched to the Trigger Object), dynamically based upon the "Special Action Float" parameter.
		case Actions.LightIntensityIncrease:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<Light>().intensity += actionFloat;
			}
			break;
			// Light Intensity Decrease action: Decreases the light's intensity, (attatched to the Trigger Object), dynamically based upon the "Special Action Float" parameter.
		case Actions.LightIntensityDecrease:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<Light>().intensity -= actionFloat;
			}
			break;
			// Light Intensity Change action: Changes the light's intensity, (attatched to the Trigger Object), based upon the "Special Action Float" parameter.
		case Actions.LightIntensityChange:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<Light>().intensity = actionFloat;
			}
			break;
			// Debug to Console action: Logs a console message displaying what the special action says.
		case Actions.DebugToConsole:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				Debug.Log(actionString);
			}
			break;
			// Change Time Scale action: Changes the Time Scale with the value specified in "Special Action Float".
		case Actions.ChangeTimeScale:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				Time.timeScale = actionFloat;
			}
			break;
			// Text Change action: Changes the text of a 3D Text GameObject.
		case Actions.TextChange:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<TextMesh>().text = actionString;
			}
			break;
			// Text Color Change action: Changes the color of a 3D Text GameObject.
		case Actions.TextColorChange:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<TextMesh>().color = actionColor;
			}
			break;
			// Color Change action: Changes the color of a GameObject.
		case Actions.ColorChange:
			if (other.gameObject == triggeringGameObject || triggeringGameObject == null)
			{
				affectedGameObject.GetComponent<Renderer>().material.color = actionColor;
			}
			break;
		}
		#endregion

		if (triggerOnlyOnce)
		{
			Destroy(this.gameObject.GetComponent<SimpleTrigger>());
		}
	}
}
