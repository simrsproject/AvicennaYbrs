<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="RasproFormStart.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RasproFormStart" %>

<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproFlowChart.ascx" TagPrefix="uc1" TagName="RasproFlowChart" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproHeader.ascx" TagPrefix="uc1" TagName="RasproHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnRasproLineID" />
    <asp:HiddenField runat="server" ID="hdnRasproLineSeqNo" />
    <uc1:RasproHeader runat="server" ID="rasproHeader" />

    <table width="100%">
        <tr>
            <td style="width: 40%; vertical-align: top;">

                <fieldset style="width: 96%">
                    <legend>
                        <asp:Label runat="server" ID="lblSpecification" Font-Size="12px"></asp:Label></legend>

                    <telerik:RadDropDownTree runat="server" ID="cboAbRestrictionID" Width="100%"
                        DefaultMessage="Choose a Infection" DefaultValue="0" DataValueField="AbRestrictionID"
                        DataTextField="AbRestrictionName" DataFieldID="AbRestrictionID" DataFieldParentID="ParentID">
                        <DropDownSettings Height="250px" CloseDropDownOnSelection="true" />
                    </telerik:RadDropDownTree>

                </fieldset>
                <asp:Panel runat="server" ID="panMenu">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnPrev" runat="server" Text="<< Prev" Width="70" OnClick="btnPrev_OnClick" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" Text="Next >>" Width="70" OnClick="btnNext_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70" OnClientClick="Close();return false;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td style="width: 60%; vertical-align: top;">
            </td>
        </tr>
    </table>

</asp:Content>
