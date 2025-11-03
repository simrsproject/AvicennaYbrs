<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThtPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.ThtPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>

<table style="width: 100%">
    <tr>
        <td style="width: 50%" valign="top">
            <uc1:GcsCtl runat="server" ID="gcsCtl" />
            <table style="width: 100%">
                <tr>
                    <td class="labelcaption" colspan="3">EARS</td>
                </tr>
                <tr>
                    <td class="labelcaption">Regio</td>
                    <td class="labelcaption">Right</td>
                    <td class="labelcaption">Left</td>
                </tr>
                <tr>
                    <td class="label">
                        <div style="font-weight: bold;">Outer ear:</div>
                        <div style="padding-left: 10px;">• Auricle</div>
                    </td>
                    <td class="entry2Column">
                        <telerik:RadTextBox ID="txtRDaun" runat="server" Width="100%" />
                    </td>
                    <td class="entry2Column">
                        <telerik:RadTextBox ID="txtLDaun" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td class="label" style="padding-left: 20px;">•	External acoustic Meatus</td>
                    <td>
                        <telerik:RadTextBox ID="txtRLiang" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLLiang" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label" style="padding-left: 20px;">•	Membran Tympani</td>
                    <td>
                        <telerik:RadTextBox ID="txtRTympani" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLTympani" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Pre Aurikula</td>
                    <td>
                        <telerik:RadTextBox ID="txtRPreAurikula" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLPreAurikula" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Post Aurikula</td>
                    <td>
                        <telerik:RadTextBox ID="txtRPostAurikula" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLPostAurikula" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Hearing tests</td>
                    <td>
                        <telerik:RadTextBox ID="txtRPendengaran" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLPendengaran" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Balance tests</td>
                    <td>
                        <telerik:RadTextBox ID="txtRKeseimbangan" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLKeseimbangan" runat="server" Width="100%" />
                    </td>

                </tr>


                <tr>
                    <td class="labelcaption" colspan="3">NOSE</td>


                </tr>
                <tr>
                    <td class="labelcaption">Regio</td>
                    <td class="labelcaption">Right</td>
                    <td class="labelcaption">Left</td>

                </tr>
                <tr>
                    <td class="label">Tests Nose</td>
                    <td class="entry2Column">
                        <telerik:RadTextBox ID="txtRTestHidung" runat="server" Width="100%" />
                    </td>
                    <td class="entry2Column">
                        <telerik:RadTextBox ID="txtLTestHidung" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">External Nose</td>
                    <td>
                        <telerik:RadTextBox ID="txtRHidungLuar" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLHidungLuar" runat="server" Width="100%" />
                    </td>

                </tr>


                <tr>
                    <td class="label">
                        <div style="font-weight: bold;">Internal Nose:</div>
                        <div style="padding-left: 10px;">•	Rhinoscopi Anterior</div>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtRAnterior" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLAnterior" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label" style="padding-left: 20px;">•	Rhinoscopi Posterior</td>
                    <td>
                        <telerik:RadTextBox ID="txtRPosterior" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLPosterior" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Sinus Paranasal</td>
                    <td>
                        <telerik:RadTextBox ID="txtRSinus" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLSinus" runat="server" Width="100%" />
                    </td>

                </tr>

                <tr>
                    <td class="labelcaption" colspan="3">THROAT</td>


                </tr>
                <tr>
                    <td class="labelcaption">Regio</td>
                    <td class="labelcaption">Right</td>
                    <td class="labelcaption">Left</td>

                </tr>


                <tr>
                    <td class="label">Oral cavity</td>
                    <td class="entry2Column">
                        <telerik:RadTextBox ID="txtRRonggaMulut" runat="server" Width="100%" />
                    </td>
                    <td class="entry2Column">
                        <telerik:RadTextBox ID="txtLRonggaMulut" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Teeth</td>
                    <td>
                        <telerik:RadTextBox ID="txtRGigi" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLGigi" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Tonsils</td>
                    <td>
                        <telerik:RadTextBox ID="txtRTonsil" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLTonsil" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Faring</td>
                    <td>
                        <telerik:RadTextBox ID="txtRFaring" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLFaring" runat="server" Width="100%" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Laring</td>
                    <td>
                        <telerik:RadTextBox ID="txtRLaring" runat="server" Width="100%" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtLLaring" runat="server" Width="100%" />
                    </td>

                </tr>
            </table>
        </td>
        <td style="width: 50%;" valign="top">
            <table style="width: 100%">
                <tr>
                    <td class="label">Head-Neck</td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtKepalaLeher" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Trakea</td>
                    <td>
                        <telerik:RadTextBox ID="txtTrakea" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Esofagus</td>
                    <td>
                        <telerik:RadTextBox ID="txtEsofagus" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Bronkus</td>
                    <td>
                        <telerik:RadTextBox ID="txtBronkus" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td class="label">Notes
                    </td>
                    <td class="entry" colspan="2">
                        <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td></td>
    </tr>
</table>
