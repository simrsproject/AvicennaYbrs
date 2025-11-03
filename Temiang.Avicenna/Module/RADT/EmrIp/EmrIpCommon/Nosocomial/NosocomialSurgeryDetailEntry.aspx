<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialSurgeryDetailEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialSurgeryDetailEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Lama Operasi (minute)"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtLamaOperasi" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Time
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtMonitoringDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rf1" runat="server" ErrorMessage="Monitoring time required."
                    ValidationGroup="entry" ControlToValidate="txtMonitoringDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="labelcaption" colspan="3">Bandage Replacement
            </td>
        </tr>
        <tr>
            <td class="label">Exudate Character
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSRExudateCharacter" Width="304px" />
            </td>
            <td width="20px"></td>
        </tr>

        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsAfDrain" Text="Aff Drain" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsAfSuture" Text="Aff Suture" />
            </td>
            <td width="20px"></td>
        </tr>


        <tr>
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Injury Condition"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInjuryCondition" runat="server" Width="304px" MaxLength="250" />
            </td>
            <td width="20px"></td>
        </tr>

        <tr>
            <td class="labelcaption" colspan="3">HAIs Risk
            </td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsRedness" Text="Redness" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsSwollen" Text="Swollen" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsPain" Text="Suppressed Pain" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsFeelingHot" Text="Feeling Hot" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsTempAbove38" Text="Fever > 38 C" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsPus" Text="Pus" />
            </td>
            <td width="20px"></td>
        </tr>

        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsCulture" Text="Culture" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsIdoDiagnose" Text="IDO Diagnose" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsGlukosa" Text="Abnormally Glucose" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="PPA"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMonitoringByName" runat="server" Width="304px" ReadOnly="True" />
            </td>
            <td width="20px"></td>
        </tr>


        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Note"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNote" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
    </table>
</asp:Content>
