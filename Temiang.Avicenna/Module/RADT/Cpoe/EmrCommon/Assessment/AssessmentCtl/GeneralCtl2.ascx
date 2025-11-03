<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralCtl2.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.GeneralCtl2" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<table style="width: 100%; padding: 0 0 0 0;">
    <tr>
        <td style="width: 50%;" valign="top">
            <table width="100%">                                
                <tr>
                    <td class="label">
                        <asp:Label ID="Label4" runat="server" Text="Anamnesis"></asp:Label>
                        <asp:LinkButton runat="server" ID="lbtnPrevAnamnesisNotes" OnClick="lbtnPrevAnamnesisNotes_OnClick" OnClientClick="if (!confirm('Copy other anamnesis from previouse Assessment?')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                            </asp:LinkButton>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtAnamnesisNotes" runat="server" Width="100%" Height="60px" Resize="Vertical"
                            TextMode="MultiLine" />
                    </td>
                </tr>
            </table>
        </td>      
    </tr>
</table>
