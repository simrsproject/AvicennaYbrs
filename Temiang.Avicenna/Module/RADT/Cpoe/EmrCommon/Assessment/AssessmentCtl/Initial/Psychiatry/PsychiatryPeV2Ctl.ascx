<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PsychiatryPeV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.PsychiatryPeV2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<telerik:RadCodeBlock ID="codeBlock" runat="server">
    <script type="text/javascript">
        function entryGenogram() {
            var url = '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/Common/Genogram/GenogramEntry.aspx?mod=edit&rimid=<%= RegistrationInfoMedicID %>&patid=<%= PatientID %>&ccm=submit&cet=<%=imgGenogram.ClientID %>&rand=' + Math.random();
            window.openWinNoTitlebar(url);
        }

        $(document).ready(function () {
            $("#<%=txtNeurologis.ClientID%>").specialinput();
            });
   </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="imgGenogram">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="imgGenogram" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<fieldset style="height: 300px; overflow: auto;">
    <legend>GENOGRAM</legend>
    <a onclick="entryGenogram();return false;" style="cursor: pointer;">
        <img style='padding: 4px' src='../../../../../../../Images/Toolbar/edit16.png' alt='edit' /></a>
    <telerik:RadBinaryImage ID="imgGenogram" runat="server" Width="400px" Height="300px" ResizeMode="Fit"></telerik:RadBinaryImage>
</fieldset>

<table width="100%">
    <tr>
        <td width="50%" valign="top">
            <fieldset>
                <legend>PHYSICAL EXAMINATION</legend>
                <uc1:GcsCtl runat="server" ID="gcsCtl" />
                <table width="100%">
                    <tr>
                        <td class="label">Sensory</td>
                        <td>
                            <telerik:RadTextBox ID="txtSensorik" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Motor</td>
                        <td>
                            <telerik:RadTextBox ID="txtMotorik" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Autonomous</td>
                        <td>
                            <telerik:RadTextBox ID="txtOtonom" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Neurological examination</td>
                        <td>
                            <telerik:RadTextBox ID="txtNeurologis" runat="server" Width="100%" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="labelcaption">Ancillary Examination</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="labelcaption"></td>
                        <td>
                            <telerik:RadTextBox ID="txtOtherExam" runat="server" Width="100%" TextMode="MultiLine" Resize="Vertical" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>MENTAL STATUS</legend>
                <table width="100%">
                    <tr>
                        <td class="label"></td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optJnsUsia" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Child" Value="A" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Teenagers" Value="R"></asp:ListItem>
                                <asp:ListItem Text="Adult" Value="T"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Appearance</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optPenampilan" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Looks Accordance" Value="LA" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Not Accordance" Value="NA"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Attitude</td>
                        <td style="width: 150px;">
                            <asp:RadioButtonList ID="optSikap" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Text="Minus" Value="MIN" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Uncooperative and seemed calm jittery" Value="UNC"></asp:ListItem>
                                <asp:ListItem Text="Back and forth" Value="BNA"></asp:ListItem>
                                <asp:ListItem Text="Etc" Value="ETC"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td valign="bottom">
                            <telerik:RadTextBox ID="txtSikap" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">General Conditions Looks Sick</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtKondisiUmum" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Awareness</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtKesadaran" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Ability Concentration </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optConcentration" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Good" Value="G" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Minus" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Bad" Value="B"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Ability to maintain attention</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optMaintainCon" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Good" Value="G" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Minus" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Bad" Value="B"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">The ability to distract</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optDistractCon" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Good" Value="G" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Minus" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Bad" Value="B"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Memory</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optMemory" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Good" Value="G" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Minus" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Bad" Value="B"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Judgement</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optJudgement" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Good" Value="G" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Minus" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Bad" Value="B"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Insight</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtInsight" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Mood</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtMood" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Affect</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtAfek" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Perception</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtPerception" runat="server" Width="100%" />
                        </td>
                    </tr>

                    <tr>
                        <td class="label">Thought process</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtProsesPikir" runat="server" Width="100%" />
                        </td>
                    </tr>


                    <tr>
                        <td class="label">Flow Thought</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtArusPikir" runat="server" Width="100%" />
                        </td>
                    </tr>


                    <tr>
                        <td class="label">Fill Thought</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtIsiPikir" runat="server" Width="100%" />
                        </td>
                    </tr>

                    <tr>
                        <td class="label">Psychodynamic formulation</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtPsikodinamik" runat="server" Width="100%" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Insomnia</td>
                        <td colspan="2">
                            <asp:CheckBox runat="server" ID="chkInsomnia" />&nbsp;&nbsp;
                            <telerik:RadTextBox ID="txtInsomnia" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Other</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtOtherThing" runat="server" Width="100%" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Notes
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                                TextMode="MultiLine" Resize="Vertical" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
        <td width="50%" valign="top">

            <fieldset>
                <legend>DIAGNOSIS MULTIAXIAL</legend>
                <table width="100%">
                    <tr>
                        <td class="label">Axis I</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAksis1" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Axis II</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAksis2" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Axis III</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAksis3" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Axis IV</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAksis4" runat="server" Width="100%" />
                        </td>
                    </tr>
<%--                    <tr>
                        <td class="label">Axis V</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAksis5" runat="server" Width="100%" />
                        </td>
                    </tr>--%>
                </table>
            </fieldset>
            <fieldset>
                <legend>THERAPY</legend>
                <table width="100%">
 <%--                   <tr>
                        <td class="label">Psychotherapeutic</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPsikofarmaka" runat="server" Width="100%" TextMode="MultiLine" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="label">Psychoterapi</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPsikoterapi" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Psychoeducation</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPsikoedukasi" runat="server" Width="100%" />
                        </td>
                    </tr>
<%--                    <tr>
                        <td class="label">Psychosocial interventions</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPsikososial" runat="server" Width="100%" />
                        </td>
                    </tr>--%>

                </table>
            </fieldset>
            <fieldset>
                <legend>PROGNOSIS</legend>
                <table width="100%">
                    <tr>
                        <td class="label">Quo Ad Vitam</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtVitam" runat="server" Width="100%" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Quo Ad Functionam</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFunctionam" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Quo Ad Sanation</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSanation" runat="server" Width="100%" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
</table>



