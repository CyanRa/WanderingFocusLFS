using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// FROM PUBLIC SOURCE

public class RadialSlider: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

	public GameplayController myGameplayController;

	public Text text;
	bool isPointerDown=false;

	// Called when the pointer enters our GUI component.
	// Start tracking the mouse
	public void OnPointerEnter( PointerEventData eventData )
	{
		StartCoroutine( "TrackPointer" );            
	}
	
	// Called when the pointer exits our GUI component.
	// Stop tracking the mouse
	public void OnPointerExit( PointerEventData eventData )
	{
		StopCoroutine( "TrackPointer" );
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isPointerDown= true;
		//Debug.Log("mousedown");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPointerDown= false;
		//Debug.Log("mousedown");
	}

	// mainloop
	IEnumerator TrackPointer()
	{
		var ray = GetComponentInParent<GraphicRaycaster>();
		var input = FindObjectOfType<StandaloneInputModule>();

		
		
		if( ray != null && input != null )
		{
			while( Application.isPlaying )
			{                    

				// TODO: if mousebutton down
				if (isPointerDown)
				{

					Vector2 localPos; // Mouse position  
					RectTransformUtility.ScreenPointToLocalPointInRectangle( transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos );
						
					// local pos is the mouse position.
					float angle = (Mathf.Atan2(-localPos.y, localPos.x)*180f/Mathf.PI+180f)/360f;
					if (text.text == "01:00:00") { GetComponent<Image>().fillAmount = 1f; } /*else if(text.text == "00:15") { GetComponent<Image>().fillAmount = 0.25f; }*/ else { GetComponent<Image>().fillAmount = angle; }
					

					//GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, angle);

					text.text = myGameplayController.translateTimer(((int)(angle * 360f)).ToString()).ToString() ;

					//Debug.Log(localPos+" : "+angle);	
				}

				yield return 0;
			}        
		}
		else
			UnityEngine.Debug.LogWarning( "Could not find GraphicRaycaster and/or StandaloneInputModule" );        
	}





}
