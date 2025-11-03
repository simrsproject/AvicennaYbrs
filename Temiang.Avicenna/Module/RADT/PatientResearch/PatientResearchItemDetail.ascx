<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientResearchItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientResearchItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PatientResearch"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr style="display: none">
        <td class="label" />
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtResearchID" runat="server" Width="300px" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblResearchTitle" runat="server" Text="Research Title"></asp:Label></td>
        <td class="entry">
            <telerik:RadTextBox ID="txtResearchTitle" runat="server" Width="300px" MaxLength="200" /></td>
        <td width="20">
            <asp:RequiredFieldValidator ID="rfvResearchTitle" runat="server" ErrorMessage="Research Title required."
                ValidationGroup="PatientResearch" ControlToValidate="txtResearchTitle" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblPeriod" runat="server" Text="Period"></asp:Label></td>
        <td class="entry">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" />
                    </td>
                    <td>&nbsp;to&nbsp;
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20">
            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Start Date required."
                ValidationGroup="PatientResearch" ControlToValidate="txtStartDate" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="End Date required."
                ValidationGroup="PatientResearch" ControlToValidate="txtEndDate" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                OnItemsRequested="cboParamedicID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ParamedicName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20"></td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" Height="100px">
            </telerik:RadTextBox>
        </td>
        <td width="20px">
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td class="entry" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PatientResearch"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="PatientResearch" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
        <td width="20"></td>
        <td></td>
    </tr>
</table>
