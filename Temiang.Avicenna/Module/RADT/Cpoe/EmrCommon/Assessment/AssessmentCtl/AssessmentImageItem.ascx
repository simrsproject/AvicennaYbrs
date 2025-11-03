<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssessmentImageItem.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.AssessmentImageItem" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemBalance" runat="server" ValidationGroup="TransChargesItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PaImage"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td runat="server" id="tdImage" style="width: 125px;">
            <fieldset>
                <legend>Image</legend>
            <asp:Image runat="server" ID="imgAssessmentImage" Width="180px" Height="180px" ClientIDMode="Static" />
            
                </fieldset>
            <asp:HiddenField runat="server" ID="hdnAssessmentImage" ClientIDMode="Static" />
        </td>
        <td style="vertical-align: top;">
            <fieldset>
                <legend>Description</legend>
                <telerik:RadTextBox ID="txtDocumentName" runat="server" Width="100%" Height="180px" MaxLength="200" TextMode="MultiLine" />
            </fieldset>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="center"><asp:Button runat="server" Text="Capture Image" ID="btnCaptureImage" Width="176px"
                OnClientClick="openWinWebCamAssessmentImage();return false;" /></td>
        <td align="right">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PaImage"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="TransChargesItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
        </td>
        <td></td>
    </tr>
</table>
<div style="height:4px;"></div>

