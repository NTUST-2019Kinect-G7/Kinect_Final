using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;
using Joint = Windows.Kinect.Joint;
public class BodysourceView : MonoBehaviour
{
    private float shootingCD = 1.2f;
    private float NextTimecanshoot=0;
    public GameObject cam;
    public BodySourceManager mbodysourceManager;
    //public GameObject dot;
    public Transform Plocation;
    public GameObject MouseLoc;
    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    private List<JointType> _joints = new List<JointType>
    {
        //JointType.HandLeft,
        JointType.HandRight,
        //JointType.Head
       //JointType.SpineMid
       JointType.SpineShoulder,
    };

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("EnemyAmount", 0);
        MouseLoc.SetActive(false);
        PlayerPrefs.SetInt("HangrightState", 0);
        PlayerPrefs.SetInt("start",0);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScript.buffOn == true)
        {
            shootingCD = 0.2f;
        }
        else if(PlayerScript.buffOn == false)
        {
            shootingCD = 1.2f;
        }
        #region Get kinect data
        Body[] data = mbodysourceManager.GetData();
        if (data == null) return;
        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null) continue;
            if (body.IsTracked)
                trackedIds.Add(body.TrackingId);
        }
        #endregion
        #region delete kinect bodies
        List<ulong> knowIds = new List<ulong>(mBodies.Keys);
        foreach (ulong trackedid in knowIds)
        {
            if (!trackedIds.Contains(trackedid))
            {
                Destroy(mBodies[trackedid]);
                mBodies.Remove(trackedid);
            }
        }
        #endregion
        #region create body
        foreach (var body in data)
        {
            if (body == null) continue;
            if (body.IsTracked)
            {
                PlayerPrefs.SetInt("start", 1);
                if (PlayerScript.hp <= 0)
                {
                    MouseLoc.SetActive(true);
                }
                
                if (body.HandRightState == HandState.Closed )
                {
                    if (PlayerScript.hp <= 0 )
                    {
                        if (MouseLoc.transform.position.y > 350.0f)
                        {
                            PlayerScript.RestartGame();
                        }
                        else if (MouseLoc.transform.position.y < 320.0f)
                        {
                            PlayerScript.QuitGame();
                        }
                        PlayerPrefs.SetInt("HangrightState", 1);
                    }
                    else
                    {
                        if (Time.time > NextTimecanshoot)
                        {
                            PlayerPrefs.SetInt("HangrightState", 1);
                            NextTimecanshoot = Time.time + shootingCD;
                        }
                        else
                        {
                            PlayerPrefs.SetInt("HangrightState", 0);
                        }
                    }
                   
                   
                   
                   // Debug.Log("close");
                }
                else if (body.HandRightState == HandState.Open)
                {
                    PlayerPrefs.SetInt("HangrightState", 0);
                   // Debug.Log("open");
                }else if (body.HandRightState == HandState.Lasso)
                {
                    PlayerPrefs.SetInt("HangrightState", 2);
                   // Debug.Log("lasso");
                    var stopmove = new Vector3(0,0,0);
                    PlayerScript.movement = stopmove;
                    break;
                }
                Joint handRight = body.Joints[JointType.HandRight];         // 右手
                Joint spineShoulder = body.Joints[JointType.SpineMid]; //  身體中心
                Vector3 handRightPos = getVec3fromjoint(handRight);
                Vector3 MousePos = new Vector3(handRight.Position.X*1920, handRight.Position.Y * 1080,0);
                Vector3 spineShoulderPos = getVec3fromjoint(spineShoulder);
                MouseLoc.transform.position = MousePos;
                //Debug.Log(MouseLoc);
                // Debug.Log("right:  "+handRightPos);
                // Debug.Log("mid:  " + spineShoulderPos);
                // Debug.Log("123");
                var direction = (handRightPos - spineShoulderPos).normalized;
                if (direction.x > 0)
                {
                    PlayerPrefs.SetInt("direction",0); // 往右
                   // Debug.Log("go right");
                }
                else
                {
                    PlayerPrefs.SetInt("direction", 1);// 往左
                    //Debug.Log("go left");
                }
                PlayerScript.movement = direction;


            }
        }    
        #endregion
    }
    private Vector3 getVec3fromjoint(Joint joint)
    {
        //float  zz = joint.Position.Y*10;
        //float  yy = joint.Position.Z*10;
        float  xx = joint.Position.X*10;
        float yy = joint.Position.Y * 10;
        return new Vector3(xx, yy, 0);
    }

    private GameObject CreateBodyobj(ulong trackingId)
    {
        GameObject body = new GameObject("Body" + trackingId);
        return body;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "restart")
    //    {
    //        PlayerScript.RestartGame();
    //    }
    //    if (collision.tag == "quit")
    //    {
    //        PlayerScript.QuitGame();
    //    }
    //}

}
