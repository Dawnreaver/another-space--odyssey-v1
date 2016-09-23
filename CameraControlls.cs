using UnityEngine;

public class CameraControlls : MonoBehaviour
{
    public enum InputMethods {Controller, Mouse, Touch}
    public InputMethods m_inputMethod;

    public float m_cameraPositionXMax = 100.0f;
    public float m_cameraPositionXMin = 0.0f;
    public float m_cameraPositionYMax = 100.0f;
    public float m_cameraPositionYMin = 0.0f;

    #region controller variables 
    public float m_controllerSensitivity = 0.5f;
    #endregion
    #region mouse variables 
    public float m_mouseSensitivity = 0.5f;
    private Vector3 m_lastMousePosition;
    #endregion
    #region touch variables 
    public float m_touchSensitivity = 0.5f;
    #endregion

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (m_inputMethod)
        {
            case InputMethods.Controller:
                if(Input.GetAxis("HorizontalLeft") > 0.1f || Input.GetAxis("HorizontalLeft") < -0.1f && gameObject.transform.position.x <= m_cameraPositionXMax && gameObject.transform.position.x >= m_cameraPositionXMin)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x+ 1*Time.deltaTime*m_controllerSensitivity*Input.GetAxis("HorizontalLeft"), gameObject.transform.position.y, gameObject.transform.position.z);
                }
                
                if (Input.GetAxis("VerticalLeft") > 0.1f || Input.GetAxis("VerticalLeft") < -0.1f && gameObject.transform.position.z <= m_cameraPositionYMax && gameObject.transform.position.z >= m_cameraPositionYMin)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1*Time.deltaTime*m_controllerSensitivity*Input.GetAxis("VerticalLeft"));
                }
                break;

            case InputMethods.Mouse:
                if(Input.GetButtonDown("Fire2") && gameObject.transform.position.x <= m_cameraPositionXMax && gameObject.transform.position.x >= m_cameraPositionXMin)
                {
                    m_lastMousePosition = new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y);
                    //Debug.Log(m_lastMousePosition);
                }

                if(Input.GetButton("Fire2") && gameObject.transform.position.z <= m_cameraPositionYMax && gameObject.transform.position.z >= m_cameraPositionYMin)
                {
                    Vector3 delta = new Vector3(Input.mousePosition.x - m_lastMousePosition.x,0, Input.mousePosition.y- m_lastMousePosition.z);
                    gameObject.transform.Translate(delta.x * m_mouseSensitivity, 0.0f, delta.z * m_mouseSensitivity );
                    m_lastMousePosition = new Vector3(Input.mousePosition.x,0.0f,Input.mousePosition.y);
                    //Debug.Log(m_lastMousePosition);
                }
            break;

            case InputMethods.Touch:
            break;
        }
	}
}
