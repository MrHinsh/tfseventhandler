<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:import href="TeamFoundation.xsl"/> <!-- Common TeamSystem elements -->
  <xsl:template match="WorkItemChangedEvent">
      <head>
      <!-- Pull in the command style settings -->
      <xsl:call-template name="style">
      </xsl:call-template>
      </head>
      <body>
          <div class="Title"><b>
            <xsl:choose>
              <xsl:when test="DisplayUrl[.!='']">
                <xsl:element name="a">
                  <xsl:attribute name="href">
                    <xsl:value-of select="DisplayUrl" />
                  </xsl:attribute>
                  Work item
                  <xsl:if test="ChangeType[.='New']">
                    Created:
                  </xsl:if>
                  <xsl:if test="ChangeType[.='Change']">
                    Changed:
                  </xsl:if>
                  <xsl:for-each select="CoreFields/StringFields/Field">
                    <xsl:if test="ReferenceName[.='System.WorkItemType']">
                      <xsl:value-of select="NewValue"/>
                    </xsl:if>
                  </xsl:for-each>
                  <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                  <xsl:for-each select="CoreFields/IntegerFields/Field">
                    <xsl:if test="ReferenceName[.='System.Id']">
                      <xsl:value-of select="NewValue"/>
                    </xsl:if>
                  </xsl:for-each>
                  -
                  <xsl:value-of select="WorkItemTitle" />
                </xsl:element>
              </xsl:when>
              <xsl:otherwise>
                Work item
                <xsl:if test="ChangeType[.='New']">
                  Created:
                </xsl:if>
                <xsl:if test="ChangeType[.='Change']">
                  Changed:
                </xsl:if>
                <xsl:for-each select="CoreFields/StringFields/Field">
                  <xsl:if test="ReferenceName[.='System.WorkItemType']">
                    <xsl:value-of select="NewValue"/>
                  </xsl:if>
                </xsl:for-each>
                <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                <xsl:for-each select="CoreFields/IntegerFields/Field">
                  <xsl:if test="ReferenceName[.='System.Id']">
                    <xsl:value-of select="NewValue"/>
                  </xsl:if>
                </xsl:for-each>
                -
                <xsl:value-of select="WorkItemTitle" />
              </xsl:otherwise>
            </xsl:choose>            
        </b>
          </div>
        <br />
        <table>
          <tr>
            <td>
                Team project:
            </td>
            <td class="PropValue">
                <xsl:value-of select="PortfolioProject" />
            </td>
          </tr>
          <tr>
            <td>
                Area:
            </td>
            <td class="PropValue">
                <xsl:value-of select="AreaPath" />
            </td>
          </tr>
          <tr>
            <td>
                Iteration:
            </td>
            <td class="PropValue">
                <xsl:for-each select="CoreFields/StringFields/Field">
                  <xsl:if test="ReferenceName[.='System.IterationPath']">
                    <xsl:value-of select="NewValue"/>
                  </xsl:if>
                </xsl:for-each>
            </td>
          </tr>
          <tr>
            <td>
                Assigned to:
            </td>
            <td class="PropValue">
                <xsl:for-each select="CoreFields/StringFields/Field">
                  <xsl:if test="ReferenceName[.='System.AssignedTo']">
                    <xsl:value-of select="NewValue"/>
                  </xsl:if>
                </xsl:for-each>
            </td>
          </tr>
          <tr>
            <td>
                State:
            </td>
            <td class="PropValue">
                <xsl:for-each select="CoreFields/StringFields/Field">
                  <xsl:if test="ReferenceName[.='System.State']">
                    <xsl:value-of select="NewValue"/>
                  </xsl:if>
                </xsl:for-each>
            </td>
          </tr>
          <tr>
            <td>
                Reason:
            </td>
            <td class="PropValue">
                <xsl:for-each select="CoreFields/StringFields/Field">
                  <xsl:if test="ReferenceName[.='System.Reason']">
                    <xsl:value-of select="NewValue"/>
                  </xsl:if>
                </xsl:for-each>
            </td>
          </tr>
          <tr>
            <td>
                Changed by:
            </td>
            <td class="PropValue">
                <xsl:for-each select="CoreFields/StringFields/Field">
                  <xsl:if test="ReferenceName[.='System.ChangedBy']">
                    <xsl:value-of select="NewValue"/>
                  </xsl:if>
                </xsl:for-each>
            </td>
          </tr>
          <tr>
            <td>
                Changed date:
            </td>
            <td class="PropValue">
                <xsl:for-each select="CoreFields/StringFields/Field">
                  <xsl:if test="ReferenceName[.='System.ChangedDate']">
                    <xsl:value-of select="NewValue"/>
                  </xsl:if>
                </xsl:for-each>
            </td>
          </tr>
        </table>

        <br/>

        <xsl:if test="boolean(/WorkItemChangedEvent/ChangedFields/IntegerFields/Field) or boolean(/WorkItemChangedEvent/ChangedFields/StringFields/Field) or boolean(/WorkItemChangedEvent/TextFields/TextField)">
            <xsl:if test="ChangeType[.='New']">
              Other fields
            </xsl:if>
            <xsl:if test="ChangeType[.='Change']">
              Changed fields
            </xsl:if>
        </xsl:if>

        <xsl:if test="ChangeType[.='Change']">

          <table class="WithBorder">
            <xsl:if test="boolean(/WorkItemChangedEvent/TextFields/TextField)">
              <tr>
                <td class="ColHeadingMedium">
                    Field
                </td>
                <td class="ColHeading">
                    New Value
                </td>
              </tr>
            </xsl:if>

            <xsl:for-each select="TextFields/TextField">
              <tr>
                <td class="Col1Data">
                    <xsl:value-of select="Name"/>
                </td>
                <xsl:choose>
                  <xsl:when test="Value[.='']">
                    <td class="ColData">
                      <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td class="ColData">
                        <xsl:value-of disable-output-escaping="yes" select="Value"/>
                    </td>
                  </xsl:otherwise>
                </xsl:choose>
              </tr>
            </xsl:for-each>
          </table>

          <xsl:if test="boolean(/WorkItemChangedEvent/TextFields/TextField)">
            <br/>
          </xsl:if>

          <xsl:if test="boolean(/WorkItemChangedEvent/ChangedFields/IntegerFields/Field) or boolean(/WorkItemChangedEvent/ChangedFields/StringFields/Field)">
          <table class="WithBorder">
              <xsl:if test="boolean(/WorkItemChangedEvent/ChangedFields/IntegerFields/Field) or boolean(/WorkItemChangedEvent/ChangedFields/StringFields/Field)">
              <tr>
                <td class="ColHeadingMedium">
                    Field
                </td>
                <td class="ColHeading">
                    New Value
                </td>
                <td class="ColHeading">
                    Old Value
                </td>
              </tr>
              </xsl:if>

            <xsl:for-each select="ChangedFields/IntegerFields/Field">
              <tr>
                <td class="Col1Data">
                    <xsl:value-of select="Name"/>
                </td>
                <xsl:choose>
                  <xsl:when test="NewValue[.='']">
                    <td class="ColData">
                      <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td class="ColData">
                        <xsl:value-of select="NewValue"/>
                    </td>
                  </xsl:otherwise>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="OldValue[.='']">
                    <td class="ColData">
                      <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td class="ColData">
                        <xsl:value-of select="OldValue"/>
                    </td>
                  </xsl:otherwise>
                </xsl:choose>
              </tr>
            </xsl:for-each>
            <xsl:for-each select="ChangedFields/StringFields/Field">
                <xsl:if test="ReferenceName[.!='System.ChangedBy']">
                    <tr>
                        <td class="Col1Data">
                            <xsl:value-of select="Name"/>
                        </td>
                        <xsl:choose>
                            <xsl:when test="NewValue[.='']">
                                <td class="ColData">
                                    <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                                </td>
                            </xsl:when>
                            <xsl:otherwise>
                                <td class="ColData">
                                    <xsl:value-of select="NewValue"/>
                                </td>
                            </xsl:otherwise>
                        </xsl:choose>
                        <xsl:choose>
                            <xsl:when test="OldValue[.='']">
                                <td class="ColData">
                                    <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                                </td>
                            </xsl:when>
                            <xsl:otherwise>
                                <td class="ColData">
                                    <xsl:value-of select="OldValue"/>
                                </td>
                            </xsl:otherwise>
                        </xsl:choose>
                    </tr>
                </xsl:if>
            </xsl:for-each>
          </table>
      </xsl:if> 
      </xsl:if> <!-- changetype = change -->

        <xsl:if test="ChangeType[.='New']">
          <table class="WithBorder">
            
            <xsl:if test="boolean(/WorkItemChangedEvent/TextFields/TextField) or boolean(/WorkItemChangedEvent/ChangedFields/IntegerFields/Field) or boolean(/WorkItemChangedEvent/ChangedFields/StringFields/Field)">
              <tr>
                <td class="ColHeadingMedium">
                    Field
                </td>
                <td class="ColHeading">
                    New Value
                </td>
              </tr>
            </xsl:if>

            <xsl:for-each select="TextFields/TextField">
              <tr>
                <td class="Col1Data">
                    <xsl:value-of select="Name"/>
                </td>
                <xsl:choose>
                  <xsl:when test="Value[.='']">
                    <td class="ColData">
                      <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td class="ColData">
                        <xsl:value-of disable-output-escaping="yes" select="Value"/>
                    </td>
                  </xsl:otherwise>
                </xsl:choose>
              </tr>
            </xsl:for-each>

            <xsl:for-each select="ChangedFields/IntegerFields/Field">
              <tr>
                <td class="Col1Data">
                    <xsl:value-of select="Name"/>
                </td>
                <xsl:choose>
                  <xsl:when test="NewValue[.='']">
                    <td class="ColData">
                      <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td class="ColData">
                        <xsl:value-of select="NewValue"/>
                    </td>
                  </xsl:otherwise>
                </xsl:choose>
              </tr>
            </xsl:for-each>
            <xsl:for-each select="ChangedFields/StringFields/Field">
                <xsl:if test="ReferenceName[.!='System.ChangedBy']">
                    <tr>
                        <td class="Col1Data">
                            <xsl:value-of select="Name"/>
                        </td>
                        <xsl:choose>
                            <xsl:when test="NewValue[.='']">
                                <td class="ColData">
                                    <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
                                </td>
                            </xsl:when>
                            <xsl:otherwise>
                                <td class="ColData">
                                    <xsl:value-of select="NewValue"/>
                                </td>
                            </xsl:otherwise>
                        </xsl:choose>
                    </tr>
                </xsl:if>
            </xsl:for-each>
          </table>
        </xsl:if>

        <xsl:if test="boolean(/WorkItemChangedEvent/AddedFiles) or boolean(/WorkItemChangedEvent/AddedResourceLinks) or boolean(/WorkItemChangedEvent/AddedRelations)">
          <br/>
          Links and Attachments
          <table class="WithBorder">
            <tr>
              <td class="ColHeadingMedium">
                Type
              </td>
              <td class="ColHeading">
                Description
              </td>              
            </tr>

            <xsl:for-each select="AddedFiles/AddedFile">
              <tr>
                <td class="Col1Data">
                  File Attachment
                </td>
                <td class="ColData">
                  <xsl:value-of select="Name"/>
                </td>                
              </tr>
            </xsl:for-each>

            <xsl:for-each select="AddedResourceLinks/AddedResourceLink">
              <tr>
                <td class="Col1Data">
                  Link
                </td>
                <td class="ColData">
                  <xsl:value-of select="Resource"/>
                </td>                
              </tr>
            </xsl:for-each>

            <xsl:for-each select="AddedRelations/AddedRelation">
              <tr>
                <td class="Col1Data">
                  Related Work Item
                </td>
                <td class="ColData">
                  <xsl:value-of select="WorkItemId"/>
                </td>                
              </tr>
            </xsl:for-each>
            
          </table>
        </xsl:if>       

        <xsl:if test="boolean(/WorkItemChangedEvent/DeletedFiles)">
          <br/>
          1 or more attachments have been deleted.  See work item for details.
        </xsl:if>
        
        <xsl:if test="boolean(/WorkItemChangedEvent/DeletedResourceLinks)">
          <br/>
          1 or more links have been deleted.  See work item for details.
        </xsl:if>

        <xsl:if test="boolean(/WorkItemChangedEvent/ChangedResourceLinks)">
          <br/>
          1 or more links have been changed.  See work item for details.
        </xsl:if>

        <xsl:if test="boolean(/WorkItemChangedEvent/DeletedRelations)">
          <br/>
          1 or more related work items have been deleted.  See work item for details.
        </xsl:if>

        <xsl:if test="boolean(/WorkItemChangedEvent/ChangedRelations)">
          <br/>
          1 or more related work items have been changed.  See work item for details.
        </xsl:if>

        <xsl:call-template name="footer">
      <xsl:with-param name="format" select="'html'"/>
      <xsl:with-param name="alertOwner" select="Subscriber"/>
      <xsl:with-param name="timeZoneOffset" select="TimeZoneOffset"/>
      <xsl:with-param name="timeZoneName" select="TimeZone"/>
      </xsl:call-template>
      </body>
  </xsl:template>
</xsl:stylesheet>
