using UnityEngine;
using System.Collections;
using System;

public class PointCloudListener : MonoBehaviour {


	private WebsocketClient wsc;
	string topic;
	public int framerate = 1;
	public string compression = "none"; //"png" is the other option, haven't tried it yet though
	int numParticles;
	ParticleSystem.Particle[] cloud;

	// Use this for initialization
	void Start () {
		numParticles = this.GetComponent<ParticleSystem> ().maxParticles;
		GameObject wso = GameObject.FindWithTag ("WebsocketTag");
		wsc = wso.GetComponent<WebsocketClient> ();
		topic = "openni/depth_registered/points";
		wsc.Subscribe (topic, "sensor_msgs/PointCloud2", compression, framerate);

		InvokeRepeating ("renderPointCloud", 5f, 1.0f/framerate);
		//Invoke ("renderPointCloud", 6f);


	}

	void renderPointCloud() {
		int byteOffset = 32;

		string message = wsc.messages[topic];
		//Debug.Log ("Got pointcloud data");

		byte[] bytes = System.Convert.FromBase64String(message);

		//Debug.Log ("there are " + bytes.Length / byteOffset + " points in this message");
		//float[] points = new float[bytes.Length / 4];
		//Buffer.BlockCopy(bytes, 0, points, 0, bytes.Length);
		cloud = new ParticleSystem.Particle[numParticles];

		float x, y, z;

		int i = 0;
		int j = 0;
		while (i < cloud.Length && j < bytes.Length / byteOffset) {
			x = BitConverter.ToSingle (bytes, j * byteOffset + 0);
			y = BitConverter.ToSingle (bytes, j * byteOffset + 4);
			z = BitConverter.ToSingle (bytes, j * byteOffset + 8);
			//Debug.Log (x + ", " + y + ", " + z);
			if (!float.IsNaN (x) && !float.IsNaN (y) && !float.IsNaN (z)) {
				cloud [i].position = RosToUnityPositionAxisConversion (new Vector3 (x, y, z));
				//cloud [i].position = new Vector3(0f, 0f, 0f);
				cloud [i].color = new Color (1f, 1f, 1f, 1f);
				cloud [i].size = 0.05f;
				i++;
				j++;
			} else {
				j++;
			}
		}


		this.GetComponent<ParticleSystem> ().SetParticles (cloud, cloud.Length);


	}

	Vector3 RosToUnityPositionAxisConversion (Vector3 rosIn)
	{
		return new Vector3 (-rosIn.x, rosIn.z, -rosIn.y);	
	}

}
