<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ConsumeMethodDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ConsumeMethodDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblSRConsumeMethod" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSRConsumeMethod" runat="server" Width="100px" MaxLength="10">
                </telerik:RadTextBox>
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSRConsumeMethod" runat="server" ErrorMessage="ID harus diisi"
                    ValidationGroup="entry" ControlToValidate="txtSRConsumeMethod" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRConsumeMethodName" runat="server" Text="Consume Method"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSRConsumeMethodName" runat="server" Width="300px" MaxLength="255">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSRConsumeMethodName" runat="server" ErrorMessage="Consume Method harus diisi"
                    ValidationGroup="entry" ControlToValidate="txtSRConsumeMethodName" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
                <tr>
            <td class="label">
                <asp:Label ID="Label10" runat="server" Text="Line No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtLineNumber" runat="server" Width="100px" NumberFormat-DecimalDigits="0" Type="Number"></telerik:RadNumericTextBox>

            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>

        <tr>
            <td class="label">
                <asp:Label ID="lblTimeSequence" runat="server" Text="Time Sequence"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTimeSequence" runat="server" Width="300px" MaxLength="50">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvTimeSequence" runat="server" ErrorMessage="Time Sequence harus diisi"
                    ValidationGroup="entry" ControlToValidate="txtTimeSequence" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSygnaText" runat="server" Text="Cigna Text"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSygnaText" runat="server" Width="300px" MaxLength="50">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSygnaText" runat="server" ErrorMessage="Cigna Text harus diisi"
                    ValidationGroup="entry" ControlToValidate="txtSygnaText" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblIterationQty" runat="server" Text="Iteration Qty"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtIterationQty" runat="server" Width="100px" MaxLength="6"
                    MinValue="0" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblIterationInInterval" runat="server" Text="Iteration In Interval"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rbtIterationInInterval" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="day" Text="Day" />
                    <asp:ListItem Value="minute" Text="Minute" />
                </asp:RadioButtonList>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvIterationInInterval" runat="server" ErrorMessage="Iteration In Interval required."
                    ValidationGroup="entry" ControlToValidate="rbtIterationInInterval" SetFocusOnError="True"
                    Width="20px">
                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTime" runat="server" Text="Time 01"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime01" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Time 02"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime02" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Time 03"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime03" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Time 04"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime04" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Time 05"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime05" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label5" runat="server" Text="Time 06"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime06" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Time 07"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime07" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Time 08"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime08" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label8" runat="server" Text="Time 09"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime09" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label9" runat="server" Text="Time 10"></asp:Label>
            </td>
            <td>
                <telerik:RadTimePicker ID="txtTime10" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>