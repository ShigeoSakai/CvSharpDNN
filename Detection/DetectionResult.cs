using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVSharpDNN.Detection
{
	public class DetectionResult
	{
		public int ClassID {  get; private set; }
		public float Confidence { get; private set; }
		public int X { get;private set; }
		public int Y { get;private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int X2 { get; private set; }
		public int Y2 { get; private set;}

		public Rectangle Rectangle { get { return new Rectangle(X,Y,Width,Height); } }

		public DetectionResult(int classID, float confidence, int x, int y, int width, int height)
		{
			ClassID = classID;
			Confidence = confidence;
			X = x;
			Y = y;
			Width = width;
			Height = height;
			X2 = x + width;
			Y2 = y + height;
		}
		public DetectionResult(int classID, int x1, int y1, int x2, int y2, float confidence)
		{
			ClassID = classID;
			Confidence = confidence;
			X = x1;
			Y = y1;
			X2 = x2;
			Y2 = y2;
			Width = (x1 < x2) ? (x2 - x1) : (x1 - x2);
			Height = (y1 < y2) ? (y2 - y1) : (y1 - y2);
		}
	}
}
