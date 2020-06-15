﻿namespace QRST.WorldGlobeTool.Geometries
{
    /// <summary>
    /// 二维地理信息矩形包围盒
    /// </summary>
    public class GeographicBoundingBox
    {
        public double North;
        public double South;
        public double West;
        public double East;
        public double MinimumAltitude;
        public double MaximumAltitude;

        public GeographicBoundingBox()
        {
            North = 90;
            South = -90;
            West = -180;
            East = 180;
        }

        public GeographicBoundingBox(double north, double south, double west, double east)
        {
            North = north;
            South = south;
            West = west;
            East = east;
        }

        public GeographicBoundingBox(double north, double south, double west, double east, double minAltitude, double maxAltitude)
        {
            North = north;
            South = south;
            West = west;
            East = east;
            MinimumAltitude = minAltitude;
            MaximumAltitude = maxAltitude;
        }

        public bool Intersects(GeographicBoundingBox boundingBox)
        {
            if (North <= boundingBox.South ||
                South >= boundingBox.North ||
                West >= boundingBox.East ||
                East <= boundingBox.West)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void ExtentGeoBBox(GeographicBoundingBox boundingBox)
        {
            North = (North < boundingBox.North) ? boundingBox.North : North;
            South = (South > boundingBox.South) ? boundingBox.South : South;
            East = (East < boundingBox.East) ? boundingBox.East : East;
            West = (West > boundingBox.West) ? boundingBox.West : West;
            MaximumAltitude = (MaximumAltitude < boundingBox.MaximumAltitude) ? boundingBox.MaximumAltitude : MaximumAltitude;
            MinimumAltitude = (MinimumAltitude > boundingBox.MinimumAltitude) ? boundingBox.MinimumAltitude : MinimumAltitude;
        }
    }
}
