<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetDepreciationEditor.ascx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.AssetDepreciationEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Label ID="lblAssetId" runat="server" Visible="false"></asp:Label>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 5%; vertical-align: top;">&nbsp;</td>
        <td style="width: 45%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td>
                        <br />
                        <p><b>To generate asset depreciation list for this asset click Generate button below, as long as there are no Depreciation Journals created for this asset you can regenerate the list multiple times.</b></p>
                        <br />
                        <asp:Button ID="btnUpdate" Text="Generate" runat="server" CommandName="PerformInsert" />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" />
                    </td>
                </tr>
            </table>
            
        </td>
    </tr>
</table>
<br /><br />