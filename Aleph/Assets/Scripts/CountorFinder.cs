using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using OpenCvSharp.Demo;

public class CountorFinder : WebCamera
{
    [SerializeField] private float Threshold = 96.4f;
    [SerializeField] private bool ShowProcessedImage = true;
    [SerializeField] private float CurveAccuracy = 10f;
    [SerializeField] private float MinArea = 5000f;
    [SerializeField] private int WindowOffset = 5;
   
    public Vector2[] vectorList;
    
    public float contourX, contourY;

    private Mat image;
    private Mat processImage = new Mat();
    private Point[][] contours;
    private HierarchyIndex[] hierarchy;
    private Vector2 position;
    private int width = 0;
    private int height = 0;

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
       image = OpenCvSharp.Unity.TextureToMat(input);

       Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
       Cv2.Threshold(processImage, processImage, Threshold, 255, ThresholdTypes.BinaryInv);
       Cv2.FindContours(processImage, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);

       width = image.Width;
       height = image.Height;
       
      

       foreach(Point[] contour in contours)
        {
            Point[] points = Cv2.ApproxPolyDP(contour, CurveAccuracy, true);
            var area = Cv2.ContourArea(contour);

            if(area > MinArea)
            {
                bool isOutOfWindow = false;
                int pointX = 0;
                int pointY = 0;

                foreach (Point point in points) {
                    pointX = point.X;
                    pointY = point.Y;

                    if (point.X < WindowOffset || point.X > width - WindowOffset) {
                        isOutOfWindow = true;
                    }
                    if (point.Y < WindowOffset || point.Y > height - WindowOffset) {
                        isOutOfWindow = true;
                    }
                }
                if (!isOutOfWindow) {
                    position = new Vector2(pointX, pointY);
                   
                }
                

            }
        }

       if(output == null) 
        {
            output = OpenCvSharp.Unity.MatToTexture(ShowProcessedImage ? processImage : image);
        } else
        {
            OpenCvSharp.Unity.MatToTexture(ShowProcessedImage ? processImage : image, output);
        }
       return true;
    }

    public Vector2 getPosition() {
        float x = (width - position.x) / width;
        float y = (height - position.y) / height;
        return new Vector2(x, y);
    }

    public Vector2[] toVector2(Point[] points)
    {
        vectorList = new Vector2[points.Length];

        for(int i = 0; i < points.Length; i++)
        {
            vectorList[i] = new Vector2(points[i].X, points[i].Y);

            contourX = points[i].X /2;
            contourY = points[i].Y / 2;
            
        }
        return vectorList;
    }
   
}
