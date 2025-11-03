<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EyePe2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EyePe2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>

<div style="width: 100%; padding: 0 0 0 0;">
    <uc1:GcsCtl runat="server" ID="gcsCtl" />
</div>
<fieldset style="width: 97%;">
<table style="width: 45%; padding: 0 0 0 0;">
    <legend>&nbsp;Localist Status&nbsp;</legend>
    <table style="width: 100%">

        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="labelcaption" style="width: 40%">Right Eye</td>
                        <td class="labelcaption" style="width: 20%">&nbsp;</td>
                        <td class="labelcaption" style="width: 40%">Left Eye</td>

                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRVisus" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Visus</td>
                        <td>
                            <telerik:RadTextBox ID="txtLVisus" runat="server" Width="100%" /></td>

                    </tr>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRRefractio" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Refractio</td>
                        <td>
                            <telerik:RadTextBox ID="txtLRefractio" runat="server" Width="100%" /></td>

                    </tr>--%>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRTension" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Tension culi</td>
                        <td>
                            <telerik:RadTextBox ID="txtLTension" runat="server" Width="100%" />
                        </td>

                    </tr>--%>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRCorrection" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Correction</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCorrection" runat="server" Width="100%" /></td>

                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRGlasses" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Reading glasses</td>
                        <td>
                            <telerik:RadTextBox ID="txtLGlasses" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtROcular" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Intra-ocular pressure</td>
                        <td>
                            <telerik:RadTextBox ID="txtLOcular" runat="server" Width="100%" /></td>
                    </tr>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRAnterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Segmen anterior</td>
                        <td>
                            <telerik:RadTextBox ID="txtLAnterior" runat="server" Width="100%" /></td>
                    </tr>--%>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRPosterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Segmen Posterior</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPosterior" runat="server" Width="100%" /></td>
                    </tr>--%>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtREyeBallPosition" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Eye Ball Position</td>
                        <td>
                            <telerik:RadTextBox ID="txtLEyeBallPosition" runat="server" Width="100%" /></td>
                    </tr>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtREyeBallMovement" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Eye Ball Movement</td>
                        <td>
                            <telerik:RadTextBox ID="txtLEyeBallMovement" runat="server" Width="100%" /></td>
                    </tr>--%>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRConfrontation" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Confrontation</td>
                        <td>
                            <telerik:RadTextBox ID="txtLConfrontation" runat="server" Width="100%" /></td>
                    </tr>--%>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtROrbitalBone" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Orbital Bone</td>
                        <td>
                            <telerik:RadTextBox ID="txtLOrbitalBone" runat="server" Width="100%" /></td>
                    </tr>--%>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRPalpebra" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Palpebrae</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPalpebra" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRConjungtivaTars" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Conjungtiva Tars</td>
                        <td>
                            <telerik:RadTextBox ID="txtLConjungtivaTars" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRConjungtivaBulbi" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Conjungtiva Bulbi</td>
                        <td>
                            <telerik:RadTextBox ID="txtLConjungtivaBulbi" runat="server" Width="100%" /></td>
                    </tr>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRSclera" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Sclera</td>
                        <td>
                            <telerik:RadTextBox ID="txtLSclera" runat="server" Width="100%" /></td>
                    </tr>--%>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRLimbCornea" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Limb Cornea</td>
                        <td>
                            <telerik:RadTextBox ID="txtLLimbCornea" runat="server" Width="100%" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRCornea" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Cornea</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCornea" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRCameraOculiAnterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Camera Oculi Anterior</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCameraOculiAnterior" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRIris" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Iris</td>
                        <td>
                            <telerik:RadTextBox ID="txtLIris" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRPupil" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Pupil</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPupil" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRLens" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Lens</td>
                        <td>
                            <telerik:RadTextBox ID="txtLLens" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRFundus" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Funduscopy</td>
                        <td>
                            <telerik:RadTextBox ID="txtLFundus" runat="server" Width="100%" /></td>
                    </tr>
