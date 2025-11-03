<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="OperationalTimeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.OperationalTimeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblOperationalTimeID" runat="server" Text="Operational Time ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtOperationalTimeID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvOperationalTimeID" runat="server" ErrorMessage="Operational Time ID required."
                    ValidationGroup="entry" ControlToValidate="txtOperationalTimeID"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblOperationalTimeName" runat="server" Text="Operational Time Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtOperationalTimeName" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvOperationalTimeName" runat="server" ErrorMessage="Operational Time Name required."
                    ValidationGroup="entry" ControlToValidate="txtOperationalTimeName"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStartTime1" runat="server" Text="Start Time 1"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTimePicker ID="txtStartTime1" runat="server" Width="100px" />
                <asp:Label ID="lblEndTime1" runat="server" Text="To"></asp:Label>
                <telerik:RadTimePicker ID="txtEndTime1" runat="server" Width="100px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvStartTime1" runat="server" ErrorMessage="Start Time 1 required."
                    ValidationGroup="entry" ControlToValidate="txtStartTime1"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvEndTime1" runat="server" ErrorMessage="End Time 1 required."
                    ValidationGroup="entry" ControlToValidate="txtEndTime1"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStartTime2" runat="server" Text="Start Time 2"></asp:Label></td>
            <td class="entry">
                <telerik:RadTimePicker ID="txtStartTime2" runat="server" Width="100px" />
                <asp:Label ID="lblEndTime2" runat="server" Text="To"></asp:Label>
                <telerik:RadTimePicker ID="txtEndTime2" runat="server" Width="100px" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStartTime3" runat="server" Text="Start Time 3"></asp:Label></td>
            <td class="entry">
                <telerik:RadTimePicker ID="txtStartTime3" runat="server" Width="100px" />
                <asp:Label ID="lblEndTime3" runat="server" Text="To"></asp:Label>
                <telerik:RadTimePicker ID="txtEndTime3" runat="server" Width="100px" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStartTime4" runat="server" Text="Start Time 4"></asp:Label></td>
            <td class="entry">
                <telerik:RadTimePicker ID="txtStartTime4" runat="server" Width="100px" />
                <asp:Label ID="lblEndTime4" runat="server" Text="To"></asp:Label>
                <telerik:RadTimePicker ID="txtEndTime4" runat="server" Width="100px" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStartTime5" runat="server" Text="Start Time 5"></asp:Label></td>
            <td class="entry">
                <telerik:RadTimePicker ID="txtStartTime5" runat="server" Width="100px" />
                <asp:Label ID="lblEndTime5" runat="server" Text="To"></asp:Label>
                <telerik:RadTimePicker ID="txtEndTime5" runat="server" Width="100px" MaxLength="5" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblOperationalTimeBackcolor" runat="server" Text="Backcolor in Schedule"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadColorPicker ShowIcon="true" ID="txtOperationalTimeBackcolor" runat="server" Width="300px" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>

    </table>
</asp:Content>

