<?xml version="1.0" encoding="UTF-8" ?>
<!--

Microsoft Team System - Work Item Tracking

Read only work item style sheet

WorkItemViewer default stylesheet

Copyright (c) Microsoft Corporation.  All rights reserved.

-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" indent="yes" doctype-public="-//W3C//DTD HTML 3.2 FINAL//EN"/>
<xsl:template match="//WorkItem">
    <table cellSpacing="0" cellPadding="0" width="100%" border="0" class="title">
        <tr>
            <td><xsl:value-of select="/WorkItem/Classification/System.WorkItemType/text()"/>&#160;<xsl:value-of select="/WorkItem/Classification/System.Id/text()"/>:&#160;<xsl:value-of select="/WorkItem/Classification/System.Title/text()"/></td>
        </tr>
    </table>
    <table class="heading" width="100%" border="0">
        <tr>
            <td>Classification</td>
    	</tr>
    </table>    
    <table class="content" width="100%" border="0">
        <tr>
            <td vAlign="top" width="25%" nowrap="true">Area:</td>
            <td align="left" width="75%"><xsl:value-of select="/WorkItem/Classification/System.AreaPath/text()"/></td>
        </tr>
        <tr>
            <td vAlign="top" width="25%" nowrap="true">Iteration:</td>
            <td align="left" width="75%"><xsl:value-of select="/WorkItem/Classification/System.IterationPath/text()"/></td>
        </tr>
    </table>
    <!-- Summary Block: Description and History -->
    <table width="100%" border="0">
      <tr>
        <!-- Fields, Links and Attachments -->
        <td vAlign="top" width="50%">
            <!-- Fields -->
            <xsl:apply-templates select="//RevisionFields"/>

            <!-- Links -->
            <table class="heading" width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>Links</td>
            	</tr>
            </table>
            <table class="content" border="0" width="100%" cellspacing="0">
        			<tr>
        				<th>Link Type</th>
        				<th>Description</th>
        				<th>Comments</th>
        			</tr>
                    <xsl:apply-templates select="//RelatedLinks"/>
                    <xsl:apply-templates select="//HyperLinks"/>
                    <xsl:apply-templates select="//ExternalLinks"/>
        	</table>

            <!-- Attachments -->
            <table class="heading" width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>Attachments</td>
            	</tr>
            </table>
            <table class="content" border="0" width="100%" cellspacing="0">
        			<tr>
        				<th>Name</th>
        				<th>Size</th>
        				<th>Comments</th>
        			</tr>
                    <xsl:apply-templates select="//Attachments"/>  
        	</table>
        </td>
        <!-- Description and History -->
        <td vAlign="top" width="50%">
            <table cellSpacing="0" width="100%" border="0">
              <tr vAlign="top">
                <!-- Description -->
                <td><xsl:apply-templates select="//WorkItem/Description"/></td>
              </tr>
              <tr Align="top">
                <!-- History -->
                  <td>
                  <table width="95%" border="0">
                    <tr class="heading"><td>History:</td></tr>
                    <tr class="content">
                      <td>
                        <!-- History Entries -->
                        <xsl:apply-templates select="//Discussion/Entry"/>
        
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr Align="top">
                <!-- Long Text -->
                  <td>
                  <table width="95%" border="0">
                    <tr class="heading"><td>Other Fields:</td></tr>
                    <tr>
                      <td>
                        <!-- Long text fields -->
                        <xsl:apply-templates select="//RevisionFields/FieldX"/>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
        </td>
      </tr>
    </table>
    <table width="100%" border="0">
          <tr>
        	<td valign="bottom">
        	<!--Notes-->
        	<table cellpadding="0" cellspacing="0">
        		<tr>
        			<td class="footer">Note: All dates and times are GMT <xsl:value-of select="/WorkItem/@Offset"/>&#160;<xsl:value-of select="/WorkItem/@TimeZone"/></td>
        		</tr>
        		<tr>
        			<td class="footer">Provided by: <a href="http://msdn.microsoft.com/vstudio/teamsystem">Microsoft Visual Studio® Team System 2008.</a></td>
        		</tr>
        	</table>
        	</td>
          </tr>
    </table>
</xsl:template>

<!-- Fields -->
<xsl:template match="//RevisionFields">
<table class="heading" width="100%" border="0">
    <tr>
        <td>Fields</td>
	</tr>
</table>
<xsl:apply-templates select="//Field"/>
</xsl:template>

<!-- Related -->
<xsl:template match="//RelatedLinks">
<xsl:apply-templates select="//Related"/>
</xsl:template>

<!-- Hyperlinks -->
<xsl:template match="//HyperLinks">
<xsl:apply-templates select="//Hyperlink"/>
</xsl:template>

<!-- External Links -->
<xsl:template match="//ExternalLinks">
<xsl:apply-templates select="//ExternalLink"/>
</xsl:template>

