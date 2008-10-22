<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:import href="TeamFoundation.xsl"/>
  <!-- Common TeamSystem elements -->
  <xsl:template match="BuildStatusChangeEvent">
    <head>
      <title>Team System Build Quality Change</title>
      <!-- Pull in the common style settings -->
      <xsl:call-template name="style">
      </xsl:call-template>
    </head>
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
    <body>
      <table>
        <tr>
          <td>Team project:</td>
          <td class="PropValue">
            <xsl:value-of select="TeamProject" />
          </td>
        </tr>

        <tr>
          <td>Build Number:</td>
          <td class="PropValue">
            <xsl:value-of select="Id" />
          </td>
        </tr>
        
        <tr>
          <td>Details:</td>
          <td class="PropValue">
            Build Quality changed by <xsl:value-of select="ChangedBy"/> from &apos;<xsl:value-of select="StatusChange/OldValue"/>&apos; to &apos;<xsl:value-of select="StatusChange/NewValue"/>&apos;
          </td>
        </tr>

        <tr>
          <td>Changed by:</td>
          <td class="PropValue">
            <xsl:value-of select="ChangedBy" />
          </td>
        </tr>
        <tr>
          <td>Changed on:</td>
          <td class="PropValue">
            <xsl:value-of select="ChangedTime" />
          </td>
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
