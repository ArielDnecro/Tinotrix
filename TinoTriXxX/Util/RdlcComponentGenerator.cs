using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoTriXxX.Util
{
    public static class RdlcComponentGenerator
    {
        #region Image
        /// <summary>
        /// Generate Image Component 
        /// </summary>
        /// <param name="name">Name of the image</param>
        /// <param name="value">The resource can be the Url of the image</param>
        /// <param name="width">Widht of the Image component</param>
        /// <param name="height">Height of the Image component</param>
        /// <param name="leftPadding">Left Padding</param>
        /// <param name="topPadding">Top Padding</param>
        /// <param name="rightPadding">Right Padding</param>
        /// <param name="bottomPadding">Bottom Padding</param>
        /// <param name="imageOrigin">Image Origin set by default External</param>
        /// <param name="imageSizing">The type of image sizing</param>
        /// <returns></returns>
        public static string GenerateImage(string name, string value,
            double width, double height,
            double leftPadding = 0.01, double topPadding = 0.01, double rightPadding = 0.01, double bottomPadding = 0.01,
            double left = 0, double top = 0,
            ImageOriginType imageOrigin = 0, ImageSizingType imageSizing = 0)
        {
            string rdlcImage = $@"<Image Name=""{name}"">
                                    <Source>{imageOrigin.ToString()}</Source>
                                    <Value>{value}</Value>
                                    { (imageSizing == ImageSizingType.AutoSize ? "" : "<Sizing>" + imageSizing.ToString() + "</Sizing>") }
                                    <Left>{left}cm</Left>
                                    <Top>{top}cm</Top>
                                    <Height>{height}cm</Height>
                                    <Width>{width}cm</Width>
                                    <Style>
                                        <Border>
                                            <Style>None</Style>
                                        </Border>
                                        <PaddingLeft>{leftPadding}cm</PaddingLeft>
                                        <PaddingRight>{rightPadding}cm</PaddingRight>
                                        <PaddingTop>{topPadding}cm</PaddingTop>
                                        <PaddingBottom>{bottomPadding}cm</PaddingBottom>
                                    </Style>            
                                </Image>";
            return rdlcImage;
        }

        /// <summary>
        /// Image Source Origin
        /// </summary>
        public enum ImageOriginType
        {
            External,
            Inserted
        }

        /// <summary>
        /// Image Sizing Type
        /// <para>(Fit - 0) => Change the size of the image to adapt it to control</para>        
        /// <para>(FitProportional - 1) => Change the size of the image to fit without losing the aspect ratio</para>
        /// <para>(Clip - 2) => Crop the image to adapt the size to the control</para>
        /// <para>(AutoSize - 3) => Original image source size</para>        
        /// </summary>
        public enum ImageSizingType
        {
            Fit,
            FitProportional,
            Clip,
            AutoSize,
        }
        #endregion

        #region Rectangule
        public static string GenerateRectangule(string name, string content, double width, double height, double topPosition)
        {
            string rectangule = $@"<Rectangle Name=""{name}"">
                                {(content == string.Empty ? "" : "<ReportItems>" + content + "</ReportItems>")}
                                <KeepTogether>true</KeepTogether>
                                <Top>{topPosition}cm</Top>
                                <Height>{height}cm</Height>
                                <Width>{width}cm</Width>
                                <ZIndex>1</ZIndex>
                                <Style>
                                    <Border>
                                        <Style>None</Style>
                                    </Border>
                                </Style>
                                </Rectangle>";

            return rectangule;
        }
        #endregion

        #region Aux
        public static double CmToInch(double cm)
        {
            return cm / 2.54;
        }

        public static double InchToCm(double inches)
        {
            return inches * 2.54;
        }

        public static double MillimitersToCentimeters(double mm)
        {
            return mm / 10;
        }

        public static double CentimetersToMillimiters(double cm)
        {
            return cm * 10;
        }


        #endregion
    }
}
