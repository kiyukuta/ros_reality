using UnityEngine;
using System.Collections;

public class TFListenerScreenTester : MonoBehaviour
{


	private WebsocketClient wsc;
	private VRTK.VRTK_ControllerEvents controller;
	int trial = 0;
	string topic = "ros_unity";

	string[] possibleOrders = {
		"xyzw", "xywz", "xzyw", "xzwy", "xwyz", "xwzy", "yxwz", "yxzw", "yzwx", "yzxw", "ywzx", "ywxz", "zxyw", "zxwy", "zyxw", "zywx", "zwxy", "zwyx", "wxzy", "wxyz", "wyzx", "wyxz", "wzyx", "wzxy",
		"ayzw", "aywz", "azyw", "azwy", "awyz", "awzy", "yawz", "yazw", "yzwa", "yzaw", "ywza", "ywaz", "zayw", "zawy", "zyaw", "zywa", "zway", "zwya", "wazy", "wayz", "wyza", "wyaz", "wzya", "wzay",
		"xbzw", "xbwz", "xzbw", "xzwb", "xwbz", "xwzb", "bxwz", "bxzw", "bzwx", "bzxw", "bwzx", "bwxz", "zxbw", "zxwb", "zbxw", "zbwx", "zwxb", "zwbx", "wxzb", "wxbz", "wbzx", "wbxz", "wzbx", "wzxb",
		"xycw", "xywc", "xcyw", "xcwy", "xwyc", "xwcy", "yxwc", "yxcw", "ycwx", "ycxw", "ywcx", "ywxc", "cxyw", "cxwy", "cyxw", "cywx", "cwxy", "cwyx", "wxcy", "wxyc", "wycx", "wyxc", "wcyx", "wcxy",
		"xyzd" ,"xydz" ,"xzyd" ,"xzdy" ,"xdyz" ,"xdzy" ,"yxdz" ,"yxzd" ,"yzdx" ,"yzxd" ,"ydzx" ,"ydxz" ,"zxyd" ,"zxdy" ,"zyxd" ,"zydx" ,"zdxy" ,"zdyx" ,"dxzy" ,"dxyz" ,"dyzx" ,"dyxz" ,"dzyx" ,"dzxy",
		"abzw" ,"abwz" ,"azbw" ,"azwb" ,"awbz" ,"awzb" ,"bawz" ,"bazw" ,"bzwa" ,"bzaw" ,"bwza" ,"bwaz" ,"zabw" ,"zawb" ,"zbaw" ,"zbwa" ,"zwab" ,"zwba" ,"wazb" ,"wabz" ,"wbza" ,"wbaz" ,"wzba" ,"wzab",
		"aycw" ,"aywc" ,"acyw" ,"acwy" ,"awyc" ,"awcy" ,"yawc" ,"yacw" ,"ycwa" ,"ycaw" ,"ywca" ,"ywac" ,"cayw" ,"cawy" ,"cyaw" ,"cywa" ,"cway" ,"cwya" ,"wacy" ,"wayc" ,"wyca" ,"wyac" ,"wcya" ,"wcay",
		"ayzd" ,"aydz" ,"azyd" ,"azdy" ,"adyz" ,"adzy" ,"yadz" ,"yazd" ,"yzda" ,"yzad" ,"ydza" ,"ydaz" ,"zayd" ,"zady" ,"zyad" ,"zyda" ,"zday" ,"zdya" ,"dazy" ,"dayz" ,"dyza" ,"dyaz" ,"dzya" ,"dzay",
		"xbcw" ,"xbwc" ,"xcbw" ,"xcwb" ,"xwbc" ,"xwcb" ,"bxwc" ,"bxcw" ,"bcwx" ,"bcxw" ,"bwcx" ,"bwxc" ,"cxbw" ,"cxwb" ,"cbxw" ,"cbwx" ,"cwxb" ,"cwbx" ,"wxcb" ,"wxbc" ,"wbcx" ,"wbxc" ,"wcbx" ,"wcxb",
		"xbzd" ,"xbdz" ,"xzbd" ,"xzdb" ,"xdbz" ,"xdzb" ,"bxdz" ,"bxzd" ,"bzdx" ,"bzxd" ,"bdzx" ,"bdxz" ,"zxbd" ,"zxdb" ,"zbxd" ,"zbdx" ,"zdxb" ,"zdbx" ,"dxzb" ,"dxbz" ,"dbzx" ,"dbxz" ,"dzbx" ,"dzxb",
		"xycd" ,"xydc" ,"xcyd" ,"xcdy" ,"xdyc" ,"xdcy" ,"yxdc" ,"yxcd" ,"ycdx" ,"ycxd" ,"ydcx" ,"ydxc" ,"cxyd" ,"cxdy" ,"cyxd" ,"cydx" ,"cdxy" ,"cdyx" ,"dxcy" ,"dxyc" ,"dycx" ,"dyxc" ,"dcyx" ,"dcxy",
		"abcw" ,"abwc" ,"acbw" ,"acwb" ,"awbc" ,"awcb" ,"bawc" ,"bacw" ,"bcwa" ,"bcaw" ,"bwca" ,"bwac" ,"cabw" ,"cawb" ,"cbaw" ,"cbwa" ,"cwab" ,"cwba" ,"wacb" ,"wabc" ,"wbca" ,"wbac" ,"wcba" ,"wcab",
		"abzd" ,"abdz" ,"azbd" ,"azdb" ,"adbz" ,"adzb" ,"badz" ,"bazd" ,"bzda" ,"bzad" ,"bdza" ,"bdaz" ,"zabd" ,"zadb" ,"zbad" ,"zbda" ,"zdab" ,"zdba" ,"dazb" ,"dabz" ,"dbza" ,"dbaz" ,"dzba" ,"dzab",
		"aycd" ,"aydc" ,"acyd" ,"acdy" ,"adyc" ,"adcy" ,"yadc" ,"yacd" ,"ycda" ,"ycad" ,"ydca" ,"ydac" ,"cayd" ,"cady" ,"cyad" ,"cyda" ,"cday" ,"cdya" ,"dacy" ,"dayc" ,"dyca" ,"dyac" ,"dcya" ,"dcay",
		"xbcd" ,"xbdc" ,"xcbd" ,"xcdb" ,"xdbc" ,"xdcb" ,"bxdc" ,"bxcd" ,"bcdx" ,"bcxd" ,"bdcx" ,"bdxc" ,"cxbd" ,"cxdb" ,"cbxd" ,"cbdx" ,"cdxb" ,"cdbx" ,"dxcb" ,"dxbc" ,"dbcx" ,"dbxc" ,"dcbx" ,"dcxb",
		"abcd" ,"abdc" ,"acbd" ,"acdb" ,"adbc" ,"adcb" ,"badc" ,"bacd" ,"bcda" ,"bcad" ,"bdca" ,"bdac" ,"cabd" ,"cadb" ,"cbad" ,"cbda" ,"cdab" ,"cdba" ,"dacb" ,"dabc" ,"dbca" ,"dbac" ,"dcba" ,"dcab"
	};