<!-- Attachments -->
<xsl:template match="//Attachments">
<xsl:apply-templates select="//Attachment"/>
</xsl:template>

<!-- RelatedLinks -->
<xsl:template match="//Related">
    <tr>
        <td>Related Work Item</td>
        <td><xsl:element name='a'><xsl:attribute name='href'>
                                      workitem.aspx?artifactMoniker=<xsl:value-of select="RelatedID/text()"/>
                                  </xsl:attribute>
                                  <xsl:attribute name='target'>new</xsl:attribute>
                                  WorkItem <xsl:value-of select="RelatedID/text()"/>
            </xsl:element></td>
        <td><xsl:value-of select="Comment/text()"/></td>
    </tr>
</xsl:template>

<!-- Hyperlinks -->
<xsl:template match="//Hyperlink">
    <tr>
        <td>Work Item Hyperlink</td>
        <td><xsl:element name='a'><xsl:attribute name='href'><xsl:value-of select="Url/text()"/></xsl:attribute><xsl:value-of select="Url/text()"/></xsl:element></td>
        <td><xsl:value-of select="Comment/text()"/></td>
    </tr>
</xsl:template>

<!-- ExternalLink -->
<xsl:template match="//ExternalLink">
    <tr>
        <td><xsl:value-of select="Artifact/text()"/></td>
        <td><xsl:element name='a'><xsl:attribute name='href'><xsl:value-of select="Url/text()"/></xsl:attribute>
            <xsl:attribute name='target'>new</xsl:attribute>
            <xsl:value-of select="Description/text()"/></xsl:element></td>
        <td><xsl:value-of select="Comment/text()"/></td>
    </tr>
</xsl:template>

<!-- Attachments -->
<xsl:template match="//Attachment">
    <tr>
        <td><xsl:element name='a'><xsl:attribute name='href'>
                                  v1.0/AttachFileHandler.ashx?FileID=<xsl:value-of select="ID/text()"/>&#38;FileName=<xsl:value-of select="FileName/text()"/>
                                  </xsl:attribute>
                                  <xsl:attribute name='target'>new</xsl:attribute>
                                  <xsl:value-of select="FileName/text()"/>
            </xsl:element></td>
        <td><xsl:value-of select="Size/text()"/>&#160;KB</td>
        <td><xsl:value-of select="Comment/text()"/></td>
    </tr>
</xsl:template>

<xsl:template match="//Field">
<table class="content" width="100%" border="0">
    <tr>
        <td vAlign="top" width="50%"><xsl:value-of select="@Name"/></td>
        <td><xsl:value-of select="Value/text()"/></td>
    </tr>
</table>
</xsl:template>

<xsl:template match="//FieldX[@Type='HTML']">
<table width="95%" border="0">
    <tr>
        <td class="heading" vAlign="top" width="50%"><xsl:value-of select="@Name"/>:</td>
    </tr>
    <tr>
        <td class="content">
		<script>
                    <![CDATA[document.write(']]><xsl:value-of select="Value/text()"/><![CDATA[');]]>
                </script>
	</td>
    </tr>
    <tr><td>&#160;</td></tr>
</table>
</xsl:template>

<xsl:template match="//FieldX[@Type='PlainText']">
<table width="95%" border="0">
    <tr>
        <td class="heading" vAlign="top" width="50%"><xsl:value-of select="@Name"/>:</td>
    </tr>
    <!--tr>
        <td class="content">
		<xsl:value-of select="Value/text()"/
	</td>
    </tr-->
    <tr>
        <td class="content">
		<script>
                    <![CDATA[document.write(unescape(']]><xsl:value-of select="Value/text()"/><![CDATA['));]]>
                </script>
	</td>
    </tr>
    <tr><td>&#160;</td></tr>
</table>
</xsl:template>
    
<xsl:template match="//Description">
    <!-- //Description -->
          <table width="95%" border="0">
            <tr class="heading">
              <td>Description:</td>
            </tr>
            <tr>
              <td class="content">
                <script>
                    <![CDATA[document.write(']]><xsl:value-of select="Text/text()"/><![CDATA[');]]>
                </script>
              </td>
            </tr>
          </table>
</xsl:template>    
    
<xsl:template match="//Discussion/Entry">
    <!-- //Discussion/Entry -->
                <!-- History Entry -->
    <b><xsl:value-of select="AddedDate/text()"/> Comment by <xsl:value-of select="ChangedBy/text()"/></b><br/>
        <script>
            <![CDATA[document.write(']]><xsl:value-of select="Text/text()"/><![CDATA[');]]>
        </script>
    <br/><br/>           
</xsl:template>
    
<xsl:template match="//Error">  
    <!-- //Error -->
    <table width="100%" >
        <tr>
            <td class="heading" width="100%" colspan="3" valign="top" align="center">
                <xsl:value-of select="Message/text()"/>
            </td>
        </tr>
    </table>            
</xsl:template>
    
</xsl:stylesheet>

