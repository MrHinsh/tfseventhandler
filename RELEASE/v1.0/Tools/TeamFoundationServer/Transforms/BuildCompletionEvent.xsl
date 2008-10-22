<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:import href="TeamFoundation.xsl"/> <!-- Common TeamSystem elements -->
  <xsl:template match="BuildCompletionEvent">
      <head>
        <title>Team System Build Completed</title>
        <!-- Pull in the common style settings -->
        <xsl:call-template name="style">
        </xsl:call-template>
        <div class="Title">
        <xsl:call-template name="link">
          <xsl:with-param name="format" select="'html'"/>
          <xsl:with-param name="embolden" select="'true'"/>
          <xsl:with-param name="fontSize" select="'larger'"/>
          <xsl:with-param name="link" select="Url"/>
          <xsl:with-param name="displayText" select="Title"/>
        </xsl:call-template>
        </div>
        <br/>
      </head>
      <body>
        <table>
          <tr>
              <td>Team project: </td>
              <td class="PropValue"><xsl:value-of select="TeamProject"/></td>
          </tr>
          <tr>
              <td>Build Number: </td>
              <td class="PropValue"><xsl:value-of select="Id"/></td>
          </tr>
          <tr>
              <td>Build started by: </td>
              <td class="PropValue">
				  <xsl:value-of select="RequestedBy"/>
            </td>
          </tr>
          <tr>
              <td>Build machine: </td>
              <td class="PropValue">
				  <xsl:value-of select="BuildMachine"/>
			  </td>
          </tr>
          <tr>
               <td>Build started on:</td>
               <td class="PropValue"><xsl:value-of select="BuildStartTime" /></td>
          </tr>

          <tr>
               <td>Build completed on:</td>
               <td class="PropValue"><xsl:value-of select="BuildCompleteTime" /></td>
          </tr>

        </table>
        <br/>
        <xsl:call-template name="footer">
        <xsl:with-param name="format" select="'html'"/>
        <xsl:with-param name="alertOwner" select="Subscriber"/>
        <xsl:with-param name="timeZoneOffset" select="TimeZoneOffset"/>
        <xsl:with-param name="timeZoneName" select="TimeZone"/>
        </xsl:call-template>
        </body>
  </xsl:template>
</xsl:stylesheet>
