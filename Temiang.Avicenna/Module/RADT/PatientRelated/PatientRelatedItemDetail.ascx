<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientRelatedItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.PatientRelatedItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PatientRelated"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                        <%# DataBinder.Eval(Container.DataItem, "MiddleName")%>
                        <%# DataBinder.Eval(Container.DataItem, "LastName")%>
                    </b>&nbsp;-&nbsp;
                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                    <br />
                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                    &nbsp;|&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                    <br />
                    Address :
                    <%# DataBinder.Eval(Container.DataItem, "StreetName")%>
                    <%# DataBinder.Eval(Container.DataItem, "City")%>
                    <%# DataBinder.Eval(Container.DataItem, "County")%>
                    <%# DataBinder.Eval(Container.DataItem, "ZipCode")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label></td>
        <td class="entry">
            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15"
                ReadOnly="true" /></td>
        <td width="20">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px">
            </telerik:RadTextBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name required."
                ValidationGroup="entry" ControlToValidate="txtFirstName" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtMiddleName" runat="server" Width="300px">
            </telerik:RadTextBox>
        </td>
        <td width="20">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px">
            </telerik:RadTextBox>
        </td>
        <td width="20">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td class="entry" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PatientRelated"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="PatientRelated" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
        <td width="20">
        </td>
        <td>
        </td>
    </tr>
</table>
