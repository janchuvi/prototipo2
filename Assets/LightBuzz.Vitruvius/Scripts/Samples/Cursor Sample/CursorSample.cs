using UnityEngine;
using LightBuzz.Vitruvius;
using Windows.Kinect;

public class CursorSample : VitruviusSample
{
    #region Variables

    BodyWrapper body;
    byte[] pixels;
    Body[] allBodies = new Body[0];
    int playerBody;

    public VitruviusVideo vitruviusVideo;

    public KinectUI kinectUI;

    [Tooltip("Maximum distance of user's head, to be recognized as a valid user")]
    public float maxHeadDistance;

    #endregion

    #region Reserved methods // Awake - OnApplicationQuit - Update

    protected override void Awake()
    {
        base.Awake();

        vitruviusVideo.Initialize(OnVideoFrameArrived, null, null);
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        vitruviusVideo.Dispose();
    }

    void Update()
    {
        vitruviusVideo.UpdateVideo();

        if (!vitruviusVideo.videoPlayer.IsPlaying)
        {
            UpdateFrame();
        }
    }

    #endregion

    #region UpdateFrame

    void UpdateFrame()
    {
        byte[] pixels = null;

        switch (visualization)
        {
            case Visualization.Color:
                UpdateColorFrame(out pixels);
                break;
            case Visualization.Depth:
                UpdateDepthFrame(out pixels);
                break;
            default:
                UpdateInfraredFrame(out pixels);
                break;
        }

        UpdateBodyFrame();

        RefreshFrame(body);

        vitruviusVideo.videoRecorder.RecordFrame(pixels, visualization, resolution, body, null, false);
    }

    #endregion

    #region GetAllBodies

    public Body[] GetAllBodies()
    {
        return allBodies;
    }

    public int GetPlayerBodyNum()
    {
        return playerBody;
    }

    #endregion

    #region OnBodyFrameReceived

    private void IdentifyAllBodies(BodyFrame frame)
    {
        // frame.GetAndRefreshBodyData
        allBodies = new Body[KinectSensor.BodyFrameSource.BodyCount];
        System.Collections.Generic.IEnumerator<Body> e = frame.Bodies().GetEnumerator();
        int i = 0;
        float bestDist = float.MaxValue;
        playerBody = -1;
        while (e.MoveNext())
        {
            Body thisBody = e.Current;
            allBodies[i] = thisBody;
            if (thisBody.IsTracked)
            {
                // Calculates that player head is exactly inside the playing hexagon
                CameraSpacePoint csp = thisBody.Joints[Windows.Kinect.JointType.Head].Position;
                float x = csp.X;
                float z = csp.Z;
                bool isIn = (z > 1.285f) && (z < 2.314f) &&
                    (x > -0.603f + (1.8f - z) * 0.1f / 0.514f) &&
                    (x > -0.603f + (z - 1.8f) * 0.1f / 0.514f) &&
                    (x < 0.603f - (1.8f - z) * 0.1f / 0.514f) &&
                    (x < 0.603f - (z - 1.8f) * 0.1f / 0.514f);
                if ((isIn) && (z < bestDist))
                {
                    bestDist = z;
                    playerBody = i;
                }
            }
            i += 1;
        }
        /* if (playerBody >= 0)
        {
            Debug.Log("Dist: " + bestDist.ToString());
        } */
    }

    protected override void OnBodyFrameReceived(BodyFrame frame)
    {
        IdentifyAllBodies(frame);

        Body body = null;
        if (playerBody >= 0)
        {
            body = allBodies[playerBody];
        }
        /*
        Body body = frame.Bodies().Closest();
        if (body != null)
        {
            float distance = body.Joints[JointType.Head].Position.Z;
            Debug.Log("Dist: " + distance.ToString());   // Exagera un poquito, cuando mi cabeza a 2mts justo, dice que estoy a 2.250156
            if (distance > maxHeadDistance)
            {
                Debug.Log("NULL: " + distance.ToString());   // Exagera un poquito, cuando mi cabeza a 2mts justo, dice que estoy a 2.250156
                body = null;
            }
        }
        */

        if (body != null && this.body == null)
        {
            this.body = new BodyWrapper();
        }
        else if (body == null && this.body != null)
        {
            this.body = null;
        }

        if (this.body != null)
        {
            this.body.Set(body, KinectSensor.CoordinateMapper, visualization);
        }
    }

    #endregion

    #region OnVideoFrameArrived

    void OnVideoFrameArrived(Texture2D image, Visualization visualization, ColorFrameResolution resolution, BodyWrapper body, Face face)
    {
        frameView.FrameTexture = image;

        this.body = body;

        RefreshFrame(body);
    }

    #endregion

    #region RefreshFrame

    void RefreshFrame(BodyWrapper body)
    {
        kinectUI.UpdateCursor(body, vitruviusVideo.videoPlayer.IsPlaying);
    }

    #endregion
}