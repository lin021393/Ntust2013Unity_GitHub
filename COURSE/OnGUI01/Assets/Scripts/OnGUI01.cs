/*
@file OnGUI01.cs
@author NDark
@date 20130721 . file started.
@date 20130726 by NDark . add class member m_DrawButton.

*/
using UnityEngine;

public class OnGUI01 : MonoBehaviour 
{
	public Texture m_Texture1 = null ;
	public Texture m_SpriteTexture = null ;
	public GUIStyle m_Style1 = new GUIStyle() ;
	public GUISkin m_Skin1 = null ;
	public Rect m_TextureCoordinate = new Rect( 0 , 0 , 49.0f/2793.0f , 1.0f ) ;
	public int m_DisplayPage = 0 ;
	 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_DisplayPage )
		{
		case 0 :
			if( true == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 1 ;
			break ;
		case 1 :
			if( false == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 2 ;
			break ;			
		case 2 :
			if( true == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 3 ;			
			break ;
		case 3 :
			if( false == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 4 ;			
			break ;
		case 4 :
			if( true == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 5 ;	
			if( true == Input.GetKey( KeyCode.PageDown ) )
			{
				Rect tmp = m_TextureCoordinate ;
				tmp.x += (49.0f/2793.0f) ;
				if( tmp.x > 1.0f )
					tmp.x = 0.0f ;
				m_TextureCoordinate = tmp ;			
			}
			if( true == Input.GetKey( KeyCode.PageUp ) )
			{
				Rect tmp = m_TextureCoordinate ;
				tmp.x -= (49.0f/2793.0f) ;
				if( tmp.x < 0.0f )
					tmp.x = 1.0f - (49.0f/2793.0f) ;
				m_TextureCoordinate = tmp ;			
			}
			break ;
		case 5 :
			if( false == Input.GetKey( KeyCode.Space ) )
				m_DisplayPage = 0 ;
				
			break ;	
		}	
	}
	
	void OnGUI()
	{
		
		switch( m_DisplayPage )
		{
		case 0 :
			DrawGUIBox() ;
			DrawGUIButton() ;
			DrawGUIToggle() ;
			DrawGUILabel() ;
			DrawGUITextField() ;
			DrawGUITextArea() ;
			break ;
		case 2 :
			DrawGUIWindow() ;
			break ;	
		case 4 :
			DrawGUISlider() ;
			DrawGUIScrollView() ;
			DrawGUIGroup() ;
			DrawGUITexture() ;
			break ;				
			
		}		

	}
	
	
	private void DrawGUIBox()
	{
		{
			Rect box1Rect = new Rect( 0 , 0 , 100 , 50 ) ;
			GUI.Box( box1Rect , "Box1" ) ;
		}
		
		{
			Rect box2Rect = new Rect( 0 , 70 , 100 , 50 ) ;
			GUI.Box( box2Rect , "Box2" ) ;
		}

		{
			Rect box3Rect = new Rect( 0 , 140 , 100 , 50 ) ;
			GUI.Box( box3Rect , "" ) ;
		}

		{
			Rect box4Rect = new Rect( 0 , 210 , 100 , 50 ) ;
			GUI.Box( box4Rect , "box4box4box4box4" ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect box5Rect = new Rect( 0 , 280 , 100 , 50 ) ;
			GUI.Box( box5Rect , m_Texture1 ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect box6Rect = new Rect( 0 , 350 , 100 , 50 ) ;
			GUI.Box( box6Rect , new GUIContent ( m_Texture1 ) ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect box7Rect = new Rect( 0 , 420 , 100 , 50 ) ;
			GUI.Box( box7Rect , new GUIContent ( "box7" , m_Texture1 ) ) ;
		}
		
		{
			Rect box8Rect = new Rect( 0 , 490 , 100 , 50 ) ;
			GUI.Box( box8Rect , new GUIContent ( "box8" , "box8 tooltip" ) ) ;
			
			Rect tooltipRect = new Rect( Input.mousePosition.x , 
									     Screen.height - Input.mousePosition.y , 
										 100 , 50 ) ;
			GUI.Label( tooltipRect , GUI.tooltip ) ;
		}
		
		{
			Rect box9Rect = new Rect( 0 , 560 , 100 , 50 ) ;
			GUI.Box( box9Rect , "box9box9box9" , m_Style1 ) ;	
		}
		
		if( null != m_Texture1 )
		{
			Rect box10Rect = new Rect( 0 , 630 , 100 , 50 ) ;
			GUI.Box( box10Rect , m_Texture1 , m_Style1 ) ;	
		}
		
		
		GUI.skin = m_Skin1 ;
		{
			Rect box11Rect = new Rect( 0 , 700 , 100 , 25 ) ;
			GUI.Box( box11Rect , "box11" ) ;	
		}		
		GUI.skin = null ;
		{
			Rect box12Rect = new Rect( 0 , 725 , 100 , 25 ) ;
			GUI.Box( box12Rect , "box12" ) ;	
		}					
	}
	
	public bool m_DrawButton = true ;
	private void DrawGUIButton()
	{
		
		{
			Rect button1Rect = new Rect( 150, 0 , 100 , 50 ) ;
			if( true == GUI.Button( button1Rect , "Button1" ) )
			{
				m_DrawButton = !m_DrawButton ;
			}
		}
		
		if( true == m_DrawButton )
		{
			Rect button2Rect = new Rect( 150, 70 , 100 , 50 ) ;
			GUI.Button( button2Rect , "Button2" ) ;
		}

		{
			Rect button3Rect = new Rect( 150, 140 , 100 , 50 ) ;
			GUI.Button( button3Rect , "" ) ;
		}

		{
			Rect button4Rect = new Rect( 150, 210 , 100 , 50 ) ;
			GUI.Button( button4Rect , "button4button4" ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect button5Rect = new Rect( 150, 280 , 100 , 50 ) ;
			GUI.Button( button5Rect , m_Texture1 ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect button6Rect = new Rect( 150, 350 , 100 , 50 ) ;
			GUI.Button( button6Rect , new GUIContent ( m_Texture1 ) ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect button7Rect = new Rect( 150, 420 , 100 , 50 ) ;
			GUI.Button( button7Rect , new GUIContent ( "button7" , m_Texture1 ) ) ;
		}
		
		{
			Rect button8Rect = new Rect( 150, 490 , 100 , 50 ) ;
			GUI.Button( button8Rect , new GUIContent ( "button8" , "button8 tooltip" ) ) ;
			
			Rect tooltipRect = new Rect( Input.mousePosition.x , 
									     Screen.height - Input.mousePosition.y , 
										 100 , 50 ) ;
			GUI.Label( tooltipRect , GUI.tooltip ) ;
		}
		
		{
			Rect button9Rect = new Rect( 150, 560 , 100 , 50 ) ;
			GUI.Button( button9Rect , "box9box9box9" , m_Style1 ) ;	
		}
		
		if( null != m_Texture1 )
		{
			Rect button10Rect = new Rect( 150, 630 , 100 , 50 ) ;
			GUI.Button( button10Rect , m_Texture1 , m_Style1 ) ;	
		}
		
		
		GUI.skin = m_Skin1 ;
		{
			Rect button11Rect = new Rect( 150, 700 , 100 , 25 ) ;
			GUI.Button( button11Rect , "button11" ) ;	
		}		
		GUI.skin = null ;
		{
			Rect button12Rect = new Rect( 150, 725 , 100 , 25 ) ;
			GUI.Button( button12Rect , "button12" ) ;	
		}					
	}	
	
	private bool m_ToggleValue = false ;
	private void DrawGUIToggle()
	{
		{
			Rect toggle1Rect = new Rect( 300, 0 , 100 , 50 ) ;
			m_ToggleValue = 
				GUI.Toggle( toggle1Rect , m_ToggleValue , "toggle1" ) ;
		}
		
		{
			Rect toggle2Rect = new Rect( 300, 70 , 100 , 50 ) ;
			GUI.Toggle( toggle2Rect , m_ToggleValue , "toggle2" ) ;
		}

		{
			Rect toggle3Rect = new Rect( 300, 140 , 100 , 50 ) ;
			GUI.Toggle( toggle3Rect , m_ToggleValue, "" ) ;
		}

		{
			Rect toggle4Rect = new Rect( 300, 210 , 100 , 50 ) ;
			m_ToggleValue = GUI.Toggle( toggle4Rect , m_ToggleValue , "toggle4toggle4" ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect toggle5Rect = new Rect( 300, 280 , 100 , 50 ) ;
			GUI.Toggle( toggle5Rect , m_ToggleValue , m_Texture1 ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect toggle6Rect = new Rect( 300, 350 , 100 , 50 ) ;
			GUI.Toggle( toggle6Rect , m_ToggleValue , new GUIContent ( m_Texture1 ) ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect toggle7Rect = new Rect( 300, 420 , 100 , 50 ) ;
			GUI.Toggle( toggle7Rect , m_ToggleValue , new GUIContent ( "toggle7" , m_Texture1 ) ) ;
		}
		
		{
			Rect toggle8Rect = new Rect( 300, 490 , 100 , 50 ) ;
			GUI.Toggle( toggle8Rect , m_ToggleValue , new GUIContent ( "toggle8" , "toggle8 tooltip" ) ) ;
			
			Rect tooltipRect = new Rect( Input.mousePosition.x , 
									     Screen.height - Input.mousePosition.y , 
										 100 , 50 ) ;
			GUI.Label( tooltipRect , GUI.tooltip ) ;
		}
		
		{
			Rect toggle9Rect = new Rect( 300, 560 , 100 , 50 ) ;
			GUI.Toggle( toggle9Rect , m_ToggleValue , "toggle9toggle9toggle9" , m_Style1 ) ;	
		}
		
		if( null != m_Texture1 )
		{
			Rect toggle10Rect = new Rect( 300, 630 , 100 , 50 ) ;
			GUI.Toggle( toggle10Rect , m_ToggleValue , m_Texture1 , m_Style1 ) ;	
		}
		
		
		GUI.skin = m_Skin1 ;
		{
			Rect toggle11Rect = new Rect( 300, 700 , 100 , 25 ) ;
			GUI.Toggle( toggle11Rect , m_ToggleValue , "toggle11" ) ;	
		}		
		GUI.skin = null ;
		{
			Rect toggle12Rect = new Rect( 300, 725 , 100 , 25 ) ;
			GUI.Toggle( toggle12Rect , m_ToggleValue , "toggle12" ) ;	
		}				
	}
	
	private void DrawGUILabel()
	{
		{
			Rect label1Rect = new Rect( 450, 0 , 100 , 50 ) ;
			GUI.Label( label1Rect  , "label1" ) ;
		}
		
		{
			Rect label2Rect = new Rect( 450, 70 , 100 , 50 ) ;
			GUI.Label( label2Rect  , "label2" ) ;
		}

		{
			Rect label3Rect = new Rect( 450, 140 , 100 , 50 ) ;
			GUI.Label( label3Rect , "" ) ;
		}

		{
			Rect label4Rect = new Rect( 450, 210 , 100 , 50 ) ;
			GUI.Label( label4Rect  , "label4label4label4label4" ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect label5Rect = new Rect( 450, 280 , 100 , 50 ) ;
			GUI.Label( label5Rect  , m_Texture1 ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect label6Rect = new Rect( 450, 350 , 100 , 50 ) ;
			GUI.Label( label6Rect  , new GUIContent ( m_Texture1 ) ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect label7Rect = new Rect( 450, 420 , 100 , 50 ) ;
			GUI.Label( label7Rect  , new GUIContent ( "label7" , m_Texture1 ) ) ;
		}
		
		{
			Rect label8Rect = new Rect( 450, 490 , 100 , 50 ) ;
			GUI.Label( label8Rect  , new GUIContent ( "label8" , "label8 tooltip" ) ) ;
			
			Rect tooltipRect = new Rect( Input.mousePosition.x , 
									     Screen.height - Input.mousePosition.y , 
										 100 , 50 ) ;
			GUI.Label( tooltipRect , GUI.tooltip ) ;
		}
		
		{
			Rect label9Rect = new Rect( 450, 560 , 100 , 50 ) ;
			GUI.Label( label9Rect  , "label9label9label9" , m_Style1 ) ;	
		}
		
		if( null != m_Texture1 )
		{
			Rect label10Rect = new Rect( 450, 630 , 100 , 50 ) ;
			GUI.Label( label10Rect , m_Texture1 , m_Style1 ) ;	
		}
		
		
		GUI.skin = m_Skin1 ;
		{
			Rect label11Rect = new Rect( 450, 700 , 100 , 25 ) ;
			GUI.Label( label11Rect  , "label11" ) ;	
		}		
		GUI.skin = null ;
		{
			Rect label12Rect = new Rect( 450, 725 , 100 , 25 ) ;
			GUI.Label( label12Rect  , "label12" ) ;	
		}				
	}
	
	private string m_TextField = "textfield" ;
	private void DrawGUITextField()
	{
		{
			Rect textField1Rect = new Rect( 600, 0 , 100 , 50 ) ;
			m_TextField = GUI.TextField( textField1Rect  , m_TextField ) ;
		}
		
		{
			Rect textField2Rect = new Rect( 600, 70 , 100 , 50 ) ;
			GUI.TextField( textField2Rect  , m_TextField ) ;
		}

		{
			Rect textField3Rect = new Rect( 600, 140 , 100 , 50 ) ;
			GUI.TextField( textField3Rect , "" ) ;
		}

		{
			Rect textField4Rect = new Rect( 600, 210 , 100 , 40 ) ;
			GUI.TextField( textField4Rect  , 
				m_TextField + m_TextField + m_TextField ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect textField5Rect = new Rect( 600, 252 , 100 , 40 ) ;
			GUI.TextField( textField5Rect  , 
				m_TextField + m_TextField + m_TextField , 10 ) ;
		}
		
		if( null != m_Texture1 )
		{
			// Rect textField6Rect = new Rect( 600, 350 , 100 , 50 ) ;
			// GUI.TextField( textField6Rect  , new GUIContent ( m_Texture1 ) ) ;
		}
		
		if( null != m_Texture1 )
		{
			// Rect textField7Rect = new Rect( 600, 420 , 100 , 50 ) ;
			// GUI.TextField( textField7Rect  , new GUIContent ( "textField7" , m_Texture1 ) ) ;
		}
		
		{
//			Rect textField8Rect = new Rect( 600, 490 , 100 , 50 ) ;
//			GUI.TextField( textField8Rect  , new GUIContent ( "textField8" , "textField8 tooltip" ) ) ;
//			
//			Rect tooltipRect = new Rect( Input.mousePosition.x , 
//									     Screen.height - Input.mousePosition.y , 
//										 100 , 50 ) ;
//			GUI.Label( tooltipRect , GUI.tooltip ) ;
		}
		
		{
			Rect textField9Rect = new Rect( 600, 560 , 100 , 50 ) ;
			GUI.TextField( textField9Rect  , m_TextField + m_TextField + m_TextField , m_Style1 ) ;	
		}
		
		if( null != m_Texture1 )
		{
//			Rect textField10Rect = new Rect( 600, 630 , 100 , 50 ) ;
//			GUI.TextField( textField10Rect  , m_Texture1 , m_Style1 ) ;	
		}
		
		
		GUI.skin = m_Skin1 ;
		{
			Rect textField11Rect = new Rect( 600, 700 , 100 , 25 ) ;
			GUI.TextField( textField11Rect  , m_TextField  ) ;	
		}		
		GUI.skin = null ;
		{
			Rect textField12Rect = new Rect( 600, 725 , 100 , 25 ) ;
			GUI.TextField( textField12Rect  , m_TextField ) ;	
		}				
	}

	private string m_TextArea = "textArea" ;
	private void DrawGUITextArea()
	{
		{
			Rect textArea1Rect = new Rect( 750, 0 , 100 , 50 ) ;
			m_TextArea = GUI.TextArea( textArea1Rect  , m_TextArea ) ;
		}
		
		{
			Rect textArea2Rect = new Rect( 750, 70 , 100 , 50 ) ;
			GUI.TextArea( textArea2Rect  , m_TextArea ) ;
		}

		{
			Rect textArea3Rect = new Rect( 750, 140 , 100 , 50 ) ;
			GUI.TextArea( textArea3Rect , "" ) ;
		}

		{
			Rect textArea4Rect = new Rect( 750, 210 , 100 , 40 ) ;
			GUI.TextArea( textArea4Rect  , m_TextArea + m_TextArea + m_TextArea ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect textArea5Rect = new Rect( 750, 252 , 100 , 40 ) ;
			GUI.TextArea( textArea5Rect  ,  m_TextArea + m_TextArea + m_TextArea , 10 ) ;
		}
		
		if( null != m_Texture1 )
		{
			// Rect textArea6Rect = new Rect( 750, 350 , 100 , 50 ) ;
			// GUI.TextArea( textArea6Rect  , new GUIContent ( m_Texture1 ) ) ;
		}
		
		if( null != m_Texture1 )
		{
			// Rect textArea7Rect = new Rect( 750, 420 , 100 , 50 ) ;
			// GUI.TextArea( textArea7Rect  , new GUIContent ( "textArea7" , m_Texture1 ) ) ;
		}
		
		{
//			Rect textArea8Rect = new Rect( 750, 490 , 100 , 50 ) ;
//			GUI.TextArea( textArea8Rect  , new GUIContent ( "textArea8" , "textArea8 tooltip" ) ) ;
//			
//			Rect tooltipRect = new Rect( Input.mousePosition.x , 
//									     Screen.height - Input.mousePosition.y , 
//										 100 , 50 ) ;
//			GUI.Label( tooltipRect , GUI.tooltip ) ;
		}
		
		{
			Rect textArea9Rect = new Rect( 750, 560 , 100 , 50 ) ;
			GUI.TextArea( textArea9Rect  , m_TextArea + m_TextArea + m_TextArea , m_Style1 ) ;	
		}
		
		if( null != m_Texture1 )
		{
//			Rect textArea10Rect = new Rect( 750, 630 , 100 , 50 ) ;
//			GUI.TextArea( textArea10Rect  , m_Texture1 , m_Style1 ) ;	
		}
		
		
		GUI.skin = m_Skin1 ;
		{
			Rect textArea11Rect = new Rect( 750, 700 , 100 , 25 ) ;
			GUI.TextArea( textArea11Rect  , m_TextArea ) ;	
		}		
		GUI.skin = null ;
		{
			Rect textArea12Rect = new Rect( 750, 725 , 100 , 25 ) ;
			GUI.TextArea( textArea12Rect  , m_TextArea ) ;	
		}				
	}
	
	public Rect window1Rect = new Rect( 0 , 0 , 120 , 700 ) ;
	public Rect window2Rect = new Rect( 200 , 0 , 280 , 700 ) ;
	public Rect window3Rect = new Rect( 500 , 0 , 450 , 700 ) ;
	private void DrawGUIWindow()
	{
		{
			GUI.Window( 0 , window1Rect , window1 , "" ) ;
		}
		{
			window2Rect = GUI.Window( 1 , window2Rect , window2 , 
				 new GUIContent ( "window3" , m_Texture1 ) ) ;
		}
		{
			window3Rect = GUI.Window( 2 , window3Rect , window3 , "window3"  ) ;
		}		
	}
	
	private void window1( int windowID ) 
	{
		DrawGUIBox() ;
	}
	
	private void window2( int windowID ) 
	{
		DrawGUIButton() ;
	}
	
	private void window3( int windowID ) 
	{
		DrawGUIToggle() ;
		GUI.DragWindow (new Rect ( 0 ,0 ,200 , 350 ) ) ;
	}
	
	public float m_SliderValue = 0 ;
	public float m_ScrollBar3Value = 0 ;
	public float m_ScrollBar4Value = 0 ;
	public Vector2 m_ScrollPosition = Vector2.zero;
	private void DrawGUISlider()
	{
		{
			Rect slider1Rect = new Rect( 0 , 0 , 100 , 50 ) ;
			m_SliderValue = GUI.HorizontalSlider( slider1Rect , 
				m_SliderValue , 0 , 1 ) ;
		}
		
		{
			Rect slider2Rect = new Rect( 0 , 70 , 100 , 50 ) ;
			m_SliderValue = GUI.VerticalSlider( slider2Rect , 
				m_SliderValue , 1 , 0 ) ;
		}

		{
			Rect scrollbar3Rect = new Rect( 0 , 140 , 100 , 50 ) ;
			m_ScrollBar3Value = GUI.HorizontalScrollbar( scrollbar3Rect , 
				m_ScrollBar3Value , 0 , 0 , 100 ) ;
		}

		// @todo consider invert sequence and minus value
		// @todo demonstration size.
		{
			Rect scrollbar4Rect = new Rect( 50 , 210 , 100 , 100 ) ;
			m_ScrollBar4Value = GUI.VerticalScrollbar( scrollbar4Rect , 
				m_ScrollBar4Value , 0 , 0 , -100 ) ;
		}
		
		
	}	
	
	private void DrawGUIScrollView()
	{
		{
			Rect viewRect = new Rect( 150 , 0 , 200 , 350 ) ;
			Rect totalAreaRect = new Rect( 0 , 0 , 400 , 800 ) ;
			m_ScrollPosition = GUI.BeginScrollView( viewRect , 
													m_ScrollPosition , 
													totalAreaRect ) ;
			DrawGUIBox() ;
			DrawGUIButton() ;
			GUI.EndScrollView() ;
		}
		
	}	
	
	private void DrawGUIGroup()
	{
		{
			Rect groupRect = new Rect( 450 , 0 , 200 , 150 ) ;
			GUI.BeginGroup( groupRect , "group" ) ;
			DrawGUIBox() ;
			DrawGUIButton() ;			
			GUI.EndGroup() ;
		}
		
		{
			Rect groupRect = new Rect( 450 , 170 , 200 , 150 ) ;
			GUI.BeginGroup( groupRect , "group2" ) ;
			// DrawGUIBox() ;
			DrawGUIButton() ;			
			GUI.EndGroup() ;
		}
		
		if( null != m_Texture1 )
		{
			Rect groupRect = new Rect( 450 , 340 , 200 , 150 ) ;
			GUI.BeginGroup( groupRect , m_Texture1 ) ;
			DrawGUIBox() ;
			DrawGUIButton() ;			
			GUI.EndGroup() ;
		}
		
		{
			Rect groupRect = new Rect( 450 , 510 , 200 , 150 ) ;
			GUI.BeginGroup( groupRect , m_Style1 ) ;
			DrawGUIBox() ;
			DrawGUIButton() ;			
			GUI.EndGroup() ;
		}
		
	}	
	
	
	private void DrawGUITexture()
	{
		if( null != m_Texture1 )
		{
			Rect drawTextureRect = new Rect( 750, 0 , 100 , 50 ) ;
			GUI.DrawTexture( drawTextureRect , m_Texture1 ) ;
		}
		
		/*
			ScaleAndCrop 	
				Scales the texture, maintaining aspect ratio, 
				so it completely covers the position rectangle 
				passed to GUI.DrawTexture. 
				If the texture is being draw to a rectangle with a different aspect ratio 
				than the original, the image is cropped.
			
			ScaleToFit 	
				Scales the texture, maintaining aspect ratio, 
				so it completely fits withing the position rectangle 
				passed to GUI.DrawTexture.	
				
			StretchToFill 	
				Stretches the texture to fill the complete rectangle 
				passed in to GUI.DrawTexture.
				
			
		 */
		if( null != m_Texture1 )
		{
			Rect drawTextureRect = new Rect( 750, 70 , 100 , 100 ) ;
			GUI.DrawTexture( drawTextureRect , m_Texture1 , ScaleMode.ScaleAndCrop ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect drawTextureRect = new Rect( 750, 170 , 100 , 100 ) ;
			GUI.DrawTexture( drawTextureRect , m_Texture1 , ScaleMode.ScaleToFit ) ;
		}
		
		if( null != m_Texture1 )		
		{
			Rect drawTextureRect = new Rect( 750, 310 , 100 , 100 ) ;
			GUI.DrawTexture( drawTextureRect , m_Texture1 , ScaleMode.StretchToFill ) ;
		}
		
		
		if( null != m_SpriteTexture )		
		{
			Rect drawTextureRect = new Rect( 0, 400 , 400 , 100 ) ;
			GUI.DrawTexture( drawTextureRect , m_SpriteTexture  ) ;
		}
		
		if( null != m_SpriteTexture )		
		{
			Rect drawTextureRect = new Rect( 50 , 550 , 49 , 49 ) ;
			
			GUI.DrawTextureWithTexCoords( drawTextureRect , 
										  m_SpriteTexture , 
										  m_TextureCoordinate  ) ;
		}
	}	
}
