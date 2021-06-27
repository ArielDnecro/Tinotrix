using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoTriXxX.Util
{
    public class RdlcGenerator
    {
        #region Report Properties
        /// <summary>
        /// Left margin of the report page (cm)
        /// </summary>
        public double LeftMarginPage { get; set; } = 2.54;

        /// <summary>
        /// Top margin of the report page (cm)
        /// </summary>
        public double TopMarginPage { get; set; } = 2.54;

        /// <summary>
        /// Right margin of the report page (cm)
        /// </summary>
        public double RightMarginPage { get; set; } = 2.54;

        /// <summary>
        /// Bottom margin of the report page (cm)
        /// </summary>
        public double BottomMarginPage { get; set; } = 2.54;

        /// <summary>
        /// Width of the report page (cm)
        /// </summary>
        public double PageWidth { get; set; } = 21.6;
        /// <summary>
        /// Height of the report page (cm)
        /// </summary>
        public double PageHeight { get; set; } = 27.9;
        #endregion

        #region Body Report Properties
        public string rdlcBody { get; set; } = string.Empty;

        /// <summary>
        /// Width of the body (cm)
        /// </summary>
        public double rdlcBodyWidth { get; set; } = 10;

        /// <summary>
        /// Height if the body (cm)
        /// </summary>
        public double rdlcBodyHeight { get; set; } = 10;

        /// <summary>
        /// Background of the body
        /// </summary>
        public string rdlcBodyBackground { get; set; } = "White";
        #endregion

        #region Constructor
        public RdlcGenerator() { }
        #endregion

        #region Methods
        public Stream GenerateReport()
        {
            Stream ret = new MemoryStream(Encoding.UTF8.GetBytes(this.GenerateRdlc()));
            return ret;
        }

        private string GenerateRdlc()
        {
            string rdlc = $@"<?xml version='1.0' encoding='utf-8'?>
                            <Report xmlns='http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition' xmlns:rd='http://schemas.microsoft.com/SQLServer/reporting/reportdesigner'>
                                <Body>
                                    {(this.rdlcBody == string.Empty ? "" : "<ReportItems>" + this.rdlcBody + "</ReportItems>")}
                                    <Height>{this.rdlcBodyHeight}cm</Height>
                                    <Style>
                                        <BackgroundColor>White</BackgroundColor>
                                    </Style>
                                </Body>
                                <Width>{this.rdlcBodyWidth}cm</Width>
                                <Page>
                                    <PageHeight>{this.PageHeight}cm</PageHeight>
                                    <PageWidth>{this.PageWidth}cm</PageWidth>
                                    <LeftMargin>{this.LeftMarginPage}cm</LeftMargin>
                                    <RightMargin>{this.RightMarginPage}cm</RightMargin>
                                    <TopMargin>{this.TopMarginPage}cm</TopMargin>
                                    <BottomMargin>{this.BottomMarginPage}cm</BottomMargin>
                                    <ColumnSpacing>0cm</ColumnSpacing>
                                    <Style>
                                        <BackgroundColor>White</BackgroundColor>
                                    </Style>
                                </Page>
                                <AutoRefresh>0</AutoRefresh>
                                <rd:ReportUnitType>Cm</rd:ReportUnitType>
                                <rd:ReportID>39253684-51b0-40fc-a059-76cc0d1b7d0f</rd:ReportID>
                            </Report>";

            return rdlc;
        }

        public void AddContentToBody(string content)
        {
            this.rdlcBody += content;
        }

        public void RefreshBody()
        {
            this.rdlcBody = string.Empty;
        }
        #endregion
    }
}
