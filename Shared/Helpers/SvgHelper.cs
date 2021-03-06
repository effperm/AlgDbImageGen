﻿namespace PuzzleImageGenerator.Shared.Helpers
{
    public static class SvgHelper
    {
        public static string GetHeader(ImageProp properties)
        {
            return "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox =\"0 0 " + properties.ImageSize.Item1 + " " +
                properties.ImageSize.Item2 + "\" preserveAspectRatio=\"xMidYMid meet\">\n\n\t<g  stroke-linejoin='round' >\n";
        }

        public static string GetPolygonText(CoordPair[] polygon, string fill = "lightgray", string stroke = "black", double width = 4)
        {
            var polygonText = "";

            polygonText += "\t\t<polygon points=\"";

            foreach (CoordPair coordPair in polygon)
                polygonText += coordPair.ToString();

            polygonText = polygonText.Trim();

            polygonText += "\" stroke=\"" + stroke + "\" stroke-width=\"" + width + "\" fill=\"" + fill + "\"/>\"\n\n";

            return polygonText;
        }

        public static string GetFooter()
        {
            return "\t</g>\n</svg>";
        }
    }
}
