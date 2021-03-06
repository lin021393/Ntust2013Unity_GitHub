/*
@file OnGUI05.cs
@author NDark
@date 20130830 file started.
*/
using UnityEngine;

public class OnGUI05 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float m_Speed = -0.05f ;
	public float m_HPValue = 1 ;
	public Rect m_HPButtomRect = new Rect( 100 , 100 , 200 , 50 ) ;
	public Rect m_HPTopValueRect = new Rect( 100 , 100 , 200 , 50 ) ;
	public Texture m_TextureButtom = null ;
	public Texture m_TextureTop = null ;
	
	
	public Rect m_DetailHPButtomRect = new Rect( 100 , 300 , 400 , 50 ) ;
	public Rect m_DetailHPTopValueRect = new Rect( 100 , 300 , 400 , 50 ) ;
	public Texture m_TextureTopWithDetail = null ;
	
	public Rect m_TexCoordHPButtomRect = new Rect( 100 , 400 , 400 , 50 ) ;
	public Rect m_TexCoordHPTopValueRect = new Rect( 100 , 400 , 400 , 50 ) ;
	public Rect m_TexCoordHPTopValueTexCoord = new Rect( 0 , 0 , 1 , 1 ) ;
	
	void OnGUI()
	{
		m_HPValue += ( m_Speed * Time.deltaTime ) ;
		if( m_HPValue < 0 || m_HPValue > 1 )
			m_Speed *= -1.0f ;
		
		if( null != m_TextureButtom &&
			null != m_TextureTop )
		{
			GUI.DrawTexture( m_HPButtomRect , m_TextureButtom ) ;
			
			m_HPTopValueRect.width = m_HPValue * m_HPButtomRect.width ;
			GUI.DrawTexture( m_HPTopValueRect , m_TextureTop ) ;
		}
		
		
		if( null != m_TextureButtom &&
			null != m_TextureTopWithDetail )
		{
			GUI.DrawTexture( m_DetailHPButtomRect , m_TextureButtom ) ;
			
			m_DetailHPTopValueRect.width = m_HPValue * m_DetailHPButtomRect.width ;
			GUI.DrawTexture( m_DetailHPTopValueRect , m_TextureTopWithDetail ) ;
			
		}		
		
		if( null != m_TextureButtom &&
			null != m_TextureTopWithDetail )
		{
			GUI.DrawTexture( m_TexCoordHPButtomRect , m_TextureButtom ) ;
			
			m_TexCoordHPTopValueRect.width = m_HPValue * m_TexCoordHPButtomRect.width ;
			m_TexCoordHPTopValueTexCoord.width = m_HPValue ;
			GUI.DrawTextureWithTexCoords( m_TexCoordHPTopValueRect , 
										  m_TextureTopWithDetail , 
										  m_TexCoordHPTopValueTexCoord ) ;
			
		}
		

	}
	
	private void windowFunc( int _ID )
	{
		
	}
}
