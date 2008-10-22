<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:import href="TeamFoundation.xsl"/> <!-- Common TeamSystem elements -->
<xsl:template match="/CheckinEvent">
    <!-- Use the common alert/webview template -->
    <xsl:call-template name="CheckinEvent">
        <xsl:with-param name="CheckinEvent" select="."/>
    </xsl:call-template>
</xsl:template>
</xsl:stylesheet>