	// Use this for initialization
	void Start ()
	{
		GameObject wso = GameObject.FindWithTag ("WebsocketTag");
		wsc = wso.GetComponent<WebsocketClient> ();
		wsc.Subscribe (topic, "std_msgs/String", "none", 0);

		GameObject RightControllerObject = GameObject.FindWithTag ("RightControllerTag");
		controller = RightControllerObject.GetComponent<VRTK.VRTK_ControllerEvents> ();

		InvokeRepeating ("checkButtons", .2f, .2f);
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

	void checkButtons ()
	{
		if (controller.gripPressed) {
			trial++;
			Debug.Log (possibleOrders [trial]);
		}
	}

	void FixedUpdate ()
	{
		string message = wsc.messages[topic];
		string[] dataPairs = message.Split (';');

		if (dataPairs.Length > 0) {
			for (int i = 0; i < dataPairs.Length; i++) {
				string[] dataPair = dataPairs [i].Split (':');
				GameObject cur = GameObject.Find (dataPair [0]); // replace with hashmap
				if (cur != null/* && dataPair [0] != "screen"*/) {

					string[] tmp = dataPair [1].Split (')');
					string pos = tmp [0];
					string rot = tmp [1];
					pos = pos.Substring (1, pos.Length - 1);
					rot = rot.Substring (1, rot.Length - 1);

					string[] poses = pos.Split (',');
					float pos_x = float.Parse (poses [0]);
					float pos_y = float.Parse (poses [1]);
					float pos_z = float.Parse (poses [2]);

					Vector3 curPos = new Vector3 (pos_x, pos_y, pos_z);


					string[] rots = rot.Split (',');
					float rot_x = float.Parse (rots [0]);
					float rot_y = float.Parse (rots [1]);
					float rot_z = float.Parse (rots [2]);
					float rot_w = float.Parse (rots [3]);


					Quaternion curRot = new Quaternion (rot_x, rot_y, rot_z, rot_w);

					cur.transform.position = RosToUnityPositionAxisConversion (curPos);
					cur.transform.rotation = RosToUnityQuaternionConversion (curRot);

					if (dataPair [0] == "screen") {
						Debug.Log ("break");
						Debug.Log (cur.transform.rotation);

						//cur.transform.Rotate (RosToUnityRotationAxisConversion(new Vector3 (-90f, 0f, 90f)));
						//Debug.Log (cur.transform.rotation);
					}
				}
			}
		}
	}

	Vector3 RosToUnityPositionAxisConversion (Vector3 rosIn)
	{
		return new Vector3 (-rosIn.x, rosIn.z, -rosIn.y);	
	}

	Quaternion RosToUnityQuaternionConversion (Quaternion rosIn)
	{
		return new Quaternion (rosIn.x, -rosIn.z, rosIn.y, rosIn.w);
	}

	Vector3 RosToUnityRotationAxisConversion(Vector3 rosIn) 
	{
		Vector3 inDeg = rosIn * 180f / 3.14159f;
		if (rosIn.y > 0.01) {
			return new Vector3 (inDeg.x, -inDeg.z, inDeg.y);
		} else {
			return new Vector3 (inDeg.x, -inDeg.z, 0);
		}
	}

	Quaternion ConversionTester (Quaternion rosIn, int i)
	{
		string order = possibleOrders [i];
		Quaternion output = new Quaternion ();
		for (int j = 0; j < order.Length; j++) {
			if (order [j] == 'x')
				output [j] = rosIn.x;
			else if (order [j] == 'y')
				output [j] = rosIn.y;
			else if (order [j] == 'z')
				output [j] = rosIn.z;
			else if (order [j] == 'w')
				output [j] = rosIn.w;
			else if (order [j] == 'a')
				output [j] = -rosIn.x;
			else if (order [j] == 'b')
				output [j] = -rosIn.y;
			else if (order [j] == 'c')
				output [j] = -rosIn.z;
			else if (order [j] == 'd')
				output [j] = -rosIn.w;
		}

		return output;
	}

	//Vector3 RosToUnityRotationAxisConversion(Quaternion rosIn) OLD, I don't think this ever worked
	//{
	//	float roll = Mathf.Atan2 (2 * rosIn.y * rosIn.w + 2 * rosIn.x * rosIn.z, 1 - 2 * rosIn.y * rosIn.y - 2 * rosIn.z * rosIn.z);
	//	float pitch = Mathf.Atan2 (2 * rosIn.x * rosIn.w + 2 * rosIn.y * rosIn.z, 1 - 2 * rosIn.x * rosIn.x - 2 * rosIn.z * rosIn.z);
	//	float yaw = Mathf.Asin (2 * rosIn.x * rosIn.y + 2 * rosIn.z * rosIn.w);
	//	return new Vector3 (-rosIn.x, rosIn.z, -rosIn.y);
	//}


}