<%--                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRCorpusVitreum" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Corpus Vitreum</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCorpusVitreum" runat="server" Width="100%" /></td>
                    </tr>--%>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtROther" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Other</td>
                        <td>
                            <telerik:RadTextBox ID="txtLOther" runat="server" Width="100%" /></td>
                    </tr>
                </table>

            </td>
        </tr>
        </table>
    </fieldset>
<fieldset style="width: 97%;">
    <legend>&nbsp; Ishihara Test&nbsp;</legend>
    <table width="100%">
        <tr>
            <td class="label" valign="top">Result</td>
            <td style="width: 280px" valign="top">
                <asp:RadioButtonList ID="optIshihara" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No Test" Value="X"></asp:ListItem>
                    <asp:ListItem Text="No Color Blind" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Color Blind :" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtIshihara" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" />
            </td>
        </tr>
    </table>
</fieldset>
<table width="100%">
    <tr>
        <td class="label" valign="top">Notes</td>

        <td colspan="2">
            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
        
    </tr>
</table>
<td style="width: 50%;" valign="top">
            <table style="width: 100%;">
                <tr>
<%--                    <td valign="top" class="label">Family Medical History</td>--%>
                    <td>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function entryLocalistStatus(mod, rimid, bodyId, parid, unit) {
            var url = '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx?mod=' + mod + '&parid=' + parid + '&patid=<%= PatientID %>&rimid=' + rimid + '&regno=<%= RegistrationNo %>&unit=' + unit + '&bodyId=' + bodyId + '&ccm=rebind&cet=<%=lvLocalistStatus.ClientID %>';
            window.openWinEntryMaxWindow(url);
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="lvLocalistStatus">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lvLocalistStatus" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>

<%--FORMAT VERTICAL --%>
<telerik:RadListView ID="lvLocalistStatus" runat="server" RenderMode="Lightweight"
    ItemPlaceholderID="BodyImageContainer" OnNeedDataSource="lvLocalistStatus_NeedDataSource">
    <LayoutTemplate>
        <fieldset style="width: 98%;">
            <legend><b>LOCALIST STATUS</b></legend>
                    <asp:PlaceHolder ID="BodyImageContainer" runat="server"></asp:PlaceHolder>
        </fieldset>
    </LayoutTemplate>
    <ItemTemplate>
            <table style="width: 100%; border: 1px solid gray;">
                <tr>
                    <td style="width: 210px">
                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit"
                            OnClientClick='<%# string.Format("entryLocalistStatus(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");return false;", 
                                                        DataBinder.Eval(Container.DataItem, "EntryMode"),        
                                                        RegistrationInfoMedicID, 
                                                        DataBinder.Eval(Container.DataItem, "BodyID"),
                                                        ParamedicID,
                                                        ServiceUnitID)%>'>
                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                Width="200px" Height="200px" ResizeMode="Fit" DataValue='<%# Eval("BodyImage") == DBNull.Value? new System.Byte[0]: Eval("BodyImage") %>'></telerik:RadBinaryImage>
                        </asp:LinkButton>
                    </td>
                    <td style="vertical-align: top">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="3"><%#DataBinder.Eval(Container.DataItem, "BodyName")%></td>
                            </tr>
                            <tr>
                                <td style="width: 150px;">Add:&nbsp;<%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute))%></td>
                                <td style="width: 150px;">Upd:&nbsp;<%#string.Format("{0}",Eval("LastUpdateDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("LastUpdateDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute))%></td>
                                <td></td>
                            </tr>
                        </table>
                        <telerik:RadTextBox runat="server" ID="txtNotes" Width="100%" Height="160px" Resize="Vertical" TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem, "Notes")%>'></telerik:RadTextBox>
                    </td>
                </tr>
            </table>
        <br/>
    </ItemTemplate>
</telerik:RadListView>
                        </td>
    </tr>
</table>
    </td>